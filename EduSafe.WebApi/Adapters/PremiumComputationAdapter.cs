using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using EduSafe.Common.Utilities;
using EduSafe.Core.Repositories;
using EduSafe.Core.Repositories.Excel;
using EduSafe.WebApi.Models;

namespace EduSafe.WebApi.Adapters
{
    internal class PremiumComputationAdapter
    {
        private const string _websiteScenarioDataFile = "EduSafe.WebApi.App_Data.EduSafe-Website-Scenario-Data.xlsx";
        private const string _websiteCollegeMajorDataFile = "EduSafe.WebApi.App_Data.EduSafe-Website-College-Major-Data.xlsx";

        private const string _freshman = "Freshman Year";
        private const string _sophmore = "Sophmore Year";
        private const string _junior = "Junior Year";
        private const string _senior = "Senior Year";
        private const string _5thYear = "5th Year";
        private const string _6thYear = "6th Year";

        private const string _privateSchool = "Private School";
        private const string _forProfitCollege = "For-Profit College";

        private static Stream _websiteScenarioDataFileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(_websiteScenarioDataFile);
        private static Stream _websiteCollegeMajorDataFileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(_websiteCollegeMajorDataFile);
        private readonly List<int> _incomeCoverageMonths = new List<int> { 3, 6, 12 };

        private PremiumComputationRepository _premiumComputationRepository;
        private CollegeMajorDataRepository _collegeMajorDataRepository;

        internal PremiumComputationAdapter()
        {
            _premiumComputationRepository = new PremiumComputationRepository(_websiteScenarioDataFileStream);
            _collegeMajorDataRepository = new CollegeMajorDataRepository(_websiteCollegeMajorDataFileStream);
        }

        internal ModelOutputSummary RunPremiumComputationScenarios(ModelInputEntry modelInputEntry)
        {
            var baseScenariosDictionary = _premiumComputationRepository.GetEnrollmentModelScenariosByName();
            var collegeMajorDateDictionary = _collegeMajorDataRepository.CollegeMajorDataDictionary;

            var schoolType = modelInputEntry.PublicOrPrivateSchool;
            var baseScenario = baseScenariosDictionary[schoolType];

            baseScenario.AnnualIncome = modelInputEntry.IncomeCoverageAmount;
            baseScenario.UnemploymentTarget = collegeMajorDateDictionary[modelInputEntry.CollegeMajor].UnemploymentRate;

            if (schoolType == _privateSchool)
                baseScenario.UnemploymentTarget = collegeMajorDateDictionary[modelInputEntry.CollegeMajor].LowEndUnemploymentRate;
            if (schoolType == _forProfitCollege)
                baseScenario.UnemploymentTarget = collegeMajorDateDictionary[modelInputEntry.CollegeMajor].HighEndUnemploymentRate;

            var startDate = modelInputEntry.CollegeStartDate;
            var endDate = modelInputEntry.ExpectedGraduationDate;
            var rollForwardPeriods = DetermineRollForwardPeriods(startDate, endDate, out ModelOutputHeaders modelOutputHeaders);

            var modelOutputSummary = new ModelOutputSummary
            {
                ModelOutputHeaders = modelOutputHeaders,
                ModelOutputEntries = new List<ModelOutputEntry>()
            };

            foreach (var numberOfCoverageMonths in _incomeCoverageMonths)
            {
                var modelOutputEntry = new ModelOutputEntry { MonthsOfSalaryCoverage = numberOfCoverageMonths };
                baseScenario.CoverageMonths = numberOfCoverageMonths;

                for (var year = 1; year <= 3; year++)
                {
                    var rollForwardPeriod = rollForwardPeriods[year - 1];
                    var premiumComputationScenario = _premiumComputationRepository.GetPremiumComputationScenario(baseScenario);
                    var premiumComputationResults = premiumComputationScenario.ComputePremiumResult(rollForwardPeriod);
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