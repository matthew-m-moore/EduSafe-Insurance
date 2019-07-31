using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.Repositories;
using EduSafe.IO.Excel.Records;
using EduSafe.WebApi.Models;

namespace EduSafe.WebApi.Adapters
{
    internal class InstitutionalPremiumComputationAdapter
    {
        private const string _websiteScenarioDataFile = "EduSafe.WebApi.App_Data.EduSafe-Website-Scenario-Data.xlsx";

        private const string _institutional = "Institutional";

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
            baseScenario = AdjustBaseScenario(institutionalDataDictionary, baseScenario, institutionInputEntry);

            var institutionOutputSummary = new InstitutionOutputSummary
            {
                InstitutionOutputEntries = new List<InstitutionOutputEntry>()
            };

            foreach (var numberOfCoverageMonths in _warrantyCoverageMonths)
            {
                foreach (var paybackOption in _unenrollmentPaybackOption)
                {
                    foreach(var debtMultiplier in _averageGraduationDebtMultiplier)
                    {
                        var institutionOutputEntry = new InstitutionOutputEntry();
                        institutionOutputSummary.InstitutionOutputEntries.Add(institutionOutputEntry);
                    }
                }
            }

            institutionOutputSummary.OutputTitle = "Results Computed Successfully!";
            return institutionOutputSummary;
        }

        private EnrollmentModelScenarioRecord AdjustBaseScenario(
            ConcurrentDictionary<string, Dictionary<string, InstitutionalGradData>> institutionalDataDictionary, 
            EnrollmentModelScenarioRecord baseScenario, 
            InstitutionInputEntry institutionInputEntry)
        {
            var degreeType = institutionInputEntry.TwoYearOrFourYearSchool;
            var collegeType = institutionInputEntry.PublicOrPrivateSchool;
            var institutionalGradData = institutionalDataDictionary[degreeType][collegeType];

            baseScenario.TwoYearGradTarget = institutionInputEntry.GraduationWithinYears1;
            baseScenario.ThreeYearGradTarget = institutionInputEntry.GraduationWithinYears2;
            baseScenario.FourYearGradTarget = institutionInputEntry.GraduationWithinYears3;

            baseScenario.UnemploymentTarget = institutionalGradData.UnemploymentTarget;

            return baseScenario;
        }
    }
}