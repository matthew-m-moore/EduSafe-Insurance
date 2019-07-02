using System.Collections.Generic;
using EduSafe.IO.Database.Entities.Servicing;
using EduSafe.IO.Database.Entities.Servicing.Claims;
using EduSafe.IO.Database.Entities.Servicing.Individuals;

namespace EduSafe.Core.BusinessLogic.Containers
{
    public class IndividualServicingData
    {
        public InsureesAccountDataEntity IndividualAccountData { get; set; }

        public InsureesNextPaymentAndBalanceInformationEntity NextPaymentAndBalanceInformation { get; set; }

        public List<InsureesEnrollmentVerificationDetailsEntity> EnrollmentVerificationHistory { get; set; }
        public List<InsureesGraduationVerificationDetailsEntity> GraduationVerificationHistory { get; set; }

        public InsureesPremiumCalculationDetailsEntity PremiumCalculationDetails { get; set; }
        public List<InsureesPremiumCalculationOptionDetailsEntity> PremiumCalculationOptionDetails { get; set; }

        public int EmailSetId { get; set; }
        public List<EmailsEntity> Emails { get; set; }

        public List<InsureesMajorMinorDetailsEntity> MajorMinorDetails { get; set; }
        public List<InsureesNotificationHistoryEntryEntity> NotificationHistory { get; set; }
        public List<InsureesPaymentHistoryEntryEntity> PaymentHistory { get; set; }

        public List<long> ClaimNumbers { get; set; }
        public Dictionary<long, ClaimOptionEntryEntity> ClaimOptionsDictionary { get; set; }
        public Dictionary<long, ClaimStatusEntryEntity> ClaimStatusDictionary { get; set; }
        public Dictionary<long, List<ClaimDocumentEntryEntity>> ClaimDocumentsDictionary { get; set; }

        public List<ClaimPaymentEntryEntity> ClaimsPaymentHistory { get; set; }
    }
}
