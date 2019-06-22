using System;
using System.Collections.Generic;

namespace EduSafe.WebApi.Models
{
    public class InstitutionProfileEntry
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
        public List<string> CustomerEmails { get; set; }

        public double CustomerBalance { get; set; }
        public double MonthlyPaymentAmount { get; set; }
        public DateTime NextPaymentDueDate { get; set; }
        public List<PaymentHistoryEntry> PaymentHistoryEntries { get; set; }

        public List<CustomerProfileEntry> CustomerProfileEntries { get; set; }
        public List<NotificationHistoryEntry> NotificationHistoryEntries { get; set; }
    }
}