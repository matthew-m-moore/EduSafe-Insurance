using System.Data.Entity;
using System.Linq;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.IO.Database;
using EduSafe.IO.Database.Contexts;

namespace EduSafe.Core.Repositories.Database
{
    public class InstitutionServicingDataRepository : DatabaseRepository
    {
        private readonly DbContext _databaseContext;
        public override DbContext DatabaseContext => _databaseContext ?? DatabaseContextRetriever.GetServicingDataContext();

        public InstitutionServicingDataRepository(DbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public InstitutionServicingData GetInstitutionServicingData(long institutionAccountNumber)
        {
            var institutionServicingData = new InstitutionServicingData();

            using (var servicingDataContext = DatabaseContext as ServicingDataContext)
            {
                GetBasicAccountData(institutionAccountNumber, institutionServicingData, servicingDataContext);
                GetNotificationAndPaymentHistory(institutionAccountNumber, institutionServicingData, servicingDataContext);
                GetIndividualInsureeAccountNumbers(institutionAccountNumber, institutionServicingData, servicingDataContext);
            }
            
            return institutionServicingData;
        }

        private void GetBasicAccountData(
            long institutionAccountNumber,
            InstitutionServicingData institutionServicingData,
            ServicingDataContext servicingDataContext)
        {
            var institutionCustomerDataEntity = servicingDataContext.InstitutionsAccountDataEntities
                .SingleOrDefault(e => e.InstitutionsAccountNumber == institutionAccountNumber);

            var nextPaymentAndBalanceEntity = servicingDataContext
                .InstitutionsNextPaymentAndBalanceInformationEntities
                .Where(e => e.InstitutionsAccountNumber == institutionAccountNumber)
                .OrderBy(i => i.Id).LastOrDefault();

            var emailsSetId = institutionCustomerDataEntity.EmailsSetId;
            var emailEntities = servicingDataContext.EmailsEntities
                .Where(e => e.EmailsSetId == emailsSetId).ToList();

            institutionServicingData.InstitutionAccountData = institutionCustomerDataEntity;
            institutionServicingData.NextPaymentAndBalanceInformation = nextPaymentAndBalanceEntity;

            institutionServicingData.Emails = emailEntities;
        }

        private void GetNotificationAndPaymentHistory(
            long institutionAccountNumber,
            InstitutionServicingData institutionServicingData,
            ServicingDataContext servicingDataContext)
        {
            var notificationHistoryEntities = servicingDataContext.InstitutionsNotificationHistoryEntryEntities
                .Where(e => e.InstitutionsAccountNumber == institutionAccountNumber).ToList();

            var paymentHistoryEntities = servicingDataContext.InstitutionsPaymentHistoryEntryEntities
                .Where(e => e.InstitutionsAccountNumber == institutionAccountNumber).ToList();

            institutionServicingData.NotificationHistory = notificationHistoryEntities;
            institutionServicingData.PaymentHistory = paymentHistoryEntities;
        }

        private void GetIndividualInsureeAccountNumbers(
            long institutionAccountNumber, 
            InstitutionServicingData institutionServicingData, 
            ServicingDataContext servicingDataContext)
        {
            var individualAccountNumbers = servicingDataContext.InstitutionsInsureeListEntities
                .Where(e => e.InstitutionsAccountNumber == institutionAccountNumber)
                .Select(i => i.InsureeAccountNumber).ToList();

            institutionServicingData.IndividualInsureeAccountNumbers = individualAccountNumbers;
        }
    }
}
