using System;

namespace EduSafe.IO.Database.Entities.Servicing.Institutions
{
    public class InstitutionsPaymentHistoryEntryEntity
    {
        public int Id { get; set; }
        public long InstitutionsAccountNumber { get; set; }
        public double PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentComments { get; set; }
    }
}
