using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using EduSafe.Core.Repositories.Database;
using EduSafe.WebApi.Models;

namespace EduSafe.WebApi.Controllers
{
    [RoutePrefix("api/authentication")]
    public class AuthenticationController : ApiController
    {
        // PUT: api/authentication
        [Route("")]
        [HttpPut]
        public bool AuthenticateCustomer(AuthenticationPackage authenticationPackage)
        {
            return !string.IsNullOrEmpty(authenticationPackage.EncryptedPassword);
        }

        // PUT: api/authentication/id
        [Route("id")]
        [HttpPut]
        public List<string> RetrieveCustomerNumbersFromUserIdentifier(string userIdentifier)
        {
            AuthenticationRepository authenticationRepository = null;
            var customerNumbers = new List<string>();

            if (userIdentifier.Contains('@'))
                authenticationRepository = new AuthenticationRepository(userIdentifier);            
            else if (long.TryParse(userIdentifier, out var customerNumber))
                authenticationRepository = new AuthenticationRepository(customerNumber);

            if (authenticationRepository != null)
                customerNumbers = authenticationRepository.AccountDataEntities.Select(e => e.AccountNumber.ToString()).ToList();

            return customerNumbers;
        }
    }
}