using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Authentication
{
    /// <summary>
    /// Represents a request to register a new user.
    /// </summary>
    public class RegisterRequest
    {

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        /// <remarks>
        /// This property is required and cannot be null or empty.
        /// </remarks>
        /// <example>
        /// <code>
        /// var registerRequest = new RegisterRequest { Name = "John Doe" };
        /// </code>
        /// </example>
        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        /// <value>The email address of the user.</value>
        /// <remarks>
        /// This property is required and must be in a valid email format.
        /// </remarks>
        /// <example>
        /// <code>
        /// var registerRequest = new RegisterRequest { Email = "john.doe@example.com" };
        /// </code>
        /// </example>
        [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Invalid Email format")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        /// <value>The password of the user.</value>
        /// <remarks>
        /// This property is required and must have a minimum length of 6 characters.
        /// </remarks>
        /// <example>
        /// <code>
        /// var registerRequest = new RegisterRequest { Password = "password123" };
        /// </code>
        /// </example>
        [Required(ErrorMessage = "Password is required"), MinLength(6, ErrorMessage = "Password must have a minimum of 8 characters.")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the confirmation password of the user.
        /// </summary>
        /// <value>The confirmation password of the user.</value>
        /// <remarks>
        /// This property is required and should match the <see cref="Password"/> property.
        /// </remarks>
        /// <example>
        /// <code>
        /// var registerRequest = new RegisterRequest { ConfirmPassword = "password123" };
        /// </code>
        /// </example>
        public required string ConfirmPassword { get; set; }
    }
}