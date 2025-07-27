namespace Api.CrossCutting.Communication.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailVerification(string toEmail, string toName, string verificationLink);
        Task SendForgotPasswordEmail(string toEmail, string toName, string verificationLink);
    }
}