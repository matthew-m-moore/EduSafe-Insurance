using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduSafe.Core.BusinessLogic.Models;
using EduSafe.Core.BusinessLogic.Models.Premiums;
using EduSafe.Core.Tests.BusinessLogic.Models.Premiums;
using EduSafe.Core.Tests.BusinessLogic.Models.StudentEnrollment;

namespace EduSafe.Core.Tests.BusinessLogic.Models
{
    [TestClass]
    public class RollForwardRepricingModelTests
    {
        private bool _outputExcel = false;
        private int _numberOfPeriodsToRollForward_FullYear = 12;
        private int _numberOfPeriodsToRollForward_HalfYear = 6;
        private double _previouslyPaidInPremiums = 1000;
        private double _precision = 1e-8;

        [TestMethod, Owner("Matthew Moore")]
        public void RollforwardRepricingModel_HalfYearWithOutPaidInPremiums_NumericalCalculation()
        {
            var analyticalOuput = false;
            var enrollmentModel = EnrollmentModelTests.PopulateEnrollmentModel(includePostGraduationTargets: true);
            var servicingCostsModel = ServicingCostsModelTests.PopulateServicingCostsModel();
            var rollForwardRepricingModel = new RollForwardRepricingModel(enrollmentModel, servicingCostsModel);

            var isNewStudent = true;
            var rollForwardEnrollmentStateTimeSeries = rollForwardRepricingModel.RollForwardEnrollmentStates(_numberOfPeriodsToRollForward_HalfYear);
            var rollForwardServicingCosts = rollForwardRepricingModel.RollForwardServicingCosts(_numberOfPeriodsToRollForward_HalfYear, isNewStudent);

            var premiumCalculationModelInput = PremiumCalculationTests.PreparePremiumCalculationModelInput();

            var premium = PremiumCalculationTests.CalculatePremiumNumerically(
                premiumCalculationModelInput,
                rollForwardServicingCosts,
                rollForwardEnrollmentStateTimeSeries,
                out PremiumCalculation premiumCalculation,
                out DataTable servicingCosts);

            Assert.AreEqual(90.79726075, premium, _precision);

            if (_outputExcel)
            {
                var excelFileWriter = PremiumCalculationTests.CreateExcelOutput(
                    rollForwardEnrollmentStateTimeSeries,
                    premiumCalculation,
                    servicingCosts,
                    analyticalOuput);

                excelFileWriter.ExportWorkbook();
            }
        }

        [TestMethod, Owner("Matthew Moore")]
        public void RollforwardRepricingModel_HalfYearWithOutPaidInPremiums_AnalyticalCalculation()
        {
            var analyticalOuput = true;
            var enrollmentModel = EnrollmentModelTests.PopulateEnrollmentModel(includePostGraduationTargets: true);
            var servicingCostsModel = ServicingCostsModelTests.PopulateServicingCostsModel();
            var rollForwardRepricingModel = new RollForwardRepricingModel(enrollmentModel, servicingCostsModel);

            var isNewStudent = true;
            var rollForwardEnrollmentStateTimeSeries = rollForwardRepricingModel.RollForwardEnrollmentStates(_numberOfPeriodsToRollForward_HalfYear);
            var rollForwardServicingCosts = rollForwardRepricingModel.RollForwardServicingCosts(_numberOfPeriodsToRollForward_HalfYear, isNewStudent);

            var premiumCalculationModelInput = PremiumCalculationTests.PreparePremiumCalculationModelInput();

            var premium = PremiumCalculationTests.CalculatePremiumAnalytically(
                premiumCalculationModelInput,
                rollForwardServicingCosts,
                rollForwardEnrollmentStateTimeSeries,
                out PremiumCalculation premiumCalculation,
                out DataTable servicingCosts);

            Assert.AreEqual(90.79726075, premium, _precision);

            if (_outputExcel)
            {
                var excelFileWriter = PremiumCalculationTests.CreateExcelOutput(
                    rollForwardEnrollmentStateTimeSeries,
                    premiumCalculation,
                    servicingCosts,
                    analyticalOuput);

                excelFileWriter.ExportWorkbook();
            }
        }

        [TestMethod, Owner("Matthew Moore")]
        public void RollforwardRepricingModel_HalfYearWithPaidInPremiums_NumericalCalculation()
        {
            var analyticalOuput = false;
            var enrollmentModel = EnrollmentModelTests.PopulateEnrollmentModel(includePostGraduationTargets: true);
            var servicingCostsModel = ServicingCostsModelTests.PopulateServicingCostsModel();
            var rollForwardRepricingModel = new RollForwardRepricingModel(enrollmentModel, servicingCostsModel);

            var rollForwardEnrollmentStateTimeSeries = rollForwardRepricingModel.RollForwardEnrollmentStates(_numberOfPeriodsToRollForward_HalfYear);
            var rollForwardServicingCosts = rollForwardRepricingModel.RollForwardServicingCosts(_numberOfPeriodsToRollForward_HalfYear);

            var premiumCalculationModelInput = PremiumCalculationTests.PreparePremiumCalculationModelInput();
            premiumCalculationModelInput.PreviouslyPaidInPremiums = _previouslyPaidInPremiums;

            var premium = PremiumCalculationTests.CalculatePremiumNumerically(
                premiumCalculationModelInput,
                rollForwardServicingCosts,
                rollForwardEnrollmentStateTimeSeries,
                out PremiumCalculation premiumCalculation,
                out DataTable servicingCosts);

            Assert.AreEqual(59.45539042, premium, _precision);

            if (_outputExcel)
            {
                var excelFileWriter = PremiumCalculationTests.CreateExcelOutput(
                    rollForwardEnrollmentStateTimeSeries,
                    premiumCalculation,
                    servicingCosts,
                    analyticalOuput);

                excelFileWriter.ExportWorkbook();
            }
        }

