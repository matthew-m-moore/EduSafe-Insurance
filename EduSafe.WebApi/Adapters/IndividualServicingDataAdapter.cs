using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.Repositories.Database;
using EduSafe.IO.Database;
using EduSafe.WebApi.Models;

namespace EduSafe.WebApi.Adapters
{
    public class IndividualServicingDataAdapter
    {
        private readonly ServicingDataTypesRepository _servicingDataTypesRepository;
        private readonly IndividualServicingDataRepository _individualServicingDataRepository;

        public IndividualServicingDataAdapter()
        {
            var databaseContext = DatabaseContextRetriever.GetServicingDataContext();

            _servicingDataTypesRepository = new ServicingDataTypesRepository(databaseContext);
            _individualServicingDataRepository = new IndividualServicingDataRepository(databaseContext);
        }

        public CustomerProfileEntry GetIndividualProfileData(long individualAccountNumber)
        {
            var individualServicingData = _individualServicingDataRepository.GetIndividualServicingData(individualAccountNumber);

            var customerNameFormatted = GetFormattedCustomerName(individualServicingData);
            var collegeMajorFormatted = GetFormattedCollegeMajorMinor(individualServicingData);
            var collegeMinorFormatted = GetFormattedCollegeMajorMinor(individualServicingData, true);
            var notificationHistory = GetNotificationHistory(individualServicingData);
            var paymentHistory = GetPaymentHistory(individualServicingData, out var totalPaidInPremiums);
            var claimStatusEntries = GetClaimStatusEntries(individualServicingData);
            var claimPaymentEntries = GetClaimPaymentEntries(individualServicingData, out var remainingCoverageAmount);

            var collegeName = _servicingDataTypesRepository
                .CollegeNameDictionary[individualServicingData.PremiumCalculationDetails.CollegeDetailId];

            var customerProfileEntry = new CustomerProfileEntry
            {
                CustomerIdNumber = individualServicingData.IndividualAccountData.AccountNumber,
                CustomerUniqueId = individualServicingData.IndividualAccountData.FolderPath,

                CustomerName = customerNameFormatted,
                CustomerAddress1 = individualServicingData.IndividualAccountData.Address1,
                CustomerAddress2 = individualServicingData.IndividualAccountData.Address2,
                CustomerAddress3 = individualServicingData.IndividualAccountData.Address3,
                CustomerCity = individualServicingData.IndividualAccountData.City,
                CustomerState = individualServicingData.IndividualAccountData.State,
                CustomerZip = individualServicingData.IndividualAccountData.ZipCode,
                CustomerEmails = individualServicingData.Emails.Select(e => e.Email).ToList(),

                CollegeName = collegeName,
                CollegeMajor = collegeMajorFormatted,
                CollegeMinor = collegeMinorFormatted,
                CollegeStartDate = individualServicingData.PremiumCalculationDetails.CollegeStartDate,
                ExpectedGraduationDate = individualServicingData.PremiumCalculationDetails.ExpectedGraduationDate,
                NotificationHistoryEntries = notificationHistory,

                CustomerBalance = individualServicingData.NextPaymentAndBalanceInformation.CurrentBalance,
                MonthlyPaymentAmount = individualServicingData.NextPaymentAndBalanceInformation.NextPaymentAmount,
                TotalPaidInPremiums = totalPaidInPremiums,
                NextPaymentDueDate = individualServicingData.NextPaymentAndBalanceInformation.NextPaymentDate,
                PaymentHistoryEntries = paymentHistory,

                TotalCoverageAmount = individualServicingData.PremiumCalculationDetails.TotalCoverageAmount,
                RemainingCoverageAmount = remainingCoverageAmount,
                CoverageMonths = individualServicingData.PremiumCalculationDetails.CoverageMonths

            };

            return new CustomerProfileEntry();
        }

