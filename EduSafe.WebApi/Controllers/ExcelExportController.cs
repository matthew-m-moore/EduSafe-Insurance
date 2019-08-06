using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using EduSafe.Common;
using EduSafe.IO.Excel;
using EduSafe.IO.Files;
using EduSafe.WebApi.Adapters;
using EduSafe.WebApi.Interfaces;
using EduSafe.WebApi.Models.Servicing;

namespace EduSafe.WebApi.Controllers
{
    [RoutePrefix("api/export")]
    public class ExcelExportController : ApiController
    {
        private const string _studentInformationReportNameStub = "-Student-Details-";
        private const string _paymentHistoryReportNameStub = "-Payment-History-";

        // POST: api/export/students
        [Route("students")]
        [HttpPost]
        public HttpResponseMessage ExportCustomerProfiles(InstitutionProfileEntry institutionProfileEntry)
        {
            var fileName = ConstructFileName(institutionProfileEntry, _studentInformationReportNameStub);
            var excelFileWriter = ExcelExportAdapter.CreateStudentInformationReport(institutionProfileEntry);

            return CreateHttpResponseMessage(institutionProfileEntry, excelFileWriter, fileName);
        }

        // POST: api/export/payments-institution
        [Route("payments-institution")]
        [HttpPost]
        public HttpResponseMessage ExportPaymentHistory(InstitutionProfileEntry institutionProfileEntry)
        {
            var fileName = ConstructFileName(institutionProfileEntry, _paymentHistoryReportNameStub);
            var paymentHistoryEntries = institutionProfileEntry.PaymentHistoryEntries;
            var excelFileWriter = ExcelExportAdapter.CreatePaymentHistoryReport(paymentHistoryEntries);

            return CreateHttpResponseMessage(institutionProfileEntry, excelFileWriter, fileName);
        }

        // POST: api/export/payments-individual
        [Route("payments-individual")]
        [HttpPost]
        public HttpResponseMessage ExportPaymentHistory(CustomerProfileEntry customerProfileEntry)
        {
            var fileName = ConstructFileName(customerProfileEntry, _paymentHistoryReportNameStub);
            var paymentHistoryEntries = customerProfileEntry.PaymentHistoryEntries;
            var excelFileWriter = ExcelExportAdapter.CreatePaymentHistoryReport(paymentHistoryEntries);

            return CreateHttpResponseMessage(customerProfileEntry, excelFileWriter, fileName);
        }

        private static string ConstructFileName(IProfileEntry profileEntry, string reportNameStub)
        {
            var timeStamp = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss");
            var fileName = profileEntry.CustomerIdNumber.ToString()
                + reportNameStub
                + timeStamp
                + Constants.ExcelFileExtension;

            return fileName;
        }

        private static HttpResponseMessage CreateHttpResponseMessage(
            IProfileEntry profileEntry, 
            ExcelFileWriter excelFileWriter, 
            string fileName)
        {
            using (var memoryStream = excelFileWriter.ExportWorkbookToMemoryStream())
            {
                var targetFilePath = Path.Combine(
                    FileServerSettings.InstitutionalCustomersDirectory,
                    profileEntry.CustomerUniqueId,
                    FileServerSettings.ReportsDirectory);

                // This saves the file to the customer's folder in the file share
                var fileServerUtility = new FileServerUtility(FileServerSettings.FileShareName);
                fileServerUtility.UploadFileFromStream(targetFilePath, fileName, memoryStream);

                var response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ByteArrayContent(memoryStream.ToArray())
                };

                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };

                // This allows you to access the file name, even with CORS in play
                response.Content.Headers.Add("FileName", fileName);
                response.Content.Headers.Add("Access-Control-Expose-Headers", "FileName");
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(Constants.ExcelFileHttpContentType);
                response.Content.Headers.ContentLength = memoryStream.Length;

                return response;
            }
        }
    }
}