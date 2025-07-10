using System.Net;
using Api.Domain.Dtos.Product;
using Api.Domain.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var products = await _repository.GetAllAsync();
                var result = _mapper.Map<IEnumerable<ProductDto>>(products);
                return Ok(result);
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
                var result = await _repository.FindByName(name);

                if (result == null)
                    return NotFound();

                var mappedResult = _mapper.Map<IEnumerable<ProductDto>>(result);
                return Ok(mappedResult);
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
                var products = await _repository.GetAllAsync();
                var result = _mapper.Map<IEnumerable<ProductDtoAvailableResult>>(products);
                return Ok(result);
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
                var product = await _repository.GetByIdAsync(id);
                if (product == null)
                    return NotFound();

                var result = _mapper.Map<ProductDto>(product);
                return Ok(result);
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
                // TODO: Implement product creation command when available
                // For now, returning a placeholder response
                return BadRequest("Product creation not implemented yet. Please implement ProductCommands first.");
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
                // TODO: Implement product update command when available
                // For now, returning a placeholder response
                return BadRequest("Product update not implemented yet. Please implement ProductCommands first.");
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
                // TODO: Implement product deletion command when available
                // For now, returning a placeholder response
                return BadRequest("Product deletion not implemented yet. Please implement ProductCommands first.");
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}