        private string GetFormattedCustomerName(IndividualServicingData individualServicingData)
        {
            var customerNameFormatted = individualServicingData.IndividualAccountData.LastName +
                ", " + individualServicingData.IndividualAccountData.FirstName;

            if (!string.IsNullOrEmpty(individualServicingData.IndividualAccountData.MiddleName))
            {
                var middleInitial = individualServicingData.IndividualAccountData.MiddleName.First();
                customerNameFormatted += (" " + middleInitial + ".");
            }

            return customerNameFormatted;
        }

        private string GetFormattedCollegeMajorMinor(IndividualServicingData individualServicingData, bool isMinor = false)
        {
            var collegeMajorMinorFormatted = string.Empty;
            foreach (var collegeMajorMinorDetail in 
                individualServicingData.MajorMinorDetails.Where(d => d.IsMinor == isMinor))
            {
                var collegeMajorMinorName = _servicingDataTypesRepository
                    .CollegeMajorDictionary[collegeMajorMinorDetail.CollegeMajorId];

                if (string.IsNullOrEmpty(collegeMajorMinorFormatted))
                    collegeMajorMinorFormatted += collegeMajorMinorName;
                else
                    collegeMajorMinorFormatted += (" / " + collegeMajorMinorName);
            }

            return collegeMajorMinorFormatted;
        }

        private List<NotificationHistoryEntry> GetNotificationHistory(IndividualServicingData individualServicingData)
        {
            var notificationHistoryEntries = new List<NotificationHistoryEntry>();

            foreach (var notificationHistoryEntity in individualServicingData.NotificationHistory)
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

        private List<PaymentHistoryEntry> GetPaymentHistory(IndividualServicingData individualServicingData, out double totalPaidInPremiums)
        {
            var paymentHistoryEntries = new List<PaymentHistoryEntry>();
            totalPaidInPremiums = 0.0;

            foreach (var paymentHistoryEntity in individualServicingData.PaymentHistory)
            {
                var paymentStatusType = _servicingDataTypesRepository
                    .PaymentStatusTypeDictionary[paymentHistoryEntity.PaymentStatusTypeId];

                if (paymentStatusType == PaymentStatusType.Processed)
                    totalPaidInPremiums += paymentHistoryEntity.PaymentAmount;

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

        private List<ClaimStatusEntry> GetClaimStatusEntries(IndividualServicingData individualServicingData)
        {
            throw new NotImplementedException();
        }

        private List<ClaimPaymentEntry> GetClaimPaymentEntries(IndividualServicingData individualServicingData, out double? remainingCoverageAmount)
        {
            var claimPaymentEntries = new List<ClaimPaymentEntry>();
            remainingCoverageAmount = null;

            foreach (var claimPaymentHistoryEntity in individualServicingData.ClaimsPaymentHistory)
            {
                var claimPaymentStatusType = _servicingDataTypesRepository
                    .PaymentStatusTypeDictionary[claimPaymentHistoryEntity.ClaimPaymentStatusTypeId];

                var claimTypeId = individualServicingData
                    .ClaimOptionsDictionary[claimPaymentHistoryEntity.ClaimNumber].ClaimOptionTypeId;
                var claimType = _servicingDataTypesRepository.OptionTypeDictionary[claimTypeId];

                if (claimType == OptionType.UnemploymentOption && claimPaymentStatusType == PaymentStatusType.Processed)
                {
                    if (remainingCoverageAmount.HasValue)
                        remainingCoverageAmount -= claimPaymentHistoryEntity.ClaimPaymentAmount;
                    else
                        remainingCoverageAmount = individualServicingData.PremiumCalculationDetails.TotalCoverageAmount
                            - claimPaymentHistoryEntity.ClaimPaymentAmount;
                }
                    

                var claimPaymentEntry = new ClaimPaymentEntry
                {
                    PaymentAmount = claimPaymentHistoryEntity.ClaimPaymentAmount,
                    PaymentDate = claimPaymentHistoryEntity.ClaimPaymentDate,
                    PaymentStatus = claimPaymentStatusType.ToString(),
                    PaymentComments = claimPaymentHistoryEntity.ClaimPaymentComments,
                };

                claimPaymentEntries.Add(claimPaymentEntry);
            }

            return claimPaymentEntries;
        }
    }
}