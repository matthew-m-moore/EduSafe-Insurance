using System;

namespace EduSafe.IO.Database.Entities.Servicing.Institutions
{
    public class InstitutionsNextPaymentAndBalanceInformationEntity
    {
        public int Id { get; set; }
        public long InstitutionsAccountNumber { get; set; }
        public double NextPaymentAmount { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public double CurrentBalance { get; set; }
        public int NextPaymentStatusTypeId { get; set; }

        public string NextPaymentStatusType { get; set; }
    }
}
