namespace Api.CrossCutting.Identity.Authentication.Model
{
    public class RefreshToken
    {        
        public string Token { get; set; } = string.Empty;
        public DateTime? Expires { get; set; }
        public DateTime Created { get; set; }
    }
}