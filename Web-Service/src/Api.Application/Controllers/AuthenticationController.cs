using System.Net;
using System.Security.Authentication;
using System.Security.Claims;
using Api.Domain.Dtos.Login;
using Api.Domain.Interfaces.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Application.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {

        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationService _service;

        public AuthenticationController(IAuthenticationService service, ILogger<AuthenticationController> logger)
        {
            _logger = logger;
            _service = service;
            _logger.LogInformation("Login controller called");
        }

        /// <summary>
        /// Registers a new user with the provided registration details.
        /// This HTTP POST endpoint is accessible without authentication.
        /// </summary>
        /// <param name="request">A data transfer object (DTO) containing the registration information for the new user.</param>
        /// <returns>
        ///   <para>HTTP 200 (OK) response if the registration is successful.</para>
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
        public async Task<ActionResult<Guid>> Register(RegisterDtoRequest request)
        {
            try
            {
                var response = await _service.Register(request);
                return Ok();
            }
            catch (AuthenticationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
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
            try
            {
                await _service.EmailVerificationToken(token);
                return Redirect("/EmailVerification.html".Replace("STATUS", "true"));
            }
            catch (SecurityTokenException ex)
            {
                return Redirect("/EmailVerification.html"
                            .Replace("STATUS", "false")
                            .Replace("ERROR_TYPE", ex.Message));
            }
        }

        /// <summary>
        /// Authenticates a user by validating the provided login credentials.
        /// This HTTP POST endpoint is accessible without authentication.
        /// </summary>
        /// <param name="request">A data transfer object (DTO) containing the user's email and password for authentication.</param>
        /// <returns>
        ///   <para>HTTP 200 (OK) response with a login result if authentication is successful.</para>
        ///   <para>HTTP 400 (Bad Request) response with an error message if the provided user or password is incorrect.</para>
        ///   <para>HTTP 500 (Internal Server Error) response for other application-related exceptions.</para>
        /// </returns>   
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginDtoResult>> Login(LoginDtoRequest request)
        {
            try
            {
                var response = await _service.Login(request.Email, request.Password);
                return Ok(response);
            }
            catch (AuthenticationException)
            {
                return BadRequest("Wrong user or password.");
            }
            catch (ApplicationException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Logs out the currently authenticated user.
        /// This HTTP POST endpoint requires the caller to be authenticated.
        /// </summary>
        /// <returns>
        ///   <para>HTTP 200 (OK) response with a boolean indicating successful logout.</para>
        /// </returns>
        [HttpPost("logout")]
        public async Task<ActionResult<bool>> Logout()
        {
            return Ok(await _service.Logout());
        }

        /// <summary>
        /// Changes the password for the currently authenticated user.
        /// This HTTP PUT endpoint requires the caller to be authenticated.
        /// </summary>
        /// <param name="newPassword">The new password to be set for the user.</param>
        /// <returns>
        ///   <para>HTTP 200 (OK) response with a boolean indicating successful password change.</para>
        /// </returns>
        [HttpPut("change-password")]
        public async Task<ActionResult<bool>> ChangePassword([FromBody] string newPassword)
        {
            var response = await _service.ChangePassword(newPassword);

            return Ok(response);
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
        public async Task<ActionResult<RefreshTokenDtoResult>> RefreshToken([FromBody] RefreshTokenDtoRequest request)
        {
            try
            {
                var token = await _service.RefreshToken(request);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
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
        public async Task<ActionResult<RefreshTokenDtoResult>> ForgotPassword([FromQuery] string userEmail)
        {
            try
            {
                await _service.ForgotPasswordRequest(userEmail);
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
        [Authorize(Roles = RolesModels.Admin)]
        public async Task<ActionResult<RefreshTokenDtoResult>> SetRule([FromBody] SetRoleDto request)
        {
            try
            {
                await _service.SetRoler(request.UserId, request.NewRole);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Revokes a specific authorization or access by the provided identifier.
        /// This HTTP PUT endpoint requires the caller to be authenticated with administrative privileges.
        /// </summary>
        /// <param name="id">The unique identifier associated with the authorization or access to be revoked.</param>
        /// <returns>
        ///   <para>HTTP 200 (OK) response if the revocation is successful.</para>
        ///   <para>HTTP 400 (Bad Request) response with an error message if an exception occurs during the process.</para>
        /// </returns>
        [HttpPut("revoke")]
        [Authorize(Roles = RolesModels.Admin)]
        public async Task<ActionResult<RefreshTokenDtoResult>> Revoke([FromBody] Guid id)
        {
            try
            {
                await _service.Revoke(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Revokes all authorizations or accesses for users.
        /// This HTTP PUT endpoint requires the caller to be authenticated with administrative privileges.
        /// </summary>
        /// <returns>
        ///   <para>HTTP 200 (OK) response if the revocation of all authorizations is successful.</para>
        ///   <para>HTTP 400 (Bad Request) response with an error message if an exception occurs during the process.</para>
        /// </returns>
        [HttpPut("revoke-all")]
        [Authorize(Roles = RolesModels.Admin)]
        public async Task<ActionResult<RefreshTokenDtoResult>> RevokeAll()
        {
            try
            {
                await _service.RevokeAll();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}