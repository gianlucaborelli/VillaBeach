using System.Net;
using Api.Core.Mediator;
using Api.Domain.Commands.ProductCommands;
using Api.Domain.Dtos.Product;
using Api.Domain.Interface;
using Api.CrossCutting.Mappings.StaticMappers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMediatorHandler _mediatorHandler;

        public ProductsController(IProductRepository repository, IMediatorHandler mediatorHandler)
        {
            _repository = repository;
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var products = await _repository.GetAllAsync();
                var result = products.ToDtoList();
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

                var mappedResult = result.ToDtoList();
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
                var result = products.ToAvailableResultList();
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

                var result = product.ToDto();
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("GetByBarCode", Name = "GetProductByBarCode")]
        public async Task<ActionResult> GetByBarCode([FromQuery]string barCode)
        {
            try
            {
                var product = await _repository.FindByBarCode(barCode);
                if (product is null)
                    return NotFound();

                var result = product.ToDto();
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
                var command = new CreateProductCommand(
                    product.Name,
                    product.Description,
                    product.BarCode,
                    product.Price,
                    product.Stock
                );

                var result = await _mediatorHandler.SendCommand(command);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors.Select(e => e.ErrorMessage));
                }

                var createdProduct = await _repository.FindByName(product.Name);
                var mappedResult = createdProduct?.FirstOrDefault()?.ToCreateResult();
                
                return CreatedAtRoute("GetProductWithId", new { id = mappedResult.Id }, mappedResult);
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
                var command = new UpdateProductCommand(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.BarCode,
                    product.Price,
                    product.Stock
                );

                var result = await _mediatorHandler.SendCommand(command);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors.Select(e => e.ErrorMessage));
                }

                var updatedProduct = await _repository.GetByIdAsync(product.Id);
                var mappedResult = updatedProduct?.ToUpdateResult();
                
                return Ok(mappedResult);
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
                var command = new DeleteProductCommand(id);

                var result = await _mediatorHandler.SendCommand(command);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors.Select(e => e.ErrorMessage));
                }

                return NoContent();
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}