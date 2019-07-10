using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using EduSafe.Common;
using EduSafe.Common.Utilities;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.Repositories;
using EduSafe.IO.Excel.Records;
using EduSafe.WebApi.Models;

namespace EduSafe.WebApi.Adapters
{
    internal class PremiumComputationAdapter
    {
        private const string _websiteScenarioDataFile = "EduSafe.WebApi.App_Data.EduSafe-Website-Scenario-Data.xlsx";

        private const string _freshman = "Freshman Year";
        private const string _sophmore = "Sophmore Year";
        private const string _junior = "Junior Year";
        private const string _senior = "Senior Year";
        private const string _5thYear = "5th Year";
        private const string _6thYear = "6th Year";

        private const string _otherMajor = "Other";

        private const string _privateSchool = "Private School";
        private const string _forProfitCollege = "For-Profit College";   

        private static Stream _websiteScenarioDataFileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(_websiteScenarioDataFile);
        private readonly List<int> _incomeCoverageMonths = new List<int> { 1, 2, 3, 6, 12 };

        private PremiumComputationRepository _premiumComputationRepository;

        internal PremiumComputationAdapter()
        {
            _premiumComputationRepository = new PremiumComputationRepository(_websiteScenarioDataFileStream);
        }

        internal ModelOutputSummary RunPremiumComputationScenarios(ModelInputEntry modelInputEntry)
        {
            var baseScenariosDictionary = _premiumComputationRepository.GetEnrollmentModelScenariosByName();
            var collegeMajorDateDictionary = CollegeDataAdapter.CollegeMajorDataRepository.CollegeMajorDataDictionary;

            var schoolType = modelInputEntry.PublicOrPrivateSchool;
            var baseScenario = baseScenariosDictionary[schoolType];
            baseScenario = AdjustBaseScenario(collegeMajorDateDictionary, baseScenario, modelInputEntry, schoolType);

            var startDate = modelInputEntry.CollegeStartDate;
            var endDate = modelInputEntry.ExpectedGraduationDate;
            var rollForwardPeriods = DetermineRollForwardPeriods(startDate, endDate, out ModelOutputHeaders modelOutputHeaders);

            var modelOutputSummary = new ModelOutputSummary
            {
                ModelOutputHeaders = modelOutputHeaders,
                DropOutCoveragePercentage = (int)(baseScenario.DropOutOptionRatio * Constants.PercentagePoints),
                GradSchoolCoveragePercentage = (int)(baseScenario.GradSchoolOptionRatio * Constants.PercentagePoints),
                EarlyHireCoveragePercentage = (int)(baseScenario.EarlyHireOptionRatio * Constants.PercentagePoints),
                ModelOutputEntries = new List<ModelOutputEntry>()
            };

            foreach (var numberOfCoverageMonths in _incomeCoverageMonths)
            {               
                baseScenario.UnemploymentCoverage = modelInputEntry.IncomeCoverageAmount * (numberOfCoverageMonths / Constants.MonthsInOneYear);
                var premiumComputationScenario = _premiumComputationRepository.GetPremiumComputationScenario(baseScenario);
                var incomeCoverageAmount = premiumComputationScenario.PremiumCalculation.PremiumCalculationModelInput.UnemploymentCoverageAmount;
                var modelOutputEntry = new ModelOutputEntry { AmountOfSalaryCoverage = incomeCoverageAmount };

                for (var year = 1; year <= 3; year++)
                {
                    var rollForwardPeriod = rollForwardPeriods[year - 1];                  
                    var premiumComputationResults = premiumComputationScenario.Copy().ComputePremiumResult(rollForwardPeriod);
                    var monthlyPremium = premiumComputationResults.CalculatedMonthlyPremium;

                    switch (year)
                    {
                        case 1:
                            modelOutputEntry.YearOneMonthlyPremiumAmount = monthlyPremium;
                            break;
                        case 2:
                            modelOutputEntry.YearTwoMonthlyPremiumAmount = monthlyPremium;
                            break;
                        case 3:
                            modelOutputEntry.YearThreeMonthlyPremiumAmount = monthlyPremium;
                            break;
                    }
                }

                modelOutputSummary.ModelOutputEntries.Add(modelOutputEntry);
            }

            modelOutputSummary.OutputTitle = "Results Computed Successfully!";
            return modelOutputSummary;
        }

