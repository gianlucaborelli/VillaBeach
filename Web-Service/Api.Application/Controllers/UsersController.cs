using System.Net;
using Api.Application.Controllers.Abstraction;
using Api.Domain.Dtos.User;
using Api.Core.Mediator;
using Api.Domain.Commands.UserCommands;
using Api.Domain.Interface;
using Api.CrossCutting.Mappings.StaticMappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    /// <summary>
    /// Controller for managing users.
    /// </summary>
    [Route("api/users")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class UsersController : ApiController
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMediatorHandler _mediator;
        private readonly IUserRepository _userRepository;

        public UsersController(
            IMediatorHandler mediator, 
            IUserRepository userRepository,
            ILogger<UsersController> logger)
        {
            _logger = logger;
            _mediator = mediator;
            _userRepository = userRepository;
            _logger.LogInformation("Users controller called ");
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns> An <see cref="UserView"/> representing the result of the operation.</returns>
        /// <response code="200"> The list of users is returned.</response>
        /// <response code="401"> If the user is not authenticated.</response>
        /// <response code="500"> If an internal server error occurs.</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Users GetAll method Starting.");

            var users = await _userRepository.GetAllAsync();
            var result = users.ToViewList();
            return Ok(result);
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> An <see cref="UserView"/> representing the result of the operation.</returns>
        /// <response code="200"> The user is returned.</response>
        /// <response code="401"> If the user is not authenticated.</response>
        /// <response code="500"> If an internal server error occurs.</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation("Users Get method Starting.");

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                AddError("User not found.");
                return CustomResponse();
            }

            var result = user.ToView();
            return Ok(result);
        }

        /// <summary>
        /// Finds a user by name.
        /// </summary>
        /// <param name="name"> The name of the user to find.</param>
        /// <returns> An <see cref="UserView"/> representing the result of the operation.</returns>
        /// <response code="200"> The user is returned.</response>
        /// <response code="401"> If the user is not authenticated.</response>
        /// <response code="500"> If an internal server error occurs.</response>
        [HttpGet("find-by-name")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            _logger.LogInformation("Users findByName method Starting.");

            var users = await _userRepository.GetByNameAsync(name);
            var result = users.ToViewList();
            return Ok(result);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <remarks> 
        /// 
        /// Sample request: 
        /// 
        ///     POST /api/users 
        ///     {
        ///         "name": "Joel Miller",
        ///         "email": "joel.miller@lastmail.com
        ///     }
        /// 
        /// </remarks>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserRequest user)
        {
            _logger.LogInformation("Users Post method Starting.");

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var command = user.ToCreateCommand();
            var result = await _mediator.SendCommand(command);

            if (result.IsValid)
                return Created();

            return CustomResponse(result);
        }

        /// <summary>
        /// Updates a user by ID. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <remarks> 
        /// 
        /// Sample request: 
        /// 
        ///     PUT /api/users/6a567499-7a3a-4b01-8007-ad53e84f217e
        ///     {
        ///         "id": "6a567499-7a3a-4b01-8007-ad53e84f217e",
        ///         "name": "Joel Miller",
        ///         "email": "joel.miller@lastmail.com
        ///     }
        /// 
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateUserRequest user)
        {
            _logger.LogInformation("Users Put method Starting.");

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!await _userRepository.ExistAsync(id))
            {
                AddError("User not found.");
                return CustomResponse();
            }

            var command = user.ToUpdateCommand();
            var result = await _mediator.SendCommand(command);

            if (result.IsValid)
                return Ok();

            return CustomResponse(result);
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>An <see cref="ActionResult"/> representing the result of the delete operation.</returns>
        /// <remarks> 
        /// 
        /// Sample request: 
        /// 
        ///     DELETE /api/users/6a567499-7a3a-4b01-8007-ad53e84f217e
        /// 
        /// </remarks> 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Users Delete method Starting.");

            if (!await _userRepository.ExistAsync(id))
            {
                AddError("User not found.");
                return CustomResponse();
            }

            var command = new DeleteUserCommand(id);
            var result = await _mediator.SendCommand(command);

            if (result.IsValid)
                return Ok();

            return CustomResponse(result);
        }

        [HttpPost("{userId}/address")]
        public async Task<IActionResult> AddAddress(Guid userId, [FromBody] AddAddressRequest address)
        {
            _logger.LogInformation("Users AddAddress method Starting.");

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var command = address.ToAddAddressCommand();
            command.UserId = userId;
            var result = await _mediator.SendCommand(command);

            if (result.IsValid)
                return Created();

            return CustomResponse(result);
        }

        [HttpPut("{userId}/address/{addressId}")]
        public async Task<IActionResult> UpdateAddress(Guid userId, Guid addressId, [FromBody] UpdateAddressRequest address)
        {
            _logger.LogInformation("Users UpdateAddress method Starting.");

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var command = address.ToUpdateAddressCommand();
            command.UserId = userId;
            var result = await _mediator.SendCommand(command);

            if (result.IsValid)
                return Ok();

            return CustomResponse(result);
        }
    }
}