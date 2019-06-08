using System;

namespace EduSafe.IO.Database.Entities.Servicing.Individuals
{
    public class InsureesPaymentHistoryEntryEntity
    {
        public int Id { get; set; }
        public long AccountNumber { get; set; }
        public double PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentComments { get; set; }
    }
}
