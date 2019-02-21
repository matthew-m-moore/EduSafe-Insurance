using System.Net.Mail;
using System.Text;

namespace EduSafe.IO.Email
{
    public class EmailCreator
    {
        public bool IsPlainText { get; }

        public MailMessage EmailMessage { get; }

        public EmailCreator(string subject, string body, string recipient = null, bool isPlainText = false)
        {
            var defaultEmailSender = new MailAddress(InputOutput.Default.EmailUser, InputOutput.Default.EmailDisplayName);
            var defaultEmailReceiver = new MailAddress(InputOutput.Default.EmailReceiver);

            EmailMessage = new MailMessage()
            {
                Body = body,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = !isPlainText,

                Subject = subject,
                SubjectEncoding = Encoding.UTF8,

                From = defaultEmailSender
            };

            EmailMessage.ReplyToList.Add(new MailAddress(InputOutput.Default.EmailReceiver));

            if (recipient != null)
            {
                EmailMessage.To.Add(new MailAddress(recipient));
                EmailMessage.Bcc.Add(defaultEmailSender);
            }
            else
            {
                EmailMessage.To.Add(defaultEmailReceiver);
            }
        }

        public void AddCopiedRecipient(string emailAddressToCopy)
        {
            EmailMessage.CC.Add(new MailAddress(emailAddressToCopy));
        }
    }
}
