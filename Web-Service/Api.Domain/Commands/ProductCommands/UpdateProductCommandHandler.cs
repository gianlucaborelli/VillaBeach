using Api.Core.Events.Messaging;
using Api.Domain.Commands.ProductCommands;
using Api.Domain.Events.Product;
using Api.Domain.Interface;
using FluentValidation.Results;
using MediatR;

namespace Api.Domain.Commands.ProductCommands
{
    public class UpdateProductCommandHandler(
        IProductRepository productRepository) : CommandHandler,
        IRequestHandler<UpdateProductCommand, ValidationResult>
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<ValidationResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product is null)
            {
                AddError("O produto não existe.");
                return ValidationResult;
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.BarCode = request.BarCode;
            product.Price = request.Price;
            product.Stock = request.Stock;

            _productRepository.Update(product);

            product.AddDomainEvent(new UpdatedProductEvent(
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