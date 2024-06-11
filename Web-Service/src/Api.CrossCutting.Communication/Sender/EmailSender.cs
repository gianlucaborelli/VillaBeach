using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Api.CrossCutting.Communication.Interfaces;
using Api.CrossCutting.Communication.Settings;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace Api.CrossCutting.Communication.Sender
{
    public class EmailSender(IOptions<EmailSettings> emailSettings, ILogger<EmailSender> logger) : IEmailSender
    {
        private readonly EmailSettings _emailSettings = emailSettings.Value;
        private readonly ILogger<EmailSender> _logger = logger;

        public async Task SendEmailAsync(
            string to,
            string subject,
            string message,
            string? attachment = null,
            MailPriority priority = MailPriority.Normal,
            DeliveryNotificationOptions deliveryNotificationOptions = DeliveryNotificationOptions.OnFailure)
        {
            string emailId = Guid.NewGuid().ToString();
            _logger.LogInformation("Starting to send an email {EmailId} with subject {Subject} to {To}", emailId, subject, to);

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

                smtp.SendCompleted += (sender, e) => SendCompletedCallback(sender, e, emailId);

                smtp.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
                smtp.EnableSsl = true;
                
                _logger.LogInformation("Configuring SMTP client for sending email {EmailId} to {To} with subject {Subject}. Host: {Host}, Port: {Port}", emailId, to, subject, _emailSettings.Host, _emailSettings.Port);
                
                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal - Failed to send email {EmailId} to {To}", emailId, to);
                throw;
            }
        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e, string emailId)
        {
            if (e.Error is not null)
            {
                _logger.LogError(e.Error, "External - Failed to send email {EmailId}", emailId);
            }
            else
            {
                _logger.LogInformation("Email {EmailId} successfully sent", emailId);
            }

            if (e.Cancelled)
            {
                _logger.LogWarning("Email {EmailId} sending was cancelled", emailId);
            }
        }
    }
}

