using System.Net;
using System.Security.Authentication;
using System.Security.Claims;
using Api.Domain.Dtos.Login;
using Api.Domain.Interfaces.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        /// Registers a new user from the application.
        /// </summary>
        /// <param name="request">Object containing registration details.</param>
        /// <returns>An ActionResult containing the unique identifier (Guid) for the newly registered user on success.</returns>
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
        /// Performs user authentication.
        /// </summary>
        /// <param name="request">Object containing login credentials (email and password).</param>
        /// <returns>An ActionResult containing a LoginDtoResult object on successful authentication.</returns>
        /// <remarks>
        /// If invalid credentials are provided, it returns BadRequest with the message "Wrong user or password.".
        /// In case of application errors, it returns InternalServerError with the corresponding message.
        /// </remarks>    
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
        /// Logs the current user out.
        /// </summary>
        /// <returns>An ActionResult indicating the success of the logout operation (true if successful).</returns>
        /// <remarks>
        /// This endpoint is used to log out the currently authenticated user.
        /// It returns Ok with a boolean value indicating the success of the logout operation.
        /// </remarks>
        [HttpPost("logout")]
        public async Task<ActionResult<bool>> Logout()
        {
            return Ok(await _service.Logout());
        }

        /// <summary>
        /// Changes the password for the currently logged-in user.
        /// </summary>
        /// <param name="newPassword">The new password to be set for the user.</param>
        /// <returns>An ActionResult indicating the success of the password change operation (true if successful).</returns>
        /// <remarks>
        /// This endpoint is used to change the password for the user currently logged in.
        /// It returns Ok with a boolean value indicating the success of the password change operation.        
        /// </remarks>
        [HttpPut("change-password")]
        public async Task<ActionResult<bool>> ChangePassword([FromBody] string newPassword)
        {            
            var response = await _service.ChangePassword(newPassword);

            return Ok(response);
        }

        /// <summary>
        /// Refreshes an expired Access Token using a Refresh Token.
        /// </summary>
        /// <param name="request">Object containing the expired Access Token and Refresh Token for authentication.</param>
        /// <returns>An ActionResult containing both the new Access Token and Refresh Token on successful refresh.</returns>
        /// <remarks>
        /// This endpoint is used to refresh an expired Access Token using a valid Refresh Token.
        /// It returns Ok with both the new Access Token and Refresh Token on success.
        /// If the Refresh Token is invalid or the operation fails, it returns Unauthorized with an error message.
        /// </remarks>
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
    }
}