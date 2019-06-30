using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.IO.Database;
using EduSafe.IO.Database.Contexts;
using EduSafe.IO.Database.Entities.Servicing.Claims;
using EduSafe.IO.Database.Entities.Servicing.Individuals;

namespace EduSafe.Core.Repositories.Database
{
    public class IndividualServicingDataRepository : DatabaseRepository
    {
        private readonly DbContext _databaseContext;
        public override DbContext DatabaseContext => _databaseContext ?? DatabaseContextRetriever.GetServicingDataContext();

        public IndividualServicingDataRepository(DbContext databaseContext = null)
        {
            _databaseContext = databaseContext;
        }

        public IndividualServicingData GetIndividualServicingData(long individualAccountNumber)
        {
            var individualServicingData = new IndividualServicingData();

            using (var servicingDataContext = DatabaseContext as ServicingDataContext)
            {
                GetBasicAccountData(individualAccountNumber, individualServicingData, servicingDataContext);
                GetPremiumCalculationDetails(individualAccountNumber, individualServicingData, servicingDataContext);
                GetNotificationAndPaymentHistory(individualAccountNumber, individualServicingData, servicingDataContext);
                GetClaimsData(individualAccountNumber, individualServicingData, servicingDataContext);
            }

            return individualServicingData;
        }

        private void GetBasicAccountData(
            long individualAccountNumber, 
            IndividualServicingData individualServicingData, 
            ServicingDataContext servicingDataContext)
        {
            var individualCustomerDataEntity = servicingDataContext.InsureesAccountDataEntities
                .SingleOrDefault(e => e.AccountNumber == individualAccountNumber);

            var nextPaymentAndBalanceEntity = servicingDataContext
                .InsureesNextPaymentAndBalanceInformationEntities
                .Where(e => e.AccountNumber == individualAccountNumber)
                .OrderBy(i => i.Id).ToList().LastOrDefault();

            var enrollmentVerificationEntities = servicingDataContext
                .InsureesEnrollmentVerificationDetailsEntities
                .Where(e => e.AccountNumber == individualAccountNumber)
                .OrderBy(i => i.VerificationDate).ToList();

            var graduationVerificationEntities = servicingDataContext
                .InsureesGraduationVerificationDetailsEntities
                .Where(e => e.AccountNumber == individualAccountNumber)
                .OrderBy(i => i.VerificationDate).ToList();

            var emailsSetId = individualCustomerDataEntity.EmailsSetId;
            var emailEntities = servicingDataContext.EmailsEntities
                .Where(e => e.EmailsSetId == emailsSetId).ToList();

            individualServicingData.IndividualAccountData = individualCustomerDataEntity;
            individualServicingData.NextPaymentAndBalanceInformation = nextPaymentAndBalanceEntity;

            individualServicingData.EnrollmentVerificationHistory = enrollmentVerificationEntities;
            individualServicingData.GraduationVerificationHistory = graduationVerificationEntities;

            individualServicingData.Emails = emailEntities;
        }

        private void GetPremiumCalculationDetails(
            long individualAccountNumber, 
            IndividualServicingData individualServicingData, 
            ServicingDataContext servicingDataContext)
        {
            var premiumCalculationDetailsSetEntity = servicingDataContext
                .InsureesPremiumCalculationDetailsSetEntities
                .Where(e => e.AccountNumber == individualAccountNumber)
                .OrderBy(i => i.SetId).ToList().LastOrDefault();

            if (premiumCalculationDetailsSetEntity != null)
            {
                var premiumCalculationDetailsId = premiumCalculationDetailsSetEntity.InsureesPremiumCalculationDetailsId;
                var premiumCalculationDetailsEntity = servicingDataContext
                    .InsureesPremiumCalculationDetailsEntities
                    .SingleOrDefault(e => e.Id == premiumCalculationDetailsId);

                var premiumCalculationOptionDetailsSetId = premiumCalculationDetailsSetEntity.InsureesPremiumCalculationOptionDetailsSetId;
                var premiumCalculationOptionDetailsEntities = servicingDataContext
                    .InsureesPremiumCalculationOptionDetailsEntities
                    .Where(e => e.InsureesPremiumCalculationOptionDetailsSetId == premiumCalculationOptionDetailsSetId).ToList();

                individualServicingData.PremiumCalculationDetails = premiumCalculationDetailsEntity;
                individualServicingData.PremiumCalculationOptionDetails = premiumCalculationOptionDetailsEntities;

                GetCollegeMajorMinorDetails(individualServicingData, servicingDataContext, premiumCalculationDetailsEntity);
            }
        }

