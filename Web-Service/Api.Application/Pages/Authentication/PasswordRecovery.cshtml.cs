using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Api.Application.Pages.Authentication
{
    public class PasswordRecoveryModel : PageModel
    {
        private readonly IConfiguration _config;

        public PasswordRecoveryModel(IConfiguration config)
        {
            _config = config;
        }

        [BindProperty]
        public bool IsSuccess { get; set; }

        [BindProperty]
        public string Message { get; set; } = string.Empty;

        [BindProperty]
        public required ResetPasswordModel Input { get; set; } = new();

        public IActionResult OnGet(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
                return BadRequest("A valid email and/or token must be provided.");

            TempData["Email"] = email;
            TempData["Token"] = token;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                IsSuccess = false;
                Message = "There are validation errors.";
                return Page();
            }

            var resetPasswordData = new
            {
                email = TempData["Email"] as string,
                token = TempData["Token"] as string,

                password = Input.Password,
                confirmPassword = Input.ConfirmPassword
            };

            var httpClient = new HttpClient();
            var content = new StringContent(JsonSerializer.Serialize(resetPasswordData), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"{_config["Host:Url"]}/api/users/forgot-password", content);

            if (response.IsSuccessStatusCode)
            {
                IsSuccess = true;
                Message = "Password reset successfully!";
                return Page();
            }

            IsSuccess = false;
            Message = "There was an error resetting your password. Please try again.";
            return Page();
        }
    }

    public class ResetPasswordModel
    {
        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
