using System.Net;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Api.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/users/address")]
    [ApiController]
    [Authorize(Roles = RolesModels.Admin)]
    public class UserAddressController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserAddressService _service;

        public UserAddressController(IUserAddressService service, ILogger<UsersController> logger)
        {
            _logger = logger;
            _service = service;
            _logger.LogInformation("Users controller called ");
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserAddressDtoCreateRequest user)
        {
            _logger.LogInformation("Users Post method Starting.");

            try
            {
                var result = await _service.Post(user);

                if (result != null)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


    }
}