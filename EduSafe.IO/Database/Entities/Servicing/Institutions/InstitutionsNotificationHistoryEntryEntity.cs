using System;

namespace EduSafe.IO.Database.Entities.Servicing.Institutions
{
    public class InstitutionsNotificationHistoryEntryEntity
    {
        public int Id { get; set; }
        public long InstitutionsAccountNumber { get; set; }
        public int NotificationTypeId { get; set; }
        public DateTime NotificationDate { get; set; }

        public string NotificationType { get; set; }
    }
}
