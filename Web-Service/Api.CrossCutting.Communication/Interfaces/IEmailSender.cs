using System.Net.Mail;

namespace Api.CrossCutting.Communication.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(
            string to,
            string subject,
            string message,
            string? attachment = null,
            MailPriority priority = MailPriority.Normal,
            DeliveryNotificationOptions deliveryNotificationOptions = DeliveryNotificationOptions.OnFailure);
    }
}