using System;

namespace EduSafe.WebApi.Models
{
    public class ClaimPaymentEntry
    {
        public double PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentComments { get; set; }
    }
}