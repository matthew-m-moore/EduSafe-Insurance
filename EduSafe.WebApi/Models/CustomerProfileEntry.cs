using System;
using System.Collections.Generic;
using EduSafe.WebApi.Interfaces;

namespace EduSafe.WebApi.Models
{
    public class CustomerProfileEntry : IProfileEntry
    {
        public long CustomerIdNumber { get; set; }
        public string CustomerUniqueId { get; set; }

        public string CustomerName { get; set; }
        public string CustomerAddress1 { get; set; }
        public string CustomerAddress2 { get; set; }
        public string CustomerAddress3 { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string CustomerZip { get; set; }

        public int EmailSetId { get; set; }
        public List<CustomerEmailEntry> CustomerEmails { get; set; }

        public string CollegeName { get; set; }
        public string CollegeMajor { get; set; }
        public string CollegeMinor { get; set; }
        public DateTime CollegeStartDate { get; set; }
        public DateTime ExpectedGraduationDate { get; set; }
        public List<NotificationHistoryEntry> NotificationHistoryEntries { get; set; }

        public double CustomerBalance { get; set; }
        public double MonthlyPaymentAmount { get; set; }
        public double TotalPaidInPremiums { get; set; }
        public DateTime NextPaymentDueDate { get; set; }
        public List<PaymentHistoryEntry> PaymentHistoryEntries { get; set; }

        public double TotalCoverageAmount { get; set; }
        public double? RemainingCoverageAmount { get; set; }
        public double CoverageMonths { get; set; }
        public List<ClaimOptionEntry> ClaimOptionEntries { get; set; }
        public List<ClaimStatusEntry> ClaimStatusEntries { get; set; }
        public List<ClaimPaymentEntry> ClaimPaymentEntries { get; set; }

        public bool EnrollmentVerified { get; set; }
        public bool GraduationVerified { get; set; }
        public bool HasClaims { get; set; }

        public List<string> InstitutionIdentifers { get; set; }
    }
}