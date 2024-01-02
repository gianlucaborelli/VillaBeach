using System.Net;
using System.Net.Mail;
using Api.Domain.EmailSettings;
using Api.Domain.Interfaces.Services.Email;
using Microsoft.Extensions.Options;

namespace Api.Service.Helpers
{
    public class EmailSender : IEmailSender
    {
        private EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                Execute(email, subject, message, null).Wait();
                return Task.FromResult(0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task SendEmailWithAttachmentAsync(string email, string subject, string message, string attachment)
        {
            try
            {
                Execute(email, subject, message, attachment).Wait();
                return Task.FromResult(0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task Execute(string toEmail, string subject, string message, string? attachment)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.Email, _emailSettings.DisplayName)
                };

                mail.To.Add(new MailAddress(toEmail));

                mail.Subject = "VillaBeach - " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                if (attachment != null) mail.Attachments.Add(new Attachment(attachment));


                using (SmtpClient smtp = new SmtpClient(_emailSettings.Host, _emailSettings.Port))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

