using System.Net;
using Api.Domain.Dtos.User;
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
            _logger.LogInformation("Users Settings controller called ");
        }

        /// <summary>
        /// Updates user settings with the provided key-value pair.
        /// Requires authentication.
        /// </summary>
        /// <remarks>
        /// This endpoint allows authenticated users to update their settings by providing a JSON payload
        /// containing the key and value for the desired update. The request is processed asynchronously,
        /// and the result is returned with an HTTP OK status code upon successful update.
        /// </remarks>
        /// <param name="request">The DTO containing the key and value for the update.</param>
        /// <returns>
        ///   <para>HTTP OK (200) - Successful update with the updated result.</para>
        ///   <para>HTTP Bad Request (400) - If the provided data is invalid or the update fails.</para>
        /// </returns>
        [HttpPut()]
        public async Task<ActionResult> UpdateSettings([FromBody] UserSettingsUpdateRequestDto request)
        {
            _logger.LogInformation("Users UpdateSettings method Starting.");

            try
            {
                return Ok(await _service.UpdateSetting(request.Key, request.Value));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}