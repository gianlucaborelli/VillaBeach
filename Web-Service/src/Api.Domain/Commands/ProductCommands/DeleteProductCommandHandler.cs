using Api.Core.Events.Messaging;
using Api.Domain.Commands.ProductCommands;
using Api.Domain.Events.Product;
using Api.Domain.Interface;
using FluentValidation.Results;
using MediatR;

namespace Api.Domain.Commands.ProductCommands
{
    public class DeleteProductCommandHandler(
        IProductRepository productRepository) : CommandHandler,
        IRequestHandler<DeleteProductCommand, ValidationResult>
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<ValidationResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product is null)
            {
                AddError("O produto não existe.");
                return ValidationResult;
            }

            _productRepository.Delete(product.Id);

            product.AddDomainEvent(new DeletedProductEvent(
                product.Id,
                product.Name
            ));

            return await Commit(_productRepository.UnitOfWork);
        }
    }
} 