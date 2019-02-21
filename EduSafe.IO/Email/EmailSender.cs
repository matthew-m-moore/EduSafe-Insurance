using System;
using System.Net;
using System.Net.Mail;

namespace EduSafe.IO.Email
{
    public class EmailSender
    {
        private SmtpClient _emailClient;

        public EmailSender(bool enableSsl = true)
        {
            var emailCredentials = 
                new NetworkCredential(InputOutput.Default.EmailUser, InputOutput.Default.EmailPassword);

            _emailClient = new SmtpClient(InputOutput.Default.EmailServer, InputOutput.Default.EmailPort)
            {
                EnableSsl = enableSsl,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = InputOutput.Default.EmailTimeout,
                Credentials = emailCredentials,
            };
        }

        public void Send(EmailCreator emailCreator)
        {
            var emailMessage = emailCreator.EmailMessage;
            Send(emailMessage);
        }

        public void Send(MailMessage emailMessage)
        {
            try
            {
                _emailClient.Send(emailMessage);
            }
            catch
            {
                // Optimistically try again if email fails to send the first time
                try
                {
                    _emailClient.Send(emailMessage);
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine(ex.Message + ", " + ex.StackTrace);
                    // Maybe write something to the datbase to show that the second try failed
                    // and, capture the exception text
                }
            }
        }
    }
}
