namespace Api.Domain.Models
{
    public class UserAuthenticationModel
    {
        private byte[] _passwordHash = new byte[32];
        public byte[] PasswordHash
        {
            get { return _passwordHash; }
            set { _passwordHash = value; }
        }

        private byte[] _passwordSalt = new byte[32];
        public byte[] PasswordSalt
        {
            get { return _passwordSalt; }
            set { _passwordSalt = value; }
        }

        private string? _forgotPasswordToken;
        public string? ForgotPasswordToken
        {
            get { return _forgotPasswordToken; }
            set { _forgotPasswordToken = value; }
        }
        
        private DateTime? _forgotPasswordExpires;
        public DateTime? ForgotPasswordExpires
        {
            get { return _forgotPasswordExpires; }
            set { _forgotPasswordExpires = value; }
        }        

        private string _refreshToken = string.Empty;
        public string RefreshToken
        {
            get { return _refreshToken; }
            set { _refreshToken = value; }
        }                

        private DateTime? _refreshTokenExpires;
        public DateTime? RefreshTokenExpires
        {
            get { return _refreshTokenExpires; }
            set { _refreshTokenExpires = value; }
        }        

        private string _role = RolesModels.Customer;
        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }  
    }
}