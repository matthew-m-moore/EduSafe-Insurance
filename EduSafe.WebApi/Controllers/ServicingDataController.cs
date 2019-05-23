using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using EduSafe.WebApi.Models;

namespace EduSafe.WebApi.Controllers
{
    [RoutePrefix("api/servicing")]
    public class ServicingDataController : ApiController
    {
        // GET: api/servicing/{customerIdentifier}/{fileName}
        [Route("{customerIdentifier}")]
        public InstitutionProfileEntry GetInstitutionProfile(string customerIdentifier)
        {
            return new InstitutionProfileEntry();
        }

        // GET: api/servicing/{customerIdentifier}/{fileName}
        [Route("{customerIdentifier}")]
        public CustomerProfileEntry GetCustomerProfile(string customerIdentifier)
        {
            return new CustomerProfileEntry();
        }
    }
}