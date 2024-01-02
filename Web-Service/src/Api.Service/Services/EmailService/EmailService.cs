using Api.Domain.Interfaces.Services.Email;
using Microsoft.Extensions.Logging;


namespace Api.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IEmailSender _emailSender;

        public EmailService(ILogger<EmailService> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }

        public Task SendEmailVerification(string toEmail, string toName, string verificationLink )
        {
            var message = EmailModels.EmailValidation.Replace("{{USER_NAME}}", toName).Replace("{{LINK_VALIDATION}}", verificationLink);

            return _emailSender.SendEmailAsync(toEmail, "Verificação de Email", message) ;
        }
    }
}