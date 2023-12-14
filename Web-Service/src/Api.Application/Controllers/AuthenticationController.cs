using System.Net;
using System.Security.Authentication;
using System.Security.Claims;
using Api.Domain.Dtos.Login;
using Api.Domain.Interfaces.Services.Login;
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
        
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<Guid>> Register(RegisterDtoRequest request)
        {
            try
            {
                var response = await _service.Register(request);            
                return Ok(response);
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

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginDtoResult>> Login(LoginDtoRequest request)
        {
            try
            {
                var response = await _service.Login(request.Email, request.Password);
                return Ok(response);
            }
            catch( AuthenticationException)
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

        [HttpPost("logout")]
        public async Task<ActionResult<bool>> Logout()
        {
            return Ok(await _service.Logout());
        }

        [HttpPost("change-password")]
        public async Task<ActionResult<bool>> ChangePassword([FromBody] string newPassword)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if  (userId ==null){
                return NotFound("Not found.");
            }
            var response = await _service.ChangePassword(userId, newPassword);           

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> RefreshToken([FromBody]RefreshTokenDtoRequest request)
        {
            try
            {
                var token = await _service.RefreshToken(request);
                return Ok(token);
            }
            catch(Exception ex)
            {
                return Unauthorized(ex.Message);
            }            
        }
    }
}