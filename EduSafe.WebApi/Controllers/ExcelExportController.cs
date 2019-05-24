using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using EduSafe.WebApi.Models;

namespace EduSafe.WebApi.Controllers
{
    [RoutePrefix("api/export")]
    public class ExcelExportController : ApiController
    {
        // GET: api/export/students
        [Route("students")]
        [HttpGet]
        public void ExportCustomerProfiles(List<CustomerProfileEntry> customerProfileEntries)
        {

        }

        // GET: api/export/payments
        [Route("payments")]
        [HttpGet]
        public void ExportPaymentHistory(List<PaymentHistoryEntry> paymentHistoryEntries)
        {

        }
    }
}