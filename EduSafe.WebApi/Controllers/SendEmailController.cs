using System.Web.Http;
using EduSafe.IO.Email;
using EduSafe.WebApi.Models;

namespace EduSafe.WebApi.Controllers
{
    [RoutePrefix("api/email")]
    public class SendEmailController : ApiController
    {
        private const string _contactFrom = "Contact Email From: ";
        private const string _yourResults = "Your Monthly Premium Calculation Results from Edu$afe";

        // POST: api/email/contact
        [Route("contact")]
        [HttpPost]
        public bool SendContactEmail(InquiryEmailEntry inquiryEmailEntry)
        {
            var contactAddress = inquiryEmailEntry.ContactAddress;
            var contactName = inquiryEmailEntry.ContactName;
            var emailsubject = _contactFrom + contactName + ", " + contactAddress;

            var emailSender = new EmailSender();
            var emailCreator = new EmailCreator(emailsubject, inquiryEmailEntry.EmailBody, isPlainText: true);

            if (inquiryEmailEntry.ReceiveCopy) emailCreator.AddCopiedRecipient(contactAddress);
            emailSender.Send(emailCreator);
            return true;
        }

        // PUT: api/email/results
        [Route("results")]
        [HttpPut]
        public bool SendResultsEmail(ResultsEmailEntry resultsEmailEntry)
        {
            // Probably need to do a lot more processing that just ripping the page HTML
            var recipientAddress = resultsEmailEntry.RecipientAddress;
            var emailBody = resultsEmailEntry.ResultsPageHtml;

            var emailSender = new EmailSender();
            var emailCreator = new EmailCreator(_yourResults, emailBody, recipientAddress);
            emailSender.Send(emailCreator);
            return true;
        }
    }
}