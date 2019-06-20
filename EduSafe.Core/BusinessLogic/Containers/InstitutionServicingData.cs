using System.Collections.Generic;
using EduSafe.IO.Database.Entities.Servicing;
using EduSafe.IO.Database.Entities.Servicing.Institutions;

namespace EduSafe.Core.BusinessLogic.Containers
{
    public class InstitutionServicingData
    {
        public InstitutionsAccountDataEntity InstitutionAccountData { get; set; }

        public InstitutionsNextPaymentAndBalanceInformationEntity NextPaymentAndBalanceInformation { get; set; }

        public List<InstitutionsNotificationHistoryEntryEntity> NotificationHistory { get; set; }
        public List<InstitutionsPaymentHistoryEntryEntity> PaymentHistory { get; set; }

        public List<EmailsEntity> Emails { get; set; }

        public List<IndividualServicingData> IndividualInsureesServicingData { get; set; }

        public InstitutionServicingData()
        {
            IndividualInsureesServicingData = new List<IndividualServicingData>();
        }
    }
}
