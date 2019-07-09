using System;
using System.Collections.Generic;
using EduSafe.WebApi.Interfaces;

namespace EduSafe.WebApi.Models
{
    public class InstitutionProfileEntry : IProfileEntry
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

        public double CustomerBalance { get; set; }
        public double MonthlyPaymentAmount { get; set; }
        public DateTime NextPaymentDueDate { get; set; }
        public List<PaymentHistoryEntry> PaymentHistoryEntries { get; set; }

        public List<CustomerProfileEntry> CustomerProfileEntries { get; set; }
        public List<NotificationHistoryEntry> NotificationHistoryEntries { get; set; }
    }
}