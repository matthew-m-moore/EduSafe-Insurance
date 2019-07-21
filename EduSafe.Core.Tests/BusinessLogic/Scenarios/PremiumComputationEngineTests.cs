using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduSafe.Core.Repositories;
using EduSafe.Core.Tests.BusinessLogic.Models.Premiums;
using EduSafe.Common.Enums;

namespace EduSafe.Core.Tests.BusinessLogic.Scenarios
{
    [TestClass]
    public class PremiumComputationEngineTests
    {
        private const string _dataFileOne = "EduSafe.Core.Tests.Resources.Dropout-Rate-Test-Scenarios-Single.xlsx";
        private const string _dataFileTwo = "EduSafe.Core.Tests.Resources.EduSafe-Website-For-Profit-Scenario-Testing-Small.xlsx";

        private static readonly Stream _inputFileStreamOne = Assembly.GetExecutingAssembly().GetManifestResourceStream(_dataFileOne);
        private static readonly Stream _inputFileStreamTwo = Assembly.GetExecutingAssembly().GetManifestResourceStream(_dataFileTwo);

        private readonly bool _outputExcel = false;
        private readonly double _precision = 1e-8;

        [TestMethod, Owner("Matthew Moore")]
        public void PremiumComputationEngine_LoadSingleScenarioFromExcel_CheckResults()
        {
            var premiumComputationRepository = new PremiumComputationRepository(_inputFileStreamOne);
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

        [TestMethod, Owner("Matthew Moore")]
        public void PremiumComputationEngine_LoadSingleScenarioByIdFromExcel_CheckResults()
        {
            var premiumComputationRepository = new PremiumComputationRepository(_inputFileStreamOne);
            var premiumComputationScenario = premiumComputationRepository.GetPremiumComputationScenarioById(1);
            var premiumComputationResult = premiumComputationScenario.ComputePremiumResult();

            var premium = premiumComputationResult.CalculatedMonthlyPremium;
            Assert.AreEqual(85.94250024, premium, _precision);

            if (_outputExcel)
            {
                var excelFileWriter = PremiumCalculationTests.CreateExcelOutput(
                    premiumComputationScenario.RepricingModel.EnrollmentModel.EnrollmentStateTimeSeries,
                    premiumComputationScenario.PremiumCalculation,
                    premiumComputationResult.ServicingCosts);

                excelFileWriter.ExportWorkbook();
            }
        }

        [TestMethod, Owner("Matthew Moore")]
        public void PremiumComputationEngine_Copy_CheckResults()
        {
            var premiumComputationRepository = new PremiumComputationRepository(_inputFileStreamOne);
            var premiumComputationScenario = premiumComputationRepository.GetPremiumComputationScenarioById(1);

            var copyOfPremiumComputationScenario = premiumComputationScenario.Copy();
            var premiumComputationResult = copyOfPremiumComputationScenario.ComputePremiumResult();

            var premium = premiumComputationResult.CalculatedMonthlyPremium;
            Assert.AreEqual(85.94250024, premium, _precision);

            if (_outputExcel)
            {
                var excelFileWriter = PremiumCalculationTests.CreateExcelOutput(
                    premiumComputationScenario.RepricingModel.EnrollmentModel.EnrollmentStateTimeSeries,
                    premiumComputationScenario.PremiumCalculation,
                    premiumComputationResult.ServicingCosts);

                excelFileWriter.ExportWorkbook();
            }
        }

        [TestMethod, Owner("Matthew Moore")]
        public void PremiumComputationEngine_SingleScenarioWithZeroGradSchoolTarget_CheckResults()
        {
            var premiumComputationRepository = new PremiumComputationRepository(_inputFileStreamTwo);
            var premiumComputationScenario = premiumComputationRepository.GetPremiumComputationScenarioById(2);
            var premiumComputationResult = premiumComputationScenario.ComputePremiumResult();

            var premium = premiumComputationResult.CalculatedMonthlyPremium;
            Assert.AreEqual(115.85580399, premium, _precision);

            var gradSchoolRate = premiumComputationScenario
                .RepricingModel.EnrollmentModel
                .EnrollmentStateTimeSeries.Last()
                .GetTotalState(StudentEnrollmentState.GraduateSchool);
            Assert.AreEqual(0.0, gradSchoolRate, _precision);

            if (_outputExcel)
            {
                var excelFileWriter = PremiumCalculationTests.CreateExcelOutput(
                    premiumComputationScenario.RepricingModel.EnrollmentModel.EnrollmentStateTimeSeries,
                    premiumComputationScenario.PremiumCalculation,
                    premiumComputationResult.ServicingCosts);

                excelFileWriter.ExportWorkbook();
            }
        }

        [TestMethod, Owner("Matthew Moore")]
        public void PremiumComputationEngine_SingleScenarioWithDropOutWarranty_CheckResults()
        {
            var premiumComputationRepository = new PremiumComputationRepository(_inputFileStreamTwo);
            var premiumComputationScenario = premiumComputationRepository.GetPremiumComputationScenarioById(7);
            var premiumComputationResult = premiumComputationScenario.ComputePremiumResult();

            var premium = premiumComputationResult.CalculatedMonthlyPremium;
            Assert.AreEqual(78.4932984, premium, _precision);

            if (_outputExcel)
            {
                var excelFileWriter = PremiumCalculationTests.CreateExcelOutput(
                    premiumComputationScenario.RepricingModel.EnrollmentModel.EnrollmentStateTimeSeries,
                    premiumComputationScenario.PremiumCalculation,
                    premiumComputationResult.ServicingCosts);

                excelFileWriter.ExportWorkbook();
            }
        }

        [TestMethod, Owner("Matthew Moore")]
        public void PremiumComputationEngine_SingleScenarioWithDropOutWarrantySpreadPayments_CheckResults()
        {
            var premiumComputationRepository = new PremiumComputationRepository(_inputFileStreamTwo);
            var premiumComputationScenario = premiumComputationRepository.GetPremiumComputationScenarioById(8, true);
            var premiumComputationResult = premiumComputationScenario.ComputePremiumResult();

            var premium = premiumComputationResult.CalculatedMonthlyPremium;
            Assert.AreEqual(65.49231415, premium, _precision);

            if (_outputExcel)
            {
                var excelFileWriter = PremiumCalculationTests.CreateExcelOutput(
                    premiumComputationScenario.RepricingModel.EnrollmentModel.EnrollmentStateTimeSeries,
                    premiumComputationScenario.PremiumCalculation,
                    premiumComputationResult.ServicingCosts, false);

                excelFileWriter.ExportWorkbook();
            }
        }

        [TestMethod, Owner("Matthew Moore")]
        public void PremiumComputationEngine_SingleScenarioWithDropOutWarrantyOnly_CheckResults()
        {
            var premiumComputationRepository = new PremiumComputationRepository(_inputFileStreamTwo);
            var premiumComputationScenario = premiumComputationRepository.GetPremiumComputationScenarioById(9, true);
            var premiumComputationResult = premiumComputationScenario.ComputePremiumResult();

            var premium = premiumComputationResult.CalculatedMonthlyPremium;
            Assert.AreEqual(122.046312304, premium, _precision);

            if (_outputExcel)
            {
                var excelFileWriter = PremiumCalculationTests.CreateExcelOutput(
                    premiumComputationScenario.RepricingModel.EnrollmentModel.EnrollmentStateTimeSeries,
                    premiumComputationScenario.PremiumCalculation,
                    premiumComputationResult.ServicingCosts, false);

                excelFileWriter.ExportWorkbook();
            }
        }
    }
}
