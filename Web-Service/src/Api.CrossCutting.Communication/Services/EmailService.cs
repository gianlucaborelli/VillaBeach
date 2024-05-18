
using Api.CrossCutting.Communication.Interfaces;
using Microsoft.Extensions.Logging;

namespace Api.CrossCutting.Communication.Services
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

        public Task SendForgotPasswordEmail(string toEmail, string toName, string verificationLink )
        {
            var message = EmailModels.ForgotPassword.Replace("{{USER_NAME}}", toName).Replace("{{FORGOT_TOKEN}}", verificationLink);

            return _emailSender.SendEmailAsync(toEmail, "Verificação de Email", message) ;
        }
    }
}