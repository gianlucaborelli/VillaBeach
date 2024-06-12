
using System.Net.Mail;
using Api.CrossCutting.Communication.Enums;
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
            try
            {
                var emailTemplate = await LoadTemplate("EmailValidationModel.html");

                emailTemplate = emailTemplate.Replace("{{USER_NAME}}", toName)
                                             .Replace("{{LINK_VALIDATION}}", verificationLink);

                await _emailSender.SendEmailAsync(toEmail, "Verificação de Email", emailTemplate);

                _logger.LogInformation($"Email verification sent to: {toEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending email verification to {toEmail}: {ex.Message}");
                throw;
            }
        }

        public async Task SendForgotPasswordEmail(string toEmail, string toName, string verificationLink)
        {
            try
            {
                var emailTemplate = await LoadTemplate(TemplateEmailEnum.ForgetPassword.ToFileName());

                emailTemplate = emailTemplate.Replace("{{USER_NAME}}", toName)
                                             .Replace("{{RESET_TOKEN}}", verificationLink);

                await _emailSender.SendEmailAsync(toEmail, "Recuperação de senha", emailTemplate);

                _logger.LogInformation($"Forgot password email sent to: {toEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending forgot password email to {toEmail}: {ex.Message}");
                throw;
            }
        }

        private async Task<string> LoadTemplate(string templateName)
        {
            var templatePath = Path.Combine(_templateDirectory, $"{templateName}");

            if (!File.Exists(templatePath))
                throw new FileNotFoundException($"Template not found at {templatePath}");

            var emailTemplate = await File.ReadAllTextAsync(templatePath);

            return emailTemplate;
        }
    }
}