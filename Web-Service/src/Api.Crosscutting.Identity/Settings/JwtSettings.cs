using System.Text;
using Microsoft.AspNetCore.DataProtection;

namespace Api.CrossCutting.Identity.JWT.Settings
{
    public class JwtSettings
    {
        public byte[] Secret { get; set; } = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("VILLABEACH_TOKEN_KEY") 
                                            ?? throw new ApplicationException("Token key is not configured."));
        public int AccessTokenExpireInMinutes { get; set; }
        public int RefreshTokenExpireInMinutes { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
    }        
}