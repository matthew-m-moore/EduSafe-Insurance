using System;

namespace EduSafe.IO.Database.Entities.Servicing.Individuals
{
    public partial class InsureesNextPaymentAndBalanceInformationEntity
    {
        public int Id { get; set; }
        public long AccountNumber { get; set; }
        public double NextPaymentAmount { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public double CurrentBalance { get; set; }
        public int NextPaymentStatusTypeId { get; set; }
    }
}
