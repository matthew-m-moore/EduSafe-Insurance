namespace EduSafe.IO.Database.Entities.WebApp
{
    public class WebSiteInquiryEmailAddressEntity
    {
        public int Id { get; set; }
		public string EmailAddress { get; set; }
        public string IpAddress { get; set; }
        public string ContactName { get; set; }
        public bool? OptOut { get; set; }
    }
}
