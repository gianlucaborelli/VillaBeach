using System.Security.Claims;
using Api.Domain;
using Api.Domain.Dtos.Login;
using Api.Domain.Interfaces.Services.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<LoginController> _logger;
        private readonly ILoginService _service;

        public LoginController(ILoginService service, ILogger<LoginController> logger)
        {
            _logger = logger;
            _service = service;
            _logger.LogInformation("Login controller called");
            
        }

        [HttpPost("register")]
        public async Task<ActionResult<Guid>> Register(RegisterDtoRequest request)
        {
            var response = await _service.Register(request);
            
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginDtoResult>> Login(LoginDtoRequest request)
        {
            var response = await _service.Login(request.Email, request.Password);           

            return Ok(response);
        }

        [HttpPost("change-password"), Authorize]
        public async Task<ActionResult<bool>> ChangePassword([FromBody] string newPassword)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if  (userId ==null){
                throw new ApplicationException("Not found.");
            }
            var response = await _service.ChangePassword(userId, newPassword);           

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken([FromBody]RefreshTokenDtoRequest request)
        {
            var token = await _service.RefreshToken(request);

            return Ok(token);
        }
    }
}