        private void GetCollegeMajorMinorDetails(
            IndividualServicingData individualServicingData, 
            ServicingDataContext servicingDataContext, 
            InsureesPremiumCalculationDetailsEntity premiumCalculationDetailsEntity)
        {
            var collegeMajorMinorDetailsSetId = premiumCalculationDetailsEntity.InsureesMajorMinorDetailsSetId;
            var collegeMajorMinorDetailsEntities = servicingDataContext.InsureesMajorMinorDetailsEntities
                .Where(e => e.InsureesMajorMinorDetailsSetId == collegeMajorMinorDetailsSetId).ToList();

            individualServicingData.MajorMinorDetails = collegeMajorMinorDetailsEntities;
        }

        private void GetNotificationAndPaymentHistory(
            long individualAccountNumber, 
            IndividualServicingData individualServicingData, 
            ServicingDataContext servicingDataContext)
        {
            var notificationHistoryEntities = servicingDataContext.InsureesNotificationHistoryEntryEntities
                .Where(e => e.AccountNumber == individualAccountNumber).ToList();

            var paymentHistoryEntities = servicingDataContext.InsureesPaymentHistoryEntryEntities
                .Where(e => e.AccountNumber == individualAccountNumber).ToList();

            individualServicingData.NotificationHistory = notificationHistoryEntities;
            individualServicingData.PaymentHistory = paymentHistoryEntities;
        }

        private void GetClaimsData(
            long individualAccountNumber, 
            IndividualServicingData individualServicingData, 
            ServicingDataContext servicingDataContext)
        {
            var claimEntities = servicingDataContext.ClaimAccountEntryEntities
                .Where(e => e.AccountNumber == individualAccountNumber).ToList();

            var claimNumbers = claimEntities.Select(c => c.ClaimNumber).ToList();

            var claimOptionsEntities = servicingDataContext.ClaimOptionEntryEntities
                .Where(e => claimNumbers.Contains(e.ClaimNumber))
                .OrderBy(i => i.Id).ToList();
            var claimStatusEntities = servicingDataContext.ClaimStatusEntryEntities
                .Where(e => claimNumbers.Contains(e.ClaimNumber))
                .OrderBy(i => i.Id).ToList();
            var claimDocumentEntities = servicingDataContext.ClaimDocumentEntryEntities
                .Where(e => claimNumbers.Contains(e.ClaimNumber))
                .OrderBy(i => i.Id).ToList();
            var claimPaymentEntities = servicingDataContext.ClaimPaymentEntryEntities
                .Where(e => claimNumbers.Contains(e.ClaimNumber))
                .OrderBy(i => i.Id).ToList();

            var claimOptionsDictionary = new Dictionary<long, ClaimOptionEntryEntity>();
            claimOptionsEntities.ForEach(o =>
            {
                if (!claimOptionsDictionary.ContainsKey(o.ClaimNumber))
                    claimOptionsDictionary.Add(o.ClaimNumber, o);
                else
                    claimOptionsDictionary[o.ClaimNumber] = o;
            });

            var claimStatusDictionary = new Dictionary<long, ClaimStatusEntryEntity>();
            claimStatusEntities.ForEach(s =>
            {
                if (!claimStatusDictionary.ContainsKey(s.ClaimNumber))
                    claimStatusDictionary.Add(s.ClaimNumber, s);
                else
                    claimStatusDictionary[s.ClaimNumber] = s;
            });

            var claimDocumentsDictionary = new Dictionary<long, List<ClaimDocumentEntryEntity>>();
            claimDocumentEntities.ForEach(d =>
            {
                if (!claimDocumentsDictionary.ContainsKey(d.ClaimNumber))
                    claimDocumentsDictionary.Add(d.ClaimNumber, new List<ClaimDocumentEntryEntity>());

                claimDocumentsDictionary[d.ClaimNumber].Add(d);
            });

            individualServicingData.ClaimNumbers = claimNumbers;
            individualServicingData.ClaimOptionsDictionary = claimOptionsDictionary;
            individualServicingData.ClaimStatusDictionary = claimStatusDictionary;
            individualServicingData.ClaimDocumentsDictionary = claimDocumentsDictionary;
            individualServicingData.ClaimsPaymentHistory = claimPaymentEntities;
        }
    }
}
