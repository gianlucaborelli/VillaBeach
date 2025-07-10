using System.Net;
using Api.Domain.Dtos.Purchase;
using Api.Domain.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseRepository _repository;
        private readonly IMapper _mapper;

        public PurchaseController(IPurchaseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var purchases = await _repository.GetAllAsync();
                var result = _mapper.Map<IEnumerable<PurchaseDto>>(purchases);
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

                var result = _mapper.Map<PurchaseDto>(purchase);
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
                // TODO: Implement purchase creation command when available
                return BadRequest("Purchase creation not implemented yet. Please implement PurchaseCommands first.");
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
                // TODO: Implement purchase update command when available
                return BadRequest("Purchase update not implemented yet. Please implement PurchaseCommands first.");
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
                // TODO: Implement purchase deletion command when available
                return BadRequest("Purchase deletion not implemented yet. Please implement PurchaseCommands first.");
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}