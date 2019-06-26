using System.Collections.Generic;
using System.Linq;
using EduSafe.Core.Repositories.Database;

namespace EduSafe.WebApi.Adapters
{
    public class AuthenticationAdapater
    {
        private AuthenticationRepository _authenicationRepository;

        public AuthenticationAdapater(long customerNumber)
        {
            _authenicationRepository = new AuthenticationRepository(customerNumber);
        }

        public AuthenticationAdapater(string emailAddress)
        {
            _authenicationRepository = new AuthenticationRepository(emailAddress);
        }

        public List<long> GetAllCustomerNumbers() => 
            _authenicationRepository.AccountDataEntities.Select(e => e.AccountNumber).ToList();

        public long GetCustomerNumberFromIdentifer(string customerIdentifier)
        {
            var accountData = _authenicationRepository
                .AccountDataEntities.SingleOrDefault(e => e.FolderPath == customerIdentifier);

            return (accountData != null)
                ? accountData.AccountNumber
                : default;
        }

        public string GetCustomerIdentifierFromNumber(long customerNumber)
        {
            var accountData = _authenicationRepository
                .AccountDataEntities.SingleOrDefault(e => e.AccountNumber == customerNumber);

            return (accountData != null)
                ? accountData.FolderPath
                : default;
        }
    }
}