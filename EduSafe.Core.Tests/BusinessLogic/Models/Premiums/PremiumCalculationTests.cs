using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduSafe.Common.Curves;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Models;
using EduSafe.Core.BusinessLogic.Models.Premiums;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;
using EduSafe.Core.Tests.BusinessLogic.Models.StudentEnrollment;
using EduSafe.IO.Excel;

namespace EduSafe.Core.Tests.BusinessLogic.Models.Premiums
{
    [TestClass]
    public class PremiumCalculationTests
    {
        private bool _outputExcel = false;
        private double _precision = 1e-8;

        private PremiumCalculation _premiumCalculation;

        [TestMethod, Owner("Matthew Moore")]
        public void EnrollmentModel_WithPostgraduationTargets_NumericalPremiumSearch()
        {
            var analyticalOuput = false;
            var studentEnrollmentModel = EnrollmentModelTests.PopulateEnrollmentModel(includePostGraduationTargets: true);
            studentEnrollmentModel.ParameterizeModel();

            var servicingCostsModel = ServicingCostsModelTests.PopulateServicingCostsModel();
            var enrollmentStateTimeSeries = studentEnrollmentModel.EnrollmentStateTimeSeries;
            var premiumCalculationModelInput = PreparePremiumCalculationModelInput();
            var premium = CalculatePremiumNumerically(premiumCalculationModelInput, servicingCostsModel, enrollmentStateTimeSeries,
                out DataTable servicingCosts);

            Assert.AreEqual(82.35792486, premium, _precision);

            if (_outputExcel)
            {
                var excelFileWriter = CreateExcelOutput(studentEnrollmentModel, _premiumCalculation, servicingCosts, analyticalOuput);
                excelFileWriter.ExportWorkbook();
            }
        }

        [TestMethod, Owner("Matthew Moore")]
        public void EnrollmentModel_WithoutPostgraduationTargets_NumericalPremiumSearch()
        {
            var analyticalOuput = false;
            var studentEnrollmentModel = EnrollmentModelTests.PopulateEnrollmentModel(includePostGraduationTargets: false);
            studentEnrollmentModel.ParameterizeModel();

            var servicingCostsModel = ServicingCostsModelTests.PopulateServicingCostsModel();
            var enrollmentStateTimeSeries = studentEnrollmentModel.EnrollmentStateTimeSeries;
            var premiumCalculationModelInput = PreparePremiumCalculationModelInput();
            var premium = CalculatePremiumNumerically(premiumCalculationModelInput, servicingCostsModel, enrollmentStateTimeSeries,
                out DataTable servicingCosts);

            Assert.AreEqual(82.35792486, premium, _precision);

            if (_outputExcel)
            {
                var excelFileWriter = CreateExcelOutput(studentEnrollmentModel, _premiumCalculation, servicingCosts, analyticalOuput);
                excelFileWriter.ExportWorkbook();
            }
        }

        [TestMethod, Owner("Matthew Moore")]
        public void EnrollmentModel_WithPostgraduationTargets_AnalyticalPremiumCalculation()
        {
            var analyticalOuput = true;
            var studentEnrollmentModel = EnrollmentModelTests.PopulateEnrollmentModel(includePostGraduationTargets: true);
            studentEnrollmentModel.ParameterizeModel();

            var servicingCostsModel = ServicingCostsModelTests.PopulateServicingCostsModel();
            var enrollmentStateTimeSeries = studentEnrollmentModel.EnrollmentStateTimeSeries;
            var premiumCalculationModelInput = PreparePremiumCalculationModelInput();
            var premium = CalculatePremiumAnalytically(premiumCalculationModelInput, servicingCostsModel, enrollmentStateTimeSeries,
                out DataTable servicingCosts);

            Assert.AreEqual(82.35792486, premium, _precision);

            if (_outputExcel)
            {
                var excelFileWriter = CreateExcelOutput(studentEnrollmentModel, _premiumCalculation, servicingCosts, analyticalOuput);
                excelFileWriter.ExportWorkbook();
            }
        }

