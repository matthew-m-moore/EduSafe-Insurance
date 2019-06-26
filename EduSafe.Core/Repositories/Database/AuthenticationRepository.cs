using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EduSafe.Common;
using EduSafe.IO.Database;
using EduSafe.IO.Database.Contexts;
using EduSafe.IO.Interfaces;

namespace EduSafe.Core.Repositories.Database
{
    public class AuthenticationRepository : DatabaseRepository
    {
        public override DbContext DatabaseContext => DatabaseContextRetriever.GetServicingDataContext();

        public List<IAccountData> AccountDataEntities { get; private set; }

        public AuthenticationRepository(long customerNumber)
        {
            AccountDataEntities = new List<IAccountData>();

            var accountData = RetrieveAccountDataFromCustomerNumber(customerNumber);
            if (accountData != null) AccountDataEntities.Add(accountData);
        }
        
        public AuthenticationRepository(string emailAddress)
        {
            AccountDataEntities = new List<IAccountData>();

            var accountDataEntities = RetrieveAccountDataFromEmail(emailAddress);
            if (accountDataEntities.Any()) AccountDataEntities.AddRange(accountDataEntities);
        }

        private IAccountData RetrieveAccountDataFromCustomerNumber(long customerNumber)
        {
            var customerNumberAsString = customerNumber.ToString();
            var customerNumberLength = customerNumberAsString.Length;

            IAccountData accountData;
            using (var servicingDataContext = DatabaseContext as ServicingDataContext)
            {
                if (customerNumberLength < Constants.IndividalAccountNumberLength)
                {
                    accountData = servicingDataContext
                        .InstitutionsAccountDataEntities
                        .SingleOrDefault(e => e.InstitutionsAccountNumber == customerNumber);
                }
                else
                {
                    accountData = servicingDataContext
                        .InsureesAccountDataEntities
                        .SingleOrDefault(e => e.AccountNumber == customerNumber);
                }
            }

            return accountData;
        }

        private List<IAccountData> RetrieveAccountDataFromEmail(string emailAddress)
        {
            var accountDataEntities = new List<IAccountData>();

            using (var servicingDataContext = DatabaseContext as ServicingDataContext)
            {
                var distinctEmailSetIds = servicingDataContext
                    .EmailsEntities.Where(e => e.Email == emailAddress && e.IsPrimary)
                    .Select(a => a.EmailsSetId).Distinct().ToList();

                var institutionalAccountData = servicingDataContext
                    .InstitutionsAccountDataEntities
                    .Where(e => distinctEmailSetIds.Contains(e.EmailsSetId)).ToList();

                var individualAccountData = servicingDataContext
                    .InsureesAccountDataEntities
                    .Where(e => distinctEmailSetIds.Contains(e.EmailsSetId)).ToList();

                accountDataEntities.AddRange(institutionalAccountData);
                accountDataEntities.AddRange(individualAccountData);
            }

            return accountDataEntities;
        }
    }
}
