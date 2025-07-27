using Api.Core.Events.Messaging;
using Api.Domain.Commands.ProductCommands;
using Api.Domain.Entities;
using Api.Domain.Events.Product;
using Api.Domain.Interface;
using FluentValidation.Results;
using MediatR;

namespace Api.Domain.Commands.ProductCommands
{
    public class CreateProductCommandHandler(
        IProductRepository productRepository) : CommandHandler,
        IRequestHandler<CreateProductCommand, ValidationResult>
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<ValidationResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var existingProduct = await _productRepository.FindByName(request.Name);

            if (existingProduct != null && existingProduct.Any())
            {
                AddError("Já existe um produto com este nome.");
                return ValidationResult;
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                BarCode = request.BarCode,
                Price = request.Price,
                Stock = request.Stock
            };

            _productRepository.Add(product);

            product.AddDomainEvent(new CreatedProductEvent(
                product.Id,
                product.Name,
                product.Description,
                product.BarCode,
                product.Price,
                product.Stock
            ));

            return await Commit(_productRepository.UnitOfWork);
        }
    }
} 