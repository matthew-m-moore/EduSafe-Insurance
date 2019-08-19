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
        // PUT: api/authentication/login
        [Route("login")]
        [HttpPut]
        public bool AuthenticateCustomer(AuthenticationPackage authenticationPackage)
        {
            return false;
            return !string.IsNullOrEmpty(authenticationPackage.EncryptedPassword);
        }

        // PUT: api/authentication/user
        [Route("user")]
        [HttpPost]
        public List<string> RetrieveCustomerNumbers(AuthenticationPackage authenticationPackage)
        {
            return null;
            AuthenticationRepository authenticationRepository = null;
            var customerNumbers = new List<string>();
            var userIdentifier = authenticationPackage.CustomerIdentifier;

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