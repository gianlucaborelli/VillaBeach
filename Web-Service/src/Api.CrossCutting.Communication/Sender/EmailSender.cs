using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Api.CrossCutting.Communication.Interfaces;
using Api.CrossCutting.Communication.Settings;

namespace Api.CrossCutting.Communication.Sender
{
    public class EmailSender(
        IOptions<EmailSettings> emailSettings) : IEmailSender
    {
        private EmailSettings _emailSettings = emailSettings.Value;

        public async Task SendEmailAsync(
            string to,
            string subject,
            string message,
            string? attachment = null,
            MailPriority priority = MailPriority.Normal,
            DeliveryNotificationOptions deliveryNotificationOptions = DeliveryNotificationOptions.OnFailure)
        {
            try
            {
                MailMessage mail = new()
                {
                    From = new MailAddress(_emailSettings.Email, _emailSettings.DisplayName)                    
                };
                
                mail.To.Add(new MailAddress(to));

                mail.Subject = "VillaBeach - " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = priority;
                mail.DeliveryNotificationOptions = deliveryNotificationOptions;

                if (attachment is not null) mail.Attachments.Add(new Attachment(attachment));

                using SmtpClient smtp = new(_emailSettings.Host, _emailSettings.Port);
                smtp.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
                smtp.EnableSsl = true;

                await smtp.SendMailAsync(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

