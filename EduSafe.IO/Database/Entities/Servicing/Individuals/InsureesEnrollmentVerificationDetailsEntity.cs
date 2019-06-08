using System;

namespace EduSafe.IO.Database.Entities.Servicing.Individuals
{
    public class InsureesEnrollmentVerificationDetailsEntity
    {
        public int Id { get; set; }
        public long AccountNumber { get; set; }
        public bool IsVerified { get; set; }
        public DateTime VerificationDate { get; set; }
        public string Comments { get; set; }
    }
}