        [TestMethod, Owner("Matthew Moore")]
        public void EnrollmentModel_WithoutPostgraduationTargets_AnalyticalPremiumCalculation()
        {
            var analyticalOuput = true;
            var studentEnrollmentModel = EnrollmentModelTests.PopulateEnrollmentModel(includePostGraduationTargets: false);
            studentEnrollmentModel.ParameterizeModel();

            var servicingCostsModel = ServicingCostsModelTests.PopulateServicingCostsModel();
            var enrollmentStateTimeSeries = studentEnrollmentModel.EnrollmentStateTimeSeries;
            var premiumCalculationModelInput = PreparePremiumCalculationModelInput();
            var premium = CalculatePremiumAnalytically(premiumCalculationModelInput, servicingCostsModel, enrollmentStateTimeSeries,
                out DataTable servicingCosts);

            Assert.AreEqual(82.35792486, premium, _precision);

            if (_outputExcel)
            {
                var excelFileWriter = CreateExcelOutput(studentEnrollmentModel, _premiumCalculation, servicingCosts, analyticalOuput);
                excelFileWriter.ExportWorkbook();
            }
        }

        private void CheckResults(EnrollmentModel studentEnrollmentModel, double premium)
        {
            Assert.AreEqual(82.35792486, premium, _precision);
        }

        public static ExcelFileWriter CreateExcelOutput(
            EnrollmentModel studentEnrollmentModel,
            PremiumCalculation premiumCalculation,
            DataTable servicingCosts,
            bool analyticalOutput)
        {
            var excelFileWriter = ServicingCostsModelTests.CreateExcelOutput(studentEnrollmentModel, servicingCosts);

            if (analyticalOutput)
            {
                var analyticalPremiumCalculationCashFlows =
                    premiumCalculation.CalculatedCashFlows.Select(c => (AnalyticalPremiumCalculationCashFlow)c).ToList();

                excelFileWriter.AddWorksheetForListOfData(analyticalPremiumCalculationCashFlows, "Cash Flows");
            }
            else
            {
                excelFileWriter.AddWorksheetForListOfData(premiumCalculation.CalculatedCashFlows, "Cash Flows");
            }

            return excelFileWriter;
        }

        public PremiumCalculationModelInput PreparePremiumCalculationModelInput()
        {
            var discountFactorCurve = new InterestRateCurve(
                InterestRateCurveType.Treasury1Mo,
                new DateTime(2018, 12, 19),
                new DataCurve<double>(0.0235), 1,
                DayCountConvention.Thirty360);

            var annualIncomeCoverage = 50000;
            var monthsOfIncomeCoverage = 6;
            var dropOutCoverageOption = 0.25;
            var gradSchoolCoverageOption = 0.25;
            var earlyHireCoverageOption = 0.25;

            var premiumCalculationModelInput =
                new PremiumCalculationModelInput(
                    annualIncomeCoverage,
                    monthsOfIncomeCoverage,
                    discountFactorCurve,
                    dropOutCoverageOption,
                    gradSchoolCoverageOption,
                    earlyHireCoverageOption);

            return premiumCalculationModelInput;
        }

        private double CalculatePremiumNumerically(
            PremiumCalculationModelInput premiumCalculationModelInput,
            ServicingCostsModel servicingCostsModel,
            List<EnrollmentStateArray> enrollmentStateTimeSeries,
            out DataTable servicingCosts)
        {
            var premiumCalculation = new NumericalPremiumCalculation(premiumCalculationModelInput);
            var premium = premiumCalculation.CalculatePremium(enrollmentStateTimeSeries, servicingCostsModel);

            servicingCosts = premiumCalculation.ServicingCostsDataTable;
            return premium;
        }

        private double CalculatePremiumAnalytically(
            PremiumCalculationModelInput premiumCalculationModelInput,
            ServicingCostsModel servicingCostsModel,
            List<EnrollmentStateArray> enrollmentStateTimeSeries,
            out DataTable servicingCosts)
        {
            var premiumCalculation = new AnalyticalPremiumCalculation(premiumCalculationModelInput);
            var premium = premiumCalculation.CalculatePremium(enrollmentStateTimeSeries, servicingCostsModel);

            servicingCosts = premiumCalculation.ServicingCostsDataTable;
            return premium;
        }
    }
}
