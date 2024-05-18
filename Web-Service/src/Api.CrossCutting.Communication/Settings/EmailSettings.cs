namespace Api.CrossCutting.Communication.Settings
{
    public class EmailSettings
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string DisplayName { get; set; }= string.Empty;
        public string Email { get; set; }= string.Empty;
        public string Password { get; set; } = Environment.GetEnvironmentVariable("BREVO_KEY")
                                                ?? throw new ApplicationException("Smtp key is not configured."); 
    }
}