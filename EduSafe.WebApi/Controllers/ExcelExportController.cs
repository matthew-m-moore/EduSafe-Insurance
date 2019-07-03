using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using ClosedXML.Excel;
using EduSafe.WebApi.Models;

namespace EduSafe.WebApi.Controllers
{
    [RoutePrefix("api/export")]
    public class ExcelExportController : ApiController
    {
        const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        // GET: api/export/students
        [Route("students")]
        [HttpPost]
        public HttpResponseMessage ExportCustomerProfiles(InstitutionProfileEntry institutionProfileEntry)
        {
            var dataTable = new DataTable();
            var fileName = "example.xlsx";

            var workbook = new XLWorkbook();
            workbook.Worksheets.Add(dataTable);
            workbook.SaveAs(fileName);

            using (var memoryStream = new MemoryStream())
            {
                var response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ByteArrayContent(memoryStream.ToArray())
                };

                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };

                response.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                //response.Content.Headers.ContentLength = ;

                return response;
            }            
        }

        // GET: api/export/payments
        [Route("payments")]
        [HttpPost]
        public HttpResponseMessage ExportPaymentHistory(List<PaymentHistoryEntry> paymentHistoryEntries)
        {
            var dataTable = new DataTable();
            var fileName = "example.xlsx";

            var workbook = new XLWorkbook();
            workbook.Worksheets.Add(dataTable);
            workbook.SaveAs(fileName);

            using (var memoryStream = new MemoryStream())
            {
                var response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ByteArrayContent(memoryStream.ToArray())
                };

                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };

                response.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                //response.Content.Headers.ContentLength = ;

                return response;
            }
        }
    }
}