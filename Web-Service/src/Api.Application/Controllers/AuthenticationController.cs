using System.Net;
using System.Security.Authentication;
using Api.Application.Controllers.Abstraction;
using Api.CrossCutting.Identity.Authentication;
using Api.CrossCutting.Identity.Authentication.Model;
using Api.CrossCutting.Identity.JWT.Manager;
using Api.CrossCutting.Identity.User;
using Api.Domain.Dtos.Authentication;
using Api.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    /// <summary>
    /// This controller contains endpoints used for user authentication and account control.
    /// </summary>
    [Route("api/users")]    
    public class AuthenticationController : ApiController
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationService _authService;
        private readonly SignInManager<AppUser> _signInManager;        
        private readonly UserManager<AppUser> _userManager;
        private readonly ILoggedInUser _loggedInUser;
        private readonly IJwtAuthManager _jwtManager;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            IAuthenticationService authService,
            ILoggedInUser loggedInUser,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IJwtAuthManager jwtManager)
        {
            _logger = logger;
            _jwtManager = jwtManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _authService = authService;
            _loggedInUser = loggedInUser;
            _logger.LogInformation("Login controller called");
        }

        /// <summary>
        /// Registers a new user identity for the application. This endpoint is accessible without authentication.
        /// </summary>        
        /// <param name="registerRequest">A data transfer object (DTO) containing the registration information for the new user.</param>
        /// <returns>
        ///   <para>HTTP 204 (OK) response if the registration is successful.</para>
        ///   <para>HTTP 409 (Conflict) response if a conflict, such as duplicate registration, occurs.</para>
        ///   <para>HTTP 500 (Internal Server Error) response for other application-related exceptions.</para>
        /// </returns>
        /// <remarks>
        /// This endpoint is intended for user self-registration from the client application.
        /// If registration fails due to duplicate user information, it returns Conflict with the corresponding error message.
        /// In case of application errors, it returns InternalServerError with the appropriate error message.
        /// </remarks>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterDtoRequest registerRequest)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _authService.Register(registerRequest);

            if (result.IsValid)
                return CustomResponse();

            foreach (var error in result.Errors)
                AddError(error.ErrorMessage);

            return CustomResponse();
        }

        /// <summary>
        /// Verifies a user's email address using the provided verification token.
        /// This HTTP GET endpoint is accessible without authentication.
        /// </summary>
        /// <param name="token">The verification token associated with the user's email address.</param>
        /// <returns>
        ///   <para>HTTP redirection to "/EmailVerification.html" with a status parameter set to "true" if email verification is successful.</para>
        ///   <para>HTTP redirection to "/EmailVerification.html" with a status parameter set to "false" and an error type parameter if a security token exception occurs during the process.</para>
        /// </returns>
        [HttpGet("verify_email")]
        [AllowAnonymous]
        public async Task<ActionResult> EmailVerification([FromQuery] string token)
        {
            string[] request = token.Split(",");

            var user = await _userManager.FindByEmailAsync(request[0]);

            var result = await _userManager.ConfirmEmailAsync(user!, WebUtility.UrlDecode(request[1]));

            if (result.Succeeded)
                return Redirect("/EmailVerification.html".Replace("STATUS", "true"));

            return Redirect("/EmailVerification.html".Replace("STATUS", "false"));
        }

        /// <summary>
        /// Authenticates a user by validating the provided login credentials.
        /// This HTTP POST endpoint is accessible without authentication.
        /// </summary>
        /// <param name="requestDto">A data transfer object (DTO) containing the user's email and password for authentication.</param>
        /// <returns>
        ///   <para>HTTP 200 (OK) response with a login result if authentication is successful.</para>
        ///   <para>HTTP 400 (Bad Request) response with an error message if the provided user or password is incorrect.</para>
        /// </returns>   
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginDtoRequest requestDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = await _userManager.FindByEmailAsync(requestDto.Email);
            var role = await _userManager.GetRolesAsync(user!) ?? throw new AuthenticationException();

            var result = await _signInManager.PasswordSignInAsync(user!.UserName!, requestDto.Password, false, true);

            if (result.Succeeded)
            {
                var accessToken = _jwtManager.GenerateAccessToken(user, role);
                var refreshToken = await _jwtManager.GenerateRefreshToken(user.Id);

                return CustomResponse(new { AccessToken = accessToken, RefreshToken = refreshToken });
            }

            if (result.IsLockedOut)
            {
                AddError("This user is temporarily blocked");
                return CustomResponse();
            }

            AddError("Incorrect user or password");
            return CustomResponse();
        }

        /// <summary>
        /// Refreshes the authentication token for a user by exchanging a valid refresh token.
        /// This HTTP POST endpoint is accessible without authentication.
        /// </summary>
        /// <param name="request">A data transfer object (DTO) containing the refresh token information.</param>
        /// <returns>
        ///   <para>HTTP 200 (OK) response with a new authentication token if the refresh token is valid.</para>
        ///   <para>HTTP 401 (Unauthorized) response with an error message if an exception occurs during the token refresh process.</para>
        /// </returns>
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenDtoRequest request)
        {
            var principal = _jwtManager.GetPrincipalFromExpiredToken(request.AccessToken);
            var result = await _jwtManager.ValidateRefresToken(request.RefreshToken, principal.GetUserId());
            

            if (result.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(principal.GetUserEmail());

                var role = await _userManager.GetRolesAsync(user!) ?? throw new AuthenticationException();

                var accessToken = _jwtManager.GenerateAccessToken(user!, role);
                var refreshToken = await _jwtManager.GenerateRefreshToken(user!.Id);

                return CustomResponse(new { AccessToken = accessToken, RefreshToken = refreshToken });
            }

            ICollection<string> errors = [];

            foreach (var error in result.Errors)
            {
                errors.Add(error.ErrorMessage);
            }

            return Unauthorized(errors);
        }

        /// <summary>
        /// Logs out the currently authenticated user.
        /// This HTTP POST endpoint requires the caller to be authenticated.
        /// </summary>
        /// <returns>
        ///   <para>HTTP 204 (No Content) response indicating successful logout.</para>
        /// </returns>
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }

        /// <summary>
        /// Changes the password for the currently authenticated user.
        /// This HTTP PUT endpoint requires the caller to be authenticated.
        /// </summary>
        /// <param name="request">The new password to be set for the user.</param>
        /// <returns>
        ///   <para>HTTP 200 (OK) response with a boolean indicating successful password change.</para>
        /// </returns>
        [HttpPut("change-password")]
        [Authorize]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = await _userManager.FindByEmailAsync(_loggedInUser.GetUserEmail());

            var result = await _userManager.ChangePasswordAsync(user!, request.CurrentPassword, request.NewPassword);

            if (result.Succeeded)
                return Ok();

            return BadRequest();
        }

        /// <summary>
        /// Initiates a password reset request for a user with the specified email address.
        /// This HTTP GET endpoint is accessible without authentication.
        /// </summary>
        /// <param name="userEmail">The email address of the user requesting a password reset.</param>
        /// <returns>
        ///   <para>HTTP 200 (OK) response if the password reset request is successful.</para>
        ///   <para>HTTP 400 (Bad Request) response with an error message if an exception occurs during the process.</para>
        /// </returns>
        [HttpGet("forgot-password-request")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> ForgotPassword([FromQuery] string userEmail)
        {
            try
            {
                //await _service.ForgotPasswordRequest(userEmail);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates the role of a user identified by the provided user ID.
        /// This HTTP PUT endpoint requires the caller to be authenticated with administrative privileges.
        /// </summary>
        /// <param name="request">A data transfer object (DTO) containing the user ID and the new role to be assigned.</param>
        /// <returns>
        ///   <para>HTTP 200 (OK) response if the role update is successful.</para>
        ///   <para>HTTP 400 (Bad Request) response with an error message if an exception occurs during the process.</para>
        /// </returns>
        [HttpPut("set-role")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<ActionResult> SetRule([FromBody] SetRoleDto request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (_loggedInUser.IsInRole("Admin") || _loggedInUser.IsInRole("SuperUser"))
            {
                var user = await _userManager.FindByEmailAsync(request.UserEmail);

                var result = await _userManager.AddToRoleAsync(user!, request.NewRole);

                if (result.Succeeded)
                    return Ok();

                return BadRequest();
            }

            return Forbid();
        }
    }
}