using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using EduSafe.Common;
using EduSafe.Common.Utilities;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.Repositories;
using EduSafe.IO.Excel.Records;
using EduSafe.WebApi.Models.Institutions;

namespace EduSafe.WebApi.Adapters
{
    internal class InstitutionalPremiumComputationAdapter
    {
        private const string _websiteScenarioDataFile = "EduSafe.WebApi.App_Data.EduSafe-Website-Scenario-Data.xlsx";

        private const string _institutional = "Institutional";
        private const string _fourYearDegree = "Four-Year";

        private const int _standardLoanTermInMonths = 120;
        private const int _numberOfPaymentsCovered = 36;

        private const double _assumedNumberOfSigma = 2.0;
        private const double _sigmaPerYearOfWarranty = 0.25;

        private static Stream _websiteScenarioDataFileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(_websiteScenarioDataFile);

        private readonly List<int> _warrantyCoverageMonths = new List<int> { 12, 24 };
        private readonly List<double> _unenrollmentPaybackOption = new List<double> { 0.0, 0.5 };
        private readonly List<double> _averageGraduationDebtMultiplier = new List<double> { 0.5, 1.0, 1.5 };

        private PremiumComputationRepository _premiumComputationRepository;

        internal InstitutionalPremiumComputationAdapter()
        {
            _premiumComputationRepository = new PremiumComputationRepository(_websiteScenarioDataFileStream);
        }

        internal InstitutionOutputSummary RunPremiumComputationScenarios(InstitutionInputEntry institutionInputEntry)
        {
            var baseScenariosDictionary = _premiumComputationRepository.GetEnrollmentModelScenariosByName();
            var institutionalDataDictionary = CollegeDataAdapter.CollegeDataRepository.InstitutionalDataDictionary;

            var degreeType = institutionInputEntry.TwoYearOrFourYearSchool;
            var baseScenario = baseScenariosDictionary[_institutional + " " + degreeType];

            var collegeType = institutionInputEntry.PublicOrPrivateSchool;
            var institutionalGradData = institutionalDataDictionary[degreeType][collegeType];

            baseScenario = AdjustBaseScenario(institutionalGradData, baseScenario, institutionInputEntry, degreeType);

            var institutionOutputSummary = new InstitutionOutputSummary
            {
                InstitutionOutputEntries = new List<InstitutionOutputEntry>()
            };

            foreach (var numberOfCoverageMonths in _warrantyCoverageMonths)
            {
                foreach (var paybackOption in _unenrollmentPaybackOption)
                {
                    foreach (var debtMultiplier in _averageGraduationDebtMultiplier)
                    {
                        var unemploymentDebtAmount = debtMultiplier * institutionInputEntry.AverageLoanDebtAtGraduation;
                        var unenrollmentDebtAmount = unemploymentDebtAmount / 2;

                        var interestRate = institutionalGradData.LoanInterestRate;
                        var unemploymentCoverage = CalculateCoverageAmount(unemploymentDebtAmount, interestRate, out var graduateMonthlyPayment);
                        var unenrollmentCoverage = CalculateCoverageAmount(unenrollmentDebtAmount, interestRate, out var dropOutMonthlyPayment);

                        baseScenario.UnemploymentCoverage = unemploymentCoverage;
                        baseScenario.DropOutWarranty = unenrollmentCoverage;
                        baseScenario.DropOutOptionRatio = paybackOption;
                        baseScenario.WarrantyCoverageMonths = numberOfCoverageMonths;

                        var premiumComputationScenario = _premiumComputationRepository.GetPremiumComputationScenario(baseScenario);
                        var premiumComputationResults = premiumComputationScenario.Copy().ComputePremiumResult();
                        var averageMonthlyPremium = premiumComputationResults.ResultSummary.AverageMonthlyPremiums;
                        var numberOfStudents = institutionInputEntry.StudentsPerStartingClass;

                        // The assumption here is that the average debt amount provided is the median of a Gaussian distribution.
                        // In this distrubtion, the mean happens to equal exactly two standard deviations, by assumption.
                        // Also, the assumed effect of one extra year of warranty coverage is 1/4 of a standard deviation.
                        var dropOutAdjuster = _sigmaPerYearOfWarranty * Math.Max((numberOfCoverageMonths / Constants.MonthsInOneYear) - 1.0, 0.0);
                        var zValue = (debtMultiplier * _assumedNumberOfSigma) - _assumedNumberOfSigma + dropOutAdjuster;
                        var probabilityOfCoverage = MathUtility.NormalDistributionProbability(zValue);

                        // At coveage equal to the average debt amount, this boils down to decreasing the default rate by half.
                        // The idea being that half of all borrowers would be under the average and protected from default.
                        var updatedCohortDefaultRate = institutionInputEntry.StartingCohortDefaultRate * (1 - probabilityOfCoverage);

                        var institutionOutputEntry = new InstitutionOutputEntry
                        {
                            StudentsMonthlyDebtPayment = graduateMonthlyPayment,
                            UndergraduateUnemploymentCoverage = unemploymentCoverage,
                            UnenrollmentWarrantyCoverage = unenrollmentCoverage,
                            UnenrollmentPaybackOption = paybackOption,
                            AverageMonthlyPayment = averageMonthlyPremium * numberOfStudents,
                            EndingCohortDefaultRate = updatedCohortDefaultRate,
                        };

                        institutionOutputSummary.InstitutionOutputEntries.Add(institutionOutputEntry);
                    }
                }
            }

            institutionOutputSummary.OutputTitle = "Results Computed Successfully!";
            return institutionOutputSummary;
        }

        private double CalculateCoverageAmount(double debtAmount, double interestRate, out double monthlyPayment)
        {
            monthlyPayment = MathUtility.CalculateFixedAnnuityPayment(debtAmount, interestRate, _standardLoanTermInMonths);
            var totalCoverage = monthlyPayment * _numberOfPaymentsCovered;

            var coverageRoundedToNearestHundred = Math.Ceiling(totalCoverage / Constants.PercentagePoints) * Constants.PercentagePoints;
            return coverageRoundedToNearestHundred;
        }

        private EnrollmentModelScenarioRecord AdjustBaseScenario(
            InstitutionalGradData institutionalGradData, 
            EnrollmentModelScenarioRecord baseScenario, 
            InstitutionInputEntry institutionInputEntry,
            string degreeType)
        {
            if (degreeType.ToUpper() == _fourYearDegree.ToUpper())
            {
                baseScenario.FourYearGradTarget = institutionInputEntry.GraduationWithinYears1;
                baseScenario.FiveYearGradTarget = institutionInputEntry.IncrementalTargetYear2;
                baseScenario.SixYearGradTarget = institutionInputEntry.IncrementalTargetYear3;
            }
            else
            {
                baseScenario.TwoYearGradTarget = institutionInputEntry.GraduationWithinYears1;
                baseScenario.ThreeYearGradTarget = institutionInputEntry.IncrementalTargetYear2;
                baseScenario.FourYearGradTarget = institutionInputEntry.IncrementalTargetYear3;
            }

            baseScenario.UnemploymentTarget = institutionalGradData.UnemploymentTarget;
            return baseScenario;
        }
    }
}