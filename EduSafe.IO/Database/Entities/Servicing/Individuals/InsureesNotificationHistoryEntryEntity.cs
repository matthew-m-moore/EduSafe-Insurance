using System;

namespace EduSafe.IO.Database.Entities.Servicing.Individuals
{
    public partial class InsureesNotificationHistoryEntryEntity
    {
        public int Id { get; set; }
        public long AccountNumber { get; set; }
        public int NotificationTypeId { get; set; }
        public DateTime NotificationDate { get; set; }
    }
}
