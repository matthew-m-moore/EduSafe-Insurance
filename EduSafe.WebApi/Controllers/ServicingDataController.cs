using System.Web.Http;
using EduSafe.WebApi.Adapters;
using EduSafe.WebApi.Models;

namespace EduSafe.WebApi.Controllers
{
    [RoutePrefix("api/servicing")]
    public class ServicingDataController : ApiController
    {
        // GET: api/servicing/institution/{customerIdentifier}
        [Route("institution/{customerIdentifier}")]
        [HttpGet]
        public InstitutionProfileEntry GetInstitutionProfile(string customerIdentifier)
        {
            var institutionServicingDataAdapter = new InstitutionServicingDataAdapter();
            if (long.TryParse(customerIdentifier, out var customerNumber))
            {
                var institutionProfileEntry = institutionServicingDataAdapter.GetInstitutionProfileData(customerNumber);
                return institutionProfileEntry;
            }

            return new InstitutionProfileEntry();
        }

        // GET: api/servicing/individual/{customerIdentifier}
        [Route("individual/{customerIdentifier}")]
        [HttpGet]
        public CustomerProfileEntry GetCustomerProfile(string customerIdentifier)
        {
            var individualServicingDataAdapter = new IndividualServicingDataAdapter();
            if (long.TryParse(customerIdentifier, out var customerNumber))
            {
                var customerProfileEntry = individualServicingDataAdapter.GetIndividualProfileData(customerNumber);
                return customerProfileEntry;
            }

            return new CustomerProfileEntry();
        }
    }
}