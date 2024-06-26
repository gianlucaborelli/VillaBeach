using System.Net;
using Api.Domain.Dtos.User;
using Api.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _service;

        public UsersController(IUserService service, ILogger<UsersController> logger)
        {
            _logger = logger;
            _service = service;
            _logger.LogInformation("Users controller called ");
        }

        [HttpGet]                
        public async Task<ActionResult> GetAll()
        {
            _logger.LogInformation("Users getAll method Starting.");

            try
            {
                return Ok(await _service.GetAll());
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("{id}", Name = "GetUserWithId")]
        public async Task<ActionResult> Get(Guid id)
        {
            _logger.LogInformation("Users Get method Starting.");

            try
            {
                return Ok(await _service.Get(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet("findByName={name}")]
        public async Task<IActionResult> FindByName(string name)
        {
            _logger.LogInformation("Users findByName method Starting.");

            if (string.IsNullOrWhiteSpace(name))
                return BadRequest();

            try
            {
                var result = await _service.FindByName(name);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDtoCreate user)
        {
            _logger.LogInformation("Users Post method Starting.");

            try
            {
                var result = await _service.Post(user);

                if (result != null)
                {
                    return Created(new Uri(Url.Link("GetUserWithId", new { id = result.Id })!), result);
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

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserDtoUpdateRequest user)
        {
            _logger.LogInformation("Users Put method Starting.");

            try
            {
                var result = await _service.Put(user);

                if (result != null)
                {
                    return Ok(result);
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

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>An <see cref="ActionResult"/> representing the result of the delete operation.</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            _logger.LogInformation("Users Delete method Starting.");

            try
            {
                return Ok( _service.Delete(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}