        private EnrollmentModelScenarioRecord AdjustBaseScenario(
            ConcurrentDictionary<string, CollegeMajorData> collegeMajorDateDictionary,
            EnrollmentModelScenarioRecord baseScenario,
            ModelInputEntry modelInputEntry, 
            string schoolType)
        {
            // Let the user type in whatever major they want, even if we don't have it
            var collegeMajorData = collegeMajorDateDictionary[_otherMajor];
            if (collegeMajorDateDictionary.ContainsKey(modelInputEntry.CollegeMajor))
                collegeMajorData = collegeMajorDateDictionary[modelInputEntry.CollegeMajor];

            baseScenario.UnemploymentTarget = collegeMajorData.UnemploymentRate;

            if (schoolType == _privateSchool)
                baseScenario.UnemploymentTarget = collegeMajorData.LowEndUnemploymentRate;
            if (schoolType == _forProfitCollege)
                baseScenario.UnemploymentTarget = collegeMajorData.HighEndUnemploymentRate;
            
            baseScenario.UnemploymentTarget *= Constants.PercentagePoints;

            var derivedUnemploymentTarget = Constants.PercentagePoints -
                (baseScenario.DropOutTarget + baseScenario.GradSchoolTarget + baseScenario.HireEarlyTarget);
            baseScenario.UnemploymentTarget = Math.Min(baseScenario.UnemploymentTarget, derivedUnemploymentTarget);

            baseScenario.HireTarget = Constants.PercentagePoints -
                (baseScenario.DropOutTarget + baseScenario.GradSchoolTarget + baseScenario.HireEarlyTarget + baseScenario.UnemploymentTarget);

            return baseScenario;
        }

        private List<int> DetermineRollForwardPeriods(DateTime startDate, DateTime endDate, out ModelOutputHeaders modelOutputHeaders)
        {
            var graduatedInPast = (endDate.Ticks <= DateTime.Now.Ticks);
            var enrollingInFuture = (startDate.Ticks >= DateTime.Now.Ticks);
            var totalMonthsPlanned = DateUtility.MonthsBetweenTwoDates(startDate, endDate);
            var monthsSinceStarting = DateUtility.MonthsBetweenTwoDates(startDate, DateTime.Now);
            var monthsLeftToFinish = totalMonthsPlanned - monthsSinceStarting;

            // The goal here is to return an output, no matter what they pick in terms of dates
            if (graduatedInPast || (totalMonthsPlanned <= 0))
            {
                modelOutputHeaders = new ModelOutputHeaders(_freshman, _sophmore, _junior);
                return new List<int> { 0, 12, 24 };
            }

            if (enrollingInFuture)
            {
                if (totalMonthsPlanned > 36)
                {
                    modelOutputHeaders = new ModelOutputHeaders(_freshman, _sophmore, _junior);
                    return new List<int> { 0, 12, 24 };
                }
                else if (totalMonthsPlanned <= 36)
                {
                    modelOutputHeaders = new ModelOutputHeaders(_sophmore, _junior, _senior);
                    return new List<int> { 12, 24, 36 };
                }
                else if (totalMonthsPlanned <= 24)
                {
                    modelOutputHeaders = new ModelOutputHeaders(_junior, _senior, _5thYear);
                    return new List<int> { 24, 36, 48 };
                }
                else
                {
                    modelOutputHeaders = new ModelOutputHeaders(_senior, _5thYear, _6thYear);
                    return new List<int> { 36, 48, 60 };
                }
            }

            if (monthsSinceStarting < 12)
            {
                if (monthsLeftToFinish >= 36)
                {
                    modelOutputHeaders = new ModelOutputHeaders(_sophmore, _junior, _senior);
                    return new List<int> { 12, 24, 36 };
                }
                else if (monthsLeftToFinish >= 24)
                {
                    modelOutputHeaders = new ModelOutputHeaders(_junior, _senior, _5thYear);
                    return new List<int> { 24, 36, 48 };
                }
                else
                {
                    modelOutputHeaders = new ModelOutputHeaders(_senior, _5thYear, _6thYear);
                    return new List<int> { 36, 48, 60 };
                }
            }
            else if (monthsSinceStarting < 24)
            {
                if (monthsLeftToFinish <= 24)
                {
                    modelOutputHeaders = new ModelOutputHeaders(_junior, _senior, _5thYear);
                    return new List<int> { 24, 36, 48 };
                }
                else
                {
                    modelOutputHeaders = new ModelOutputHeaders(_senior, _5thYear, _6thYear);
                    return new List<int> { 36, 48, 60 };
                }
            }
            else
            {
                modelOutputHeaders = new ModelOutputHeaders(_senior, _5thYear, _6thYear);
                return new List<int> { 36, 48, 60 };
            }
        }
    }
}