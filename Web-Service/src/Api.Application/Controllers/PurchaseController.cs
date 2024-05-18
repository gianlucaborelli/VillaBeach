using System.Net;
using Api.Domain.Dtos.Purchase;
using Api.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private IPurchaseService _service;

        public PurchaseController(IPurchaseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
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
        [Route("{id}", Name = "GetPurchaseWithId")]
        public async Task<ActionResult> Get(Guid id)
        {
            try
            {
                return Ok(await _service.Get(id));
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
                return Ok(await _service.GetAllIncomplete());
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
                return Ok(await _service.GetAllIncompleteByUser(userId));
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
                return Ok(await _service.FindByUserId(userId));
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
                var result = await _service.SetPurchaseAsComplete(id);

                if (result != null)
                {
                    return Created(new Uri(Url.Link("GetPurchaseWithId", new { id = result.Id })!), result);
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

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PurchaseDtoCreateRequest purchase)
        {
            try
            {
                var result = await _service.Post(purchase);

                if (result != null)
                {
                    return Created(new Uri(Url.Link("GetPurchaseWithId", new { id = result.Id })!), result);
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
        public async Task<ActionResult> Put([FromBody] PurchaseDtoUpdateRequest purchase)
        {
            try
            {
                var result = await _service.Put(purchase);

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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                return Ok(await _service.Delete(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}