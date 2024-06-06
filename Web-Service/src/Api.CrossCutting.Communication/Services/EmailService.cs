
using Api.CrossCutting.Communication.Interfaces;
using Microsoft.Extensions.Logging;

namespace Api.CrossCutting.Communication.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IEmailSender _emailSender;

        private readonly string _templateDirectory;

        public EmailService(ILogger<EmailService> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
            _templateDirectory = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, "Api.CrossCutting.Communication", "Templates");            
        }

        public async Task SendEmailVerification(string toEmail, string toName, string verificationLink)
        {
            var templatePath = Path.Combine(_templateDirectory, $"EmailValidationModel.html");

            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Template not found at {templatePath}");
            }

            var emailTemplate = await File.ReadAllTextAsync(templatePath);

            
            emailTemplate = emailTemplate.Replace("{{USER_NAME}}", toName)
                                         .Replace("{{LINK_VALIDATION}}", verificationLink);

            await _emailSender.SendEmailAsync(toEmail, "Verificação de Email", emailTemplate);
            return ;
        }

        public async Task SendForgotPasswordEmail(string toEmail, string toName, string verificationLink)
        {
            //var message = EmailModels.ForgotPassword.Replace("{{USER_NAME}}", toName).Replace("{{FORGOT_TOKEN}}", verificationLink);

            string message = string.Empty;
            await _emailSender.SendEmailAsync(toEmail, "Verificação de Email", message);
            return ;
        }
    }
}