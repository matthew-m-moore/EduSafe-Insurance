namespace EduSafe.WebApi.Models
{
    public class InquiryEmailEntry
    {
        public string IpAddress { get; set; }
        public string ContactAddress { get; set; }
        public string ContactName { get; set; }
        public string EmailBody { get; set; }
        public bool ReceiveCopy { get; set; }
    }
}