﻿using System.Linq;
using System.Web.Http;
using EduSafe.IO.Email;
using EduSafe.WebApi.Models;
using EduSafe.WebApi.Models.Individuals;
using EduSafe.WebApi.Models.Institutions;

namespace EduSafe.WebApi.Controllers
{
    [RoutePrefix("api/email")]
    public class SendEmailController : ApiController
    {
        private const string _contactFrom = "Contact Email From: ";
        private const string _yourResults = "Your Monthly Premium Calculation Results from Edu$afe";
        private const string _estimatedResults = "Estimated Premium Calculation Results from Edu$afe";
        private const string _contactUs = "Please don't hesitate to contact us at inquiries@edusafe.company if you have any questions.";

        private const string _buttonStart = "<button ";
        private const string _buttonEnd = "</button>";

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
            var recipientAddress = resultsEmailEntry.RecipientAddress;
            var emailBody = resultsEmailEntry.ResultsPageHtml;

            var clipStartIndex = emailBody.IndexOf(_buttonStart);
            var clipEndIndex = emailBody.LastIndexOf(_buttonEnd);

            var startOfEmailBody = new string(emailBody.Take(clipStartIndex).ToArray());
            var endOfEmailBody = new string(emailBody.Skip(clipEndIndex + _buttonEnd.Length).ToArray());

            // This gobbledeguk inserts our email in place of the buttons, left-aligns the table,
            // and also makes the remaining text appear below the table.
            var editedEmailBody = startOfEmailBody + _contactUs + endOfEmailBody;
            editedEmailBody = editedEmailBody.Replace("align=\"center\"", "align=\"left\"");
            editedEmailBody = editedEmailBody.Replace(
                "<i _ngcontent-",
                "<br><br><br><br><br><br><br><br><div><i _ngcontent-");
            editedEmailBody += "</div>";

            var emailSender = new EmailSender();
            var emailCreator = new EmailCreator(_yourResults, editedEmailBody, recipientAddress);
            emailSender.Send(emailCreator);
            return true;
        }

        // PUT: api/email/institutional-results
        [Route("institutional-results")]
        [HttpPut]
        public bool SendInstitutionResultEmail(InstitutionResultEmailEntry institutionResultsEmailEntry)
        {
            var recipientAddress = institutionResultsEmailEntry.RecipientAddress;
            var emailBody = institutionResultsEmailEntry.ResultsPageHtml;

            var clipStartIndex = emailBody.IndexOf(_buttonStart);
            var clipEndIndex = emailBody.LastIndexOf(_buttonEnd);

            var startOfEmailBody = new string(emailBody.Take(clipStartIndex).ToArray());
            var endOfEmailBody = new string(emailBody.Skip(clipEndIndex + _buttonEnd.Length).ToArray());

            // This gobbledeguk inserts our email in place of the buttons, left-aligns the table,
            // and also makes the remaining text appear below the table.
            var editedEmailBody = startOfEmailBody + "<br>" + _contactUs + endOfEmailBody;
            editedEmailBody = editedEmailBody.Replace("align=\"center\"", "align=\"left\"");
            editedEmailBody = editedEmailBody.Replace(
                "class=\"col-md-8 text-left\"><i _ngcontent-",
                "class=\"col-md-8 text-left\"><br><br><br><br><br><br><br><br><br><br><br><div><i _ngcontent-");
            editedEmailBody += "</div>";

            var emailSender = new EmailSender();
            var emailCreator = new EmailCreator(_estimatedResults, editedEmailBody, recipientAddress);
            emailSender.Send(emailCreator);
            return true;
        }
    }
}