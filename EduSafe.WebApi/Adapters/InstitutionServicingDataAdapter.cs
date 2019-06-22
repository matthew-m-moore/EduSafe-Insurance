using System.Collections.Generic;
using System.Linq;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.Repositories.Database;
using EduSafe.IO.Database;
using EduSafe.WebApi.Models;

namespace EduSafe.WebApi.Adapters
{
    public class InstitutionServicingDataAdapter
    {
        private readonly ServicingDataTypesRepository _servicingDataTypesRepository;
        private readonly InstitutionServicingDataRepository _institutionServicingDataRepository;
        private readonly IndividualServicingDataAdapter _individualServicingDataAdapter;

        public InstitutionServicingDataAdapter()
        {
            var databaseContext = DatabaseContextRetriever.GetServicingDataContext();

            _servicingDataTypesRepository = new ServicingDataTypesRepository(databaseContext);
            _institutionServicingDataRepository = new InstitutionServicingDataRepository(databaseContext);
            _individualServicingDataAdapter = new IndividualServicingDataAdapter();
        }

        public InstitutionProfileEntry GetInstitutionProfileData(long institutionAccountNumber)
        {
            var institutionServicingData = _institutionServicingDataRepository.GetInstitutionServicingData(institutionAccountNumber);
           
            var paymentHistory = GetPaymentHistory(institutionServicingData);
            var notificationHistory = GetNotificationHistory(institutionServicingData);
            var customerProfileEntries = GetCustomerProfileEntries(institutionServicingData);

            var customerProfileEntry = new InstitutionProfileEntry
            {
                CustomerIdNumber = institutionServicingData.InstitutionAccountData.InstitutionsAccountNumber,
                CustomerUniqueId = institutionServicingData.InstitutionAccountData.FolderPath,

                CustomerName = institutionServicingData.InstitutionAccountData.InstitutionName,
                CustomerAddress1 = institutionServicingData.InstitutionAccountData.Address1,
                CustomerAddress2 = institutionServicingData.InstitutionAccountData.Address2,
                CustomerAddress3 = institutionServicingData.InstitutionAccountData.Address3,
                CustomerCity = institutionServicingData.InstitutionAccountData.City,
                CustomerState = institutionServicingData.InstitutionAccountData.State,
                CustomerZip = institutionServicingData.InstitutionAccountData.ZipCode,
                CustomerEmails = institutionServicingData.Emails.Select(e => e.Email).ToList(),

                CustomerBalance = institutionServicingData.NextPaymentAndBalanceInformation.CurrentBalance,
                MonthlyPaymentAmount = institutionServicingData.NextPaymentAndBalanceInformation.NextPaymentAmount,
                NextPaymentDueDate = institutionServicingData.NextPaymentAndBalanceInformation.NextPaymentDate,

                PaymentHistoryEntries = paymentHistory,
                NotificationHistoryEntries = notificationHistory,
                CustomerProfileEntries = customerProfileEntries,
            };

            return customerProfileEntry;
        }

        private List<PaymentHistoryEntry> GetPaymentHistory(InstitutionServicingData institutionServicingData)
        {
            var paymentHistoryEntries = new List<PaymentHistoryEntry>();

            foreach (var paymentHistoryEntity in institutionServicingData.PaymentHistory)
            {
                var paymentStatusType = _servicingDataTypesRepository
                    .PaymentStatusTypeDictionary[paymentHistoryEntity.PaymentStatusTypeId];

                var paymentHistoryEntry = new PaymentHistoryEntry
                {
                    PaymentAmount = paymentHistoryEntity.PaymentAmount,
                    PaymentDate = paymentHistoryEntity.PaymentDate,
                    PaymentStatus = paymentStatusType.ToString(),
                    PaymentComments = paymentHistoryEntity.PaymentComments,
                };

                paymentHistoryEntries.Add(paymentHistoryEntry);
            }

            return paymentHistoryEntries;
        }

        private List<NotificationHistoryEntry> GetNotificationHistory(InstitutionServicingData institutionServicingData)
        {
            var notificationHistoryEntries = new List<NotificationHistoryEntry>();

            foreach (var notificationHistoryEntity in institutionServicingData.NotificationHistory)
            {
                var notificationTypeTuple = _servicingDataTypesRepository
                    .NotificationTypeTupleDictionary[notificationHistoryEntity.NotificationTypeId];

                var notificationHistoryEntry = new NotificationHistoryEntry
                {
                    NotificationType = notificationTypeTuple.NotificationType.ToString(),
                    NotificationDate = notificationHistoryEntity.NotificationDate,
                    NotificationDescription = notificationTypeTuple.NotificationDescription
                };

                notificationHistoryEntries.Add(notificationHistoryEntry);
            }

            return notificationHistoryEntries;
        }

        private List<CustomerProfileEntry> GetCustomerProfileEntries(InstitutionServicingData institutionServicingData)
        {
            var customerProfileEntries = new List<CustomerProfileEntry>();
            foreach (var individualAccountNumber in institutionServicingData.IndividualInsureeAccountNumbers)
            {
                var customerProfileEntry = 
                    _individualServicingDataAdapter.GetIndividualProfileData(individualAccountNumber);

                customerProfileEntries.Add(customerProfileEntry);
            }

            return customerProfileEntries;
        }
    }
}