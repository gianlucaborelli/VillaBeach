namespace Api.Domain.Interfaces.Services.Email
{
    public interface IEmailService
    {
        Task SendEmailVerification(string toEmail, string toName, string verificationLink);
        Task SendForgotPasswordEmail(string toEmail, string toName, string verificationLink);
    }
}