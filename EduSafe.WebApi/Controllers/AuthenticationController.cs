using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using EduSafe.WebApi.Models;

namespace EduSafe.WebApi.Controllers
{
    [RoutePrefix("api/authentication")]
    public class AuthenticationController : ApiController
    {
        // Note: Send the customerNumber or customerEmail via a header, not in the URL

        // GET: api/authentication/id
        [Route("id")]
        [HttpGet]
        public string GetCustomerIdentifierFromNumber(string customerNumber)
        {
            return RetrieveCustomerIdentifer(customerNumber);
        }

        // GET: api/authentication/email
        [Route("email")]
        [HttpGet]
        public List<string> GetCustomerIdentifiersFromEmail(string customerEmail)
        {
            var customerNumbers = RetrieveCustomerNumbers(customerEmail);
            return customerNumbers.Select(RetrieveCustomerIdentifer).ToList();
        }

        private static List<string> RetrieveCustomerNumbers(string customerEmail)
        {
            throw new NotImplementedException();
        }

        private static string RetrieveCustomerIdentifer(string customerNumber)
        {
            if (customerNumber.Length < 10)
            {
                // Retrieve institution customer identifier
                return string.Empty;
            }
            else
            {
                // Retrieve individual customer identifier
                return string.Empty;
            }
        }
    }
}