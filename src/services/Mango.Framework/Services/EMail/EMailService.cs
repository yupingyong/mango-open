using System;
using System.Collections.Generic;
using System.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;
namespace Mango.Framework.Services.EMail
{
    public class EMailService:IEMailService
    {
        private EMailOptions _options;
        public EMailService(EMailOptions options)
        {
            _options = options;
        }
        public bool SendEmail(string email, string subject, string message)
        {
            bool sendResult = false;
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(_options.FromName, _options.FromEMail));
                emailMessage.To.Add(new MailboxAddress("mail", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart("plain") { Text = message };

                using (var client = new SmtpClient())
                {
                    client.Connect(_options.SmtpServerUrl, _options.SmtpServerPort, true);
                    client.Authenticate(_options.SmtpAuthenticateEmail, _options.SmtpAuthenticatePasswordText);

                    client.Send(emailMessage);
                    client.Disconnect(true);
                    sendResult = true;
                }
            }
            catch
            {
                sendResult = false;
            }
            return sendResult;
        }
    }
}
