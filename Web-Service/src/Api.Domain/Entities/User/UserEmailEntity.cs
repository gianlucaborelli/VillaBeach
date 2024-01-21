namespace Api.Domain.Entities
{
    public class UserEmailEntity
    {
        public string Address { get; set; } = string.Empty;     

        public bool EmailIsVerified { get; set; } = false;
        
        public string? EmailVerificationToken { get; set; }

        public DateTime? EmailVerifiedAt { get; set; }
    }
}