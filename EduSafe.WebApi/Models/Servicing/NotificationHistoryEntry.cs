using System;

namespace EduSafe.WebApi.Models.Servicing
{
    public class NotificationHistoryEntry
    {
        public string NotificationType { get; set; }
        public DateTime NotificationDate { get; set; }
        public string NotificationDescription { get; set; }
    }
}