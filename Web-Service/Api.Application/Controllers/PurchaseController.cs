using System.Net;
using Api.Domain.Dtos.Purchase;
using Api.Domain.Interface;
using Api.CrossCutting.Mappings.StaticMappers;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Commands.PurchaseCommands;
using Api.Core.Mediator;
using FluentValidation.Results;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseRepository _repository;
        private readonly IMediatorHandler _mediator;

        public PurchaseController(IPurchaseRepository repository, IMediatorHandler mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var purchases = await _repository.GetAllAsync();
                var result = purchases.ToDtoList();
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("{id}", Name = "GetPurchaseWithId")]
        public async Task<ActionResult> Get(Guid id)
        {
            try
            {
                var purchase = await _repository.GetByIdAsync(id);
                if (purchase == null)
                    return NotFound();

                var result = purchase.ToDto();
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("incomplete")]
        public async Task<ActionResult> GetAllIncomplete()
        {
            try
            {
                // TODO: Implement when repository method is available
                return BadRequest("GetAllIncomplete not implemented yet in repository.");
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("incomplete/{userId}")]
        public async Task<ActionResult> GetAllIncompleteByUser(Guid userId)
        {
            try
            {
                // TODO: Implement when repository method is available
                return BadRequest("GetAllIncompleteByUser not implemented yet in repository.");
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet("findByUserId/{userId}")]
        public async Task<ActionResult> FindByUserId(Guid userId)
        {
            try
            {
                // TODO: Implement when repository method is available
                return BadRequest("FindByUserId not implemented yet in repository.");
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost("setComplete/{id}")]
        public async Task<ActionResult> SetComplete(Guid id)
        {
            try
            {
                // TODO: Implement purchase completion command when available
                return BadRequest("Purchase completion not implemented yet. Please implement PurchaseCommands first.");
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PurchaseDtoCreateRequest purchase)
        {
            try
            {
                var command = purchase.ToCreateCommand();
                ValidationResult result = await _mediator.SendCommand(command);
                if (!result.IsValid)
                    return BadRequest(result.Errors.Select(e => e.ErrorMessage));
                return Ok();
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] PurchaseDtoUpdateRequest purchase)
        {
            try
            {
                var command = purchase.ToUpdateCommand();
                ValidationResult result = await _mediator.SendCommand(command);
                if (!result.IsValid)
                    return BadRequest(result.Errors.Select(e => e.ErrorMessage));
                return Ok();
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeletePurchaseCommand(id);
                ValidationResult result = await _mediator.SendCommand(command);
                if (!result.IsValid)
                    return BadRequest(result.Errors.Select(e => e.ErrorMessage));
                return Ok();
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}