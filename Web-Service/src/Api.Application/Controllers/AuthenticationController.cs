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
    [Route("api/users/account")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class AuthenticationController : ApiController
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthenticationService _authService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtAuthManager _jwtManager;
        private readonly ILoggedInUser _loggedInUser;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            SignInManager<AppUser> signInManager,
            IAuthenticationService authService,
            UserManager<AppUser> userManager,
            IJwtAuthManager jwtManager,
            ILoggedInUser loggedInUser)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _authService = authService;
            _jwtManager = jwtManager;
            _loggedInUser = loggedInUser;
            _logger.LogInformation("Authentication controller called");
        }

        /// <summary>
        /// Registers a new user identity for the application. This endpoint is accessible without authentication.
        /// </summary>        
        /// <param name="request">A data transfer object (DTO) containing the registration information for the new user.</param>        
        /// <remarks>
        /// This endpoint is intended for user self-registration from the client application. 
        /// 
        /// The request body should contain the user's Full Name, email address, password, and password confirmation. 
        /// 
        /// The password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, 
        /// one number, and one special character. 
        /// 
        /// The password confirmation must match the password field.   
        /// 
        /// All fields are required.
        ///  
        /// Example:
        /// 
        ///     POST /register
        ///     {
        ///         "name": "Ellie Williams",
        ///         "email": "ellie.williams@lastmail.com",
        ///         "password": "1ValidPassword!",
        ///         "confirmPassword": "1ValidPassword!"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">The registration is successful.</response>
        /// <response code="400">If domain validation fails, such as an invalid password, 
        /// a password confirmation not matching, or an empty field.</response>
        /// <response code="409">If an existing user is found with the same email address.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is not null)
            {
                return Conflict("User already exists.");
            }

            var result = await _authService.Register(request);

            if (result.IsValid)
                return Created();

            foreach (var error in result.Errors)
                AddError(error.ErrorMessage);

            return CustomResponse();
        }

        /// <summary>
        /// Verifies the email address of a user by confirming the email verification token. This endpoint is accessible without authentication.
        /// </summary>
        /// <remarks>
        /// Verifies the email address using the confirmation token sent to the user's email. 
        /// 
        /// Example:
        /// 
        ///     POST /verify_email
        ///     {
        ///         "email": "ellie.williams@lastmail.com",
        ///         "token": "c7349562b8760745921eb6fdd6dc89cc113d82b684586fd324e6f844c3f371d8"
        ///     }
        /// 
        ///     <para> All fields are required. </para>
        /// 
        /// </remarks>
        /// <param name="request"> 
        ///    <para> Parameters for the request must include the user's email address and the confirmation token sent to the user's email for account verification. </para>
        /// </param> 
        /// <returns></returns>
        /// <response code="200">The email verification is successful.</response>
        /// <response code="400">The request is invalid or the email verification fails.</response>
        /// <response code="404">If the user is not found.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpPost("verify_email")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> EmailVerification([FromBody] EmailConfirmationRequest request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                AddError("User not found");
                return CustomResponse();
            }

            var result = await _userManager.ConfirmEmailAsync(user!, request.Token);

            if (result.Succeeded)
                return CustomResponse("Success");

            foreach (var error in result.Errors)
                AddError(error.Description);

            return CustomResponse();
        }

        /// <summary>
        /// Logs in a user with the provided email address and password. This endpoint is accessible without authentication.
        /// </summary>
        /// <param name="requestDto">
        ///     A data transfer object (DTO) containing the user's email address and password.
        /// </param>
        /// <remarks>
        /// This endpoint is used to authenticate a user with the provided email address and password.
        /// 
        /// The request body should contain the user's email address and password.
        /// 
        /// The email address and password are required fields.
        /// 
        /// The password must be at least 8 characters long and contain at least one uppercase letter, 
        ///     one lowercase letter, one number, and one special character.
        ///     
        /// Example:
        /// 
        ///     POST /login
        ///     {
        ///         "email": "ellie.williams@lastmail.com",
        ///         "token": "1ValidPassword!"
        ///     }
        /// 
        /// </remarks>
        /// <returns>
        ///     <para>HTTP 200 (OK) response with an access token and a refresh token if the login is successful.</para>
        /// </returns>
        /// <response code="200">The login is successful.</response>        
        /// <response code="400">If the request is invalid or the login fails.</response>
        /// <response code="404">If the user is not found.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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
        /// Refreshes the authentication token for the currently authenticated user.
        /// This endpoint is accessible without authentication.
        /// </summary>
        /// <remarks>
        /// This endpoint is used to refresh the authentication token for the currently authenticated user. 
        /// 
        /// Example:
        /// 
        ///     POST /refresh-token
        ///     {
        ///         "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJlbGxpZS53aWxsaWFtc0BkZW1vLmNvb.1"
        ///         "refreshToken": "c7349562b8760745921eb6fdd6dc89cc113d82b684586fd324e6f844c3f371d8" 
        ///     }
        /// 
        /// </remarks>
        /// <param name="request">A data transfer object (DTO) containing the refresh token information.</param>
        /// <returns>
        ///     <para>HTTP 200 (OK) response with a new access token and refresh token if the refresh is successful.</para>
        /// </returns>
        /// <response code="200"> The refresh is successful.</response>       
        /// <response code="401"> If the refresh token is invalid.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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
        /// This endpoint requires the caller to be authenticated.
        /// </summary>
        /// <returns>
        ///   <para>HTTP 204 (No Content) response indicating successful logout.</para>
        /// </returns>
        /// <response code="204"> The logout is successful.</response>
        [HttpPost("logout")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }

        /// <summary>
        /// Changes the password for the currently authenticated user.
        /// This endpoint requires the caller to be authenticated.
        /// </summary>
        /// <remarks>
        /// This endpoint is used to change the password for the currently authenticated user.
        /// 
        /// Example:
        /// 
        ///     PUT /change-password
        ///     {
        ///         "currentPassword": "1ValidPassword!",
        ///         "newPassword": "1NewValidPassword!",
        ///         "newPasswordConfirm": "1NewValidPassword!"
        ///     }
        /// 
        /// </remarks>
        /// <param name="request">The new password to be set for the user.</param>
        /// <returns>
        ///   <para>HTTP 200 (OK) response with a boolean indicating successful password change.</para>
        /// </returns>
        /// <response code="200"> The password change is successful.</response>
        /// <response code="400"> If the request is invalid or the password change fails.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpPut("change-password")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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
        /// Sends a password reset email to the user with the provided email address.
        /// This endpoint is accessible without authentication.
        /// </summary>
        /// <remarks>
        /// This endpoint is used to send a password reset email to the user with the provided email address.
        /// 
        /// Example:
        /// 
        ///     POST /forgot-password?Email=ellie.williams@lastmail.com
        /// 
        /// </remarks>
        /// <param name="request">The email address of the user requesting a password reset.</param>
        /// <returns>
        ///   <para>HTTP 200 (OK) response if the password reset request is successful.</para>
        ///   <para>HTTP 400 (Bad Request) response with an error message if an exception occurs during the process.</para>
        /// </returns>
        /// <response code="204"> The password reset request is successful.</response>
        /// <response code="400"> If the request is invalid or the password reset request fails.</response>
        /// <response code="500"> If an internal server error occurs.</response>
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> ForgotPassword([FromQuery] ForgotPasswordRequest request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _authService.ForgetPassword(request);

            if (result.IsValid) return NoContent();

            foreach (var error in result.Errors)
                AddError(error.ErrorMessage);

            return CustomResponse();
        }

        /// <summary>
        /// Verifies the password reset token and changes the password for the user.
        /// This endpoint is accessible without authentication.
        /// </summary>
        /// <remarks>
        /// This endpoint is used to verify the password reset token and change the password for the user.
        /// The request body should contain the token sent to the user's email, 
        /// the user's email address, the new password, and the new password confirmation.
        /// 
        /// Example:
        /// 
        ///     PUT /forgot-password
        ///     {
        ///         "token": "c7349562b8760745921eb6fdd6dc89cc113d82b684586fd324e6f844c3f371d8"
        ///         "email": "ellie.williams@lastmail.com",
        ///         "newPassword": "1NewValidPassword!",
        ///         "newPasswordConfirm": "1NewValidPassword!"
        ///     }
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200"> The password reset is successful.</response>
        /// <response code="400"> If the request is invalid or the password reset fails.</response>
        [HttpPut("forgot-password")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgetPasswordVerificationRequest request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _authService.ForgetPasswordVerification(request);

            if (result.IsValid) return Ok();

            foreach (var error in result.Errors)
                AddError(error.ErrorMessage);

            return CustomResponse();
        }

        /// <summary>
        /// Sets the role for a user with the provided email address.
        /// This endpoint requires the caller to be authenticated with the Admin or SuperAdmin role.
        /// </summary>
        /// <remarks> 
        /// This endpoint is used to set the role for a user with the provided email address.
        /// 
        /// Example:
        /// 
        ///     PUT /forgot-password
        ///     {
        ///         "userEmail": "ellie.williams@lastmail.com",
        ///         "newRole": "Admin"
        ///     }
        /// </remarks>
        /// <param name="request">A data transfer object (DTO) containing the user ID and the new role to be assigned.</param>        
        /// <response code="200"> The role is set successfully.</response>
        /// <response code="400"> If the request is invalid or the role assignment fails.</response>
        /// <response code="403"> If the caller is not authorized to set the role.</response>
        [HttpPut("set-role")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
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