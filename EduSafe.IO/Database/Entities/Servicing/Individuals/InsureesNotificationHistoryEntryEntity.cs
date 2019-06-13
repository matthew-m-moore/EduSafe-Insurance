using System;

namespace EduSafe.IO.Database.Entities.Servicing.Individuals
{
    public class InsureesNotificationHistoryEntryEntity
    {
        public int Id { get; set; }
        public long AccountNumber { get; set; }
        public int NotificationTypeId { get; set; }
        public DateTime NotificationDate { get; set; }

        public string NotificationType { get; set; }
    }
}
