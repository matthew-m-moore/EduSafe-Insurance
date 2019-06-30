using EduSafe.IO.Database.Entities.Servicing;

namespace EduSafe.WebApi.Models
{
    public class CustomerEmailEntry
    {
        public int EmailSetId { get; set; }
        public int EmailId { get; set; }
        public string EmailAddress { get; set; }
        public bool IsPrimary { get; set; }

        public CustomerEmailEntry(EmailsEntity emailsEntity)
        {
            EmailSetId = emailsEntity.EmailsSetId;
            EmailId = emailsEntity.Id;
            EmailAddress = emailsEntity.Email;
            IsPrimary = emailsEntity.IsPrimary;
        }
    }
}