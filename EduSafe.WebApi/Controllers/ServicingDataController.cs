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
        // GET: api/servicing/institution/{customerIdentifier}
        [Route("institution/{customerIdentifier}")]
        [HttpGet]
        public InstitutionProfileEntry GetInstitutionProfile(string customerIdentifier)
        {   
            return new InstitutionProfileEntry();
        }

        // GET: api/servicing/individual/{customerIdentifier}
        [Route("individual/{customerIdentifier}")]
        [HttpGet]
        public CustomerProfileEntry GetCustomerProfile(string customerIdentifier)
        {
            return new CustomerProfileEntry();
        }
    }
}