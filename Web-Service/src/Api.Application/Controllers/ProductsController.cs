using System.Net;
using Api.Domain.Dtos.Product;
using Api.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _service;

        public ProductsController(IProductService service)
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

        [HttpGet("findByName/{name}")]
        public async Task<IActionResult> FindByName(string name)
        {
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

        [HttpGet]
        [Route("Available")]
        public async Task<ActionResult> GetAllAvailableProducts()
        {
            try
            {
                return Ok(await _service.GetAvailableProducts());
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("{id}", Name = "GetProductWithId")]
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

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDtoCreateRequest product)
        {
            try
            {
                var result = await _service.Post(product);

                if (result != null)
                {
                    return Created(new Uri(Url.Link("GetProductWithId", new { id = result.Id })!), result);
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
        public async Task<ActionResult> Put([FromBody] ProductDtoUpdateRequest product)
        {
            try
            {
                var result = await _service.Put(product);

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