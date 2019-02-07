using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduSafe.Core.Repositories;
using EduSafe.Core.Tests.BusinessLogic.Models.Premiums;

namespace EduSafe.Core.Tests.BusinessLogic.Scenarios
{
    [TestClass]
    public class PremiumComputationEngineTests
    {
        private const string _dataFile = "EduSafe.Core.Tests.Resources.Dropout-Rate-Test-Scenarios-Single.xlsx";
        private static Stream _inputFileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(_dataFile);

        private bool _outputExcel = false;
        private double _precision = 1e-8;

        [TestMethod, Owner("Matthew Moore")]
        public void PremiumComputationEngine_LoadSingleScenarioFromExcel_CheckResults()
        {
            var premiumComputationRepository = new PremiumComputationRepository(_inputFileStream);
            var premiumComputationScenarios = premiumComputationRepository.GetPremiumComputationScenarios();
            var premiumComputationScenario = premiumComputationScenarios.Single();
            var premiumComputationResult = premiumComputationScenario.Value.ComputePremiumResult();

            var premium = premiumComputationResult.CalculatedMonthlyPremium;
            Assert.AreEqual(85.94250024, premium, _precision);

            if (_outputExcel)
            {
                var excelFileWriter = PremiumCalculationTests.CreateExcelOutput(
                    premiumComputationScenario.Value.RepricingModel.EnrollmentModel.EnrollmentStateTimeSeries,
                    premiumComputationScenario.Value.PremiumCalculation,
                    premiumComputationResult.ServicingCosts);

                excelFileWriter.ExportWorkbook();
            }
        }
    }
}
