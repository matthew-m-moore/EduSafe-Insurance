using System;
using System.ComponentModel;

namespace EduSafe.IO.Excel.Records
{
    public class PaymentHistoryRecord
    {
        [Description("Date")]
        public DateTime PaymentDate { get; set; }
        [Description("Amount ($)")]
        public double PaymentAmount { get; set; }
        [Description("Status")]
        public string PaymentStatus { get; set; }
        [Description("Comments")]
        public string PaymentComments { get; set; }
    }
}
