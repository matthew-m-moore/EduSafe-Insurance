using System.Collections.Generic;
using System.Linq;
using EduSafe.Core.Reporting;
using EduSafe.IO.Excel;
using EduSafe.IO.Excel.Records;
using EduSafe.WebApi.Models.Servicing;

namespace EduSafe.WebApi.Adapters
{
    internal class ExcelExportAdapter
    {
        internal static ExcelFileWriter CreateStudentInformationReport(InstitutionProfileEntry institutionProfileEntry)
        {
            var studentInformationRecords = institutionProfileEntry
                .CustomerProfileEntries.Select(ConvertToStudentInformationRecord).ToList();

            var excelFileWriter = new ExcelFileWriter();
            StudentInformationReport.AddReportTab(excelFileWriter.ExcelWorkbook, studentInformationRecords);

            return excelFileWriter;
        }

        internal static ExcelFileWriter CreatePaymentHistoryReport(List<PaymentHistoryEntry> paymentHistoryEntries)
        {
            var paymentHistoryRecords = paymentHistoryEntries.Select(ConvertToPaymentHistoryRecord).ToList();

            var excelFileWriter = new ExcelFileWriter();
            PaymentHistoryReport.AddReportTab(excelFileWriter.ExcelWorkbook, paymentHistoryRecords);

            return excelFileWriter;
        }       

        private static StudentInformationRecord ConvertToStudentInformationRecord(CustomerProfileEntry customerProfileEntry)
        {
            return new StudentInformationRecord
            {
                StudentSchoolId = "XX123ABC",
                StudentPolicyId = customerProfileEntry.CustomerIdNumber,
                StudentName = customerProfileEntry.CustomerName,
                StudentMajor = customerProfileEntry.CollegeMajor,

                CollegeStartDate = customerProfileEntry.CollegeStartDate,
                CollegeGraduationDate = customerProfileEntry.ExpectedGraduationDate,

                MonthlyPayment = customerProfileEntry.MonthlyPaymentAmount,
                TotalPaidInPremiums = customerProfileEntry.TotalPaidInPremiums,
                TotalCoverage = customerProfileEntry.TotalCoverageAmount,
                RemainingCoverage = customerProfileEntry.RemainingCoverageAmount,

                IsEnrolled = customerProfileEntry.EnrollmentVerified,
                HasGraduated = customerProfileEntry.GraduationVerified,
                HasClaims = customerProfileEntry.HasClaims,
            };
        }

        private static PaymentHistoryRecord ConvertToPaymentHistoryRecord(PaymentHistoryEntry paymentHistoryEntry)
        {
            return new PaymentHistoryRecord
            {
                PaymentDate = paymentHistoryEntry.PaymentDate,
                PaymentAmount = paymentHistoryEntry.PaymentAmount,
                PaymentStatus = paymentHistoryEntry.PaymentStatus,
                PaymentComments = paymentHistoryEntry.PaymentComments,
            };
        }
    }
}