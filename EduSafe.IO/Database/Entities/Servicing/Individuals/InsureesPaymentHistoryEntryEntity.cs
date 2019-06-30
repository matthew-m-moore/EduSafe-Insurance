using System;

namespace EduSafe.IO.Database.Entities.Servicing.Individuals
{
    public partial class InsureesPaymentHistoryEntryEntity
    {
        public int Id { get; set; }
        public long AccountNumber { get; set; }
        public double PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int PaymentStatusTypeId { get; set; }
        public string PaymentComments { get; set; }
    }
}
