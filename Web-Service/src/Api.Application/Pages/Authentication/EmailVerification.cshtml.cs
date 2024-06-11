using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Api.Application.Pages.Authentication
{
    public class EmailVerificationModel : PageModel
    {
        private readonly IConfiguration _config;

        public EmailVerificationModel(IConfiguration config)
        {
            _config = config;
        }

        public bool Confirmed { get; set; }
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGet(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Error");
            }

            var apiUrl = $"{_config["Host:Url"]}/api/users/verify_email";
            var client = new HttpClient();

            var requestData = new
            {
                Email = email,
                Token = token
            };

            var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            try
            {
                response = await client.PostAsync(apiUrl, content);
            }
            catch (HttpRequestException e)
            {
                Confirmed = false;
                ErrorMessage = e.Message;
                return Page();
            }

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse>(responseData);
                Confirmed = result!.IsValid;
            }
            else
            {
                Confirmed = false;                
                ErrorMessage = "Erro na validação.";
            }

            return Page();
        }

        private class ApiResponse
        {
            public bool IsValid { get; set; }
            public string? ErrorMessage { get; set; }
        }
    }
}
