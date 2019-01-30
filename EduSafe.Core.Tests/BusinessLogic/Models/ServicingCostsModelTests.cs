using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.CostsOrFees;
using EduSafe.Core.BusinessLogic.Models;
using EduSafe.Core.Tests.BusinessLogic.Models.StudentEnrollment;
using EduSafe.Core.BusinessLogic.Models.StudentEnrollment;
using EduSafe.IO.Excel;
using EduSafe.Common;

namespace EduSafe.Core.Tests.BusinessLogic.Models
{
    [TestClass]
    public class ServicingCostsModelTests
    {
        private bool _outputExcel = false;
        private double _precision = 1e-2;

        [TestMethod, Owner("Matthew Moore")]
        public void ServicingCostsModel_PeriodicAndEventsBasedCosts_WithPostGraduationTargets()
        {
            var studentEnrollmentModel = EnrollmentModelTests.PopulateEnrollmentModel(includePostGraduationTargets: true);
            studentEnrollmentModel.ParameterizeModel();

            var servicingCostsModel = PopulateServicingCostsModel();
            var enrollmentStateTimeSeries = studentEnrollmentModel.EnrollmentStateTimeSeries;
            var servicingCosts = servicingCostsModel.CalculateServicingCosts(enrollmentStateTimeSeries);
            CheckResults(servicingCosts);

            if (_outputExcel)
            {
                var excelFileWriter = CreateExcelOutput(studentEnrollmentModel, servicingCosts);
                excelFileWriter.ExportWorkbook();
            }
        }

        [TestMethod, Owner("Matthew Moore")]
        public void ServicingCostsModel_PeriodicAndEventsBasedCosts_WithoutPostGraduationTargets()
        {
            var studentEnrollmentModel = EnrollmentModelTests.PopulateEnrollmentModel(includePostGraduationTargets: false);
            studentEnrollmentModel.ParameterizeModel();

            var servicingCostsModel = PopulateServicingCostsModel();
            var enrollmentStateTimeSeries = studentEnrollmentModel.EnrollmentStateTimeSeries;
            var servicingCosts = servicingCostsModel.CalculateServicingCosts(enrollmentStateTimeSeries);
            CheckResults(servicingCosts);

            if (_outputExcel)
            {
                var excelFileWriter = CreateExcelOutput(studentEnrollmentModel, servicingCosts);
                excelFileWriter.ExportWorkbook();
            }
        }

        private void CheckResults(DataTable servicingCosts)
        {
            var initialServicingCosts = servicingCosts.Rows[0];
            var initialBackgroundCheck = initialServicingCosts.Field<double>("Background Check");
            Assert.AreEqual(100.00, initialBackgroundCheck, _precision);

            var initialCreditScore = initialServicingCosts.Field<double>("Credit Score");
            Assert.AreEqual(25.00, initialCreditScore, _precision);

            var initialTranscripts = initialServicingCosts.Field<double>("Transcripts");
            Assert.AreEqual(25.00, initialTranscripts, _precision);

            var initialServicing = initialServicingCosts.Field<double>("Servicing");
            Assert.AreEqual(20.00, initialServicing, _precision);

            var totalBackgroundCheck = servicingCosts.AsEnumerable().Sum(r => r.Field<double>("Background Check"));
            Assert.AreEqual(368.13, totalBackgroundCheck, _precision);

            var totalCreditScore = servicingCosts.AsEnumerable().Sum(r => r.Field<double>("Credit Score"));
            Assert.AreEqual(92.03, totalCreditScore, _precision);

            var totalTranscripts = servicingCosts.AsEnumerable().Sum(r => r.Field<double>("Transcripts"));
            Assert.AreEqual(92.03, totalTranscripts, _precision);

            var totalServicing = servicingCosts.AsEnumerable().Sum(r => r.Field<double>("Servicing"));
            Assert.AreEqual(838.60, totalServicing, _precision);

            var totalDropOutVerification = servicingCosts.AsEnumerable().Sum(r => r.Field<double>("Drop Out Verification"));
            Assert.AreEqual(60.00, totalDropOutVerification, _precision);

            var totalGradSchoolVerification = servicingCosts.AsEnumerable().Sum(r => r.Field<double>("Graduate School Verification"));
            Assert.AreEqual(22.50, totalGradSchoolVerification, _precision);

            var totalEarlyHireVerification = servicingCosts.AsEnumerable().Sum(r => r.Field<double>("Early Hire Verification"));
            Assert.AreEqual(15.00, totalEarlyHireVerification, _precision);

            var totalUnemploymentVerification = servicingCosts.AsEnumerable().Sum(r => r.Field<double>("Unemployment Verification"));
            Assert.AreEqual(60.00, totalUnemploymentVerification, _precision);

            var totalCostsAndFees = servicingCosts.AsEnumerable().Sum(r => r.Field<double>(Constants.TotalIdentifier));
            Assert.AreEqual(1548.30, totalCostsAndFees, _precision);
        }

        public static ExcelFileWriter CreateExcelOutput(EnrollmentModel studentEnrollmentModel, DataTable servicingCosts)
        {
            var excelFileWriter = EnrollmentModelTests.CreateExcelOutput(studentEnrollmentModel);
            excelFileWriter.AddWorksheetForDataTable(servicingCosts, "Servicing Costs");
            return excelFileWriter;
        }

        public static ServicingCostsModel PopulateServicingCostsModel()
        {
            var listOfCostsOrFees = new List<CostOrFee>();

            var backgroundCheckFee = new PeriodicCostOrFee(PaymentConvention.Annual, "Background Check", 100);
            var creditScoreFee = new PeriodicCostOrFee(PaymentConvention.Annual, "Credit Score", 25);
            var transcriptsFee = new PeriodicCostOrFee(PaymentConvention.Annual, "Transcripts", 25);
            var servicingFee = new PeriodicCostOrFee(PaymentConvention.Monthly, "Servicing", 20);

            var dropOutVerificationFee = new EventBasedCostOrFee(StudentEnrollmentState.DroppedOut, "Drop Out Verification", 150);
            var gradSchoolVerificationFee = new EventBasedCostOrFee(StudentEnrollmentState.GraduateSchool, "Graduate School Verification", 150);
            var earlyHireVerificationFee = new EventBasedCostOrFee(StudentEnrollmentState.EarlyHire, "Early Hire Verification", 300);
            var unemploymentVerificationFee = new EventBasedCostOrFee(StudentEnrollmentState.GraduatedUnemployed, "Unemployment Verification", 1000);

            listOfCostsOrFees.Add(backgroundCheckFee);
            listOfCostsOrFees.Add(creditScoreFee);
            listOfCostsOrFees.Add(transcriptsFee);
            listOfCostsOrFees.Add(servicingFee);

            listOfCostsOrFees.Add(dropOutVerificationFee);
            listOfCostsOrFees.Add(gradSchoolVerificationFee);
            listOfCostsOrFees.Add(earlyHireVerificationFee);
            listOfCostsOrFees.Add(unemploymentVerificationFee);

            return new ServicingCostsModel(listOfCostsOrFees, 72);
        }
    }
}
