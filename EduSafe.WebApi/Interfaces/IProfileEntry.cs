using System.Collections.Generic;
using EduSafe.WebApi.Models;

namespace EduSafe.WebApi.Interfaces
{
    internal interface IProfileEntry
    {
        long CustomerIdNumber { get; set; }
        string CustomerUniqueId { get; set; }

        string CustomerName { get; set; }
        string CustomerAddress1 { get; set; }
        string CustomerAddress2 { get; set; }
        string CustomerAddress3 { get; set; }
        string CustomerCity { get; set; }
        string CustomerState { get; set; }
        string CustomerZip { get; set; }

        int EmailSetId { get; set; }
        List<CustomerEmailEntry> CustomerEmails { get; set; }
        List<PaymentHistoryEntry> PaymentHistoryEntries { get; set; }
        List<NotificationHistoryEntry> NotificationHistoryEntries { get; set; }
    }
}
