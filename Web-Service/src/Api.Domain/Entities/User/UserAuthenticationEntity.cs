namespace Api.Domain.Entities
{
    public class UserAuthenticationEntity
    {
        public byte[] PasswordHash { get; set; } = new byte[32];

        public byte[] PasswordSalt { get; set; } = new byte[32];        

        public string? ForgotPasswordToken { get; set; }

        public DateTime? ForgotPasswordExpires { get; set; }

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime? RefreshTokenExpires {get; set;}

        public string Role { get; set; } = RolesModels.Customer;
    }
}