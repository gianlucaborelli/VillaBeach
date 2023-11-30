using System.Net;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/users/settings")]
    [ApiController]
    [Authorize]
    public class UsersSettingsController : ControllerBase
    {
        private readonly ILogger<UsersSettingsController> _logger;
        private readonly IUserSettingsService _service;

        public UsersSettingsController(IUserSettingsService service, ILogger<UsersSettingsController> logger)
        {
            _logger = logger;
            _service = service;
            _logger.LogInformation("Users controller called ");
        }

        [HttpPost("{key}/value={value}")]                
        public async Task<ActionResult> UpdateSettings(string key, string value )
        {
            _logger.LogInformation("Users UpdateSettings method Starting.");

            try
            {
                return Ok(await _service.UpdateSetting(key, int.Parse(value)));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}