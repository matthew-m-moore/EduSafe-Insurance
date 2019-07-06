using System.Collections.Generic;
using System.Linq;
using EduSafe.Core.Repositories.Database;

namespace EduSafe.WebApi.Adapters
{
    internal class AuthenticationAdapater
    {
        private AuthenticationRepository _authenicationRepository;

        internal AuthenticationAdapater(long customerNumber)
        {
            _authenicationRepository = new AuthenticationRepository(customerNumber);
        }

        internal AuthenticationAdapater(string emailAddress)
        {
            _authenicationRepository = new AuthenticationRepository(emailAddress);
        }

        internal List<long> GetAllCustomerNumbers() => 
            _authenicationRepository.AccountDataEntities.Select(e => e.AccountNumber).ToList();

        internal long GetCustomerNumberFromIdentifer(string customerIdentifier)
        {
            var accountData = _authenicationRepository
                .AccountDataEntities.SingleOrDefault(e => e.FolderPath == customerIdentifier);

            return (accountData != null)
                ? accountData.AccountNumber
                : default;
        }

        internal string GetCustomerIdentifierFromNumber(long customerNumber)
        {
            var accountData = _authenicationRepository
                .AccountDataEntities.SingleOrDefault(e => e.AccountNumber == customerNumber);

            return (accountData != null)
                ? accountData.FolderPath
                : default;
        }
    }
}