        [TestMethod, Owner("Matthew Moore")]
        public void RollforwardRepricingModel_HalfYearWithPaidInPremiums_AnalyticalCalculation()
        {
            var analyticalOuput = true;
            var enrollmentModel = EnrollmentModelTests.PopulateEnrollmentModel(includePostGraduationTargets: true);
            var servicingCostsModel = ServicingCostsModelTests.PopulateServicingCostsModel();
            var rollForwardRepricingModel = new RollForwardRepricingModel(enrollmentModel, servicingCostsModel);

            var rollForwardEnrollmentStateTimeSeries = rollForwardRepricingModel.RollForwardEnrollmentStates(_numberOfPeriodsToRollForward_HalfYear);
            var rollForwardServicingCosts = rollForwardRepricingModel.RollForwardServicingCosts(_numberOfPeriodsToRollForward_HalfYear);

            var premiumCalculationModelInput = PremiumCalculationTests.PreparePremiumCalculationModelInput();
            premiumCalculationModelInput.PreviouslyPaidInPremiums = _previouslyPaidInPremiums;

            var premium = PremiumCalculationTests.CalculatePremiumAnalytically(
                premiumCalculationModelInput,
                rollForwardServicingCosts,
                rollForwardEnrollmentStateTimeSeries,
                out PremiumCalculation premiumCalculation,
                out DataTable servicingCosts);

            Assert.AreEqual(59.45539042, premium, _precision);

            if (_outputExcel)
            {
                var excelFileWriter = PremiumCalculationTests.CreateExcelOutput(
                    rollForwardEnrollmentStateTimeSeries,
                    premiumCalculation,
                    servicingCosts,
                    analyticalOuput);

                excelFileWriter.ExportWorkbook();
            }
        }

        [TestMethod, Owner("Matthew Moore")]
        public void RollforwardRepricingModel_FullYear_ComparisonTest()
        {
            var numericalPremium = RollforwardRepricingModel_FullYearWithPaidInPremiums_NumericalCalculation();
            var analyticalPremium = RollforwardRepricingModel_FullYearWithPaidInPremiums_AnalyticalCalculation();
            Assert.AreEqual(numericalPremium, analyticalPremium, _precision);
        }

        private double RollforwardRepricingModel_FullYearWithPaidInPremiums_NumericalCalculation()
        {
            var analyticalOuput = false;
            var enrollmentModel = EnrollmentModelTests.PopulateEnrollmentModel(includePostGraduationTargets: true);
            var servicingCostsModel = ServicingCostsModelTests.PopulateServicingCostsModel();
            var rollForwardRepricingModel = new RollForwardRepricingModel(enrollmentModel, servicingCostsModel);

            var rollForwardEnrollmentStateTimeSeries = rollForwardRepricingModel.RollForwardEnrollmentStates(_numberOfPeriodsToRollForward_FullYear);
            var rollForwardServicingCosts = rollForwardRepricingModel.RollForwardServicingCosts(_numberOfPeriodsToRollForward_FullYear);

            var premiumCalculationModelInput = PremiumCalculationTests.PreparePremiumCalculationModelInput();
            premiumCalculationModelInput.PreviouslyPaidInPremiums = _previouslyPaidInPremiums;

            var premium = PremiumCalculationTests.CalculatePremiumNumerically(
                premiumCalculationModelInput, 
                rollForwardServicingCosts, 
                rollForwardEnrollmentStateTimeSeries,
                out PremiumCalculation premiumCalculation, 
                out DataTable servicingCosts);

            Assert.AreEqual(67.93549053, premium, _precision);

            if (_outputExcel)
            {
                var excelFileWriter = PremiumCalculationTests.CreateExcelOutput(
                    rollForwardEnrollmentStateTimeSeries, 
                    premiumCalculation, 
                    servicingCosts, 
                    analyticalOuput);

                excelFileWriter.ExportWorkbook();
            }

            return premium;
        }

        private double RollforwardRepricingModel_FullYearWithPaidInPremiums_AnalyticalCalculation()
        {
            var analyticalOuput = true;
            var enrollmentModel = EnrollmentModelTests.PopulateEnrollmentModel(includePostGraduationTargets: true);
            var servicingCostsModel = ServicingCostsModelTests.PopulateServicingCostsModel();
            var rollForwardRepricingModel = new RollForwardRepricingModel(enrollmentModel, servicingCostsModel);

            var rollForwardEnrollmentStateTimeSeries = rollForwardRepricingModel.RollForwardEnrollmentStates(_numberOfPeriodsToRollForward_FullYear);
            var rollForwardServicingCosts = rollForwardRepricingModel.RollForwardServicingCosts(_numberOfPeriodsToRollForward_FullYear);

            var premiumCalculationModelInput = PremiumCalculationTests.PreparePremiumCalculationModelInput();
            premiumCalculationModelInput.PreviouslyPaidInPremiums = _previouslyPaidInPremiums;

            var premium = PremiumCalculationTests.CalculatePremiumAnalytically(
                premiumCalculationModelInput,
                rollForwardServicingCosts,
                rollForwardEnrollmentStateTimeSeries,
                out PremiumCalculation premiumCalculation,
                out DataTable servicingCosts);

            Assert.AreEqual(67.93549053, premium, _precision);

            if (_outputExcel)
            {
                var excelFileWriter = PremiumCalculationTests.CreateExcelOutput(
                    rollForwardEnrollmentStateTimeSeries,
                    premiumCalculation,
                    servicingCosts,
                    analyticalOuput);

                excelFileWriter.ExportWorkbook();
            }

            return premium;
        }
    }
}
