using Api.Core.Events.Messaging;
using Api.Domain.Entities;
using Api.Domain.Events.Purchase;
using Api.Domain.Interface;
using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Domain.Commands.PurchaseCommands
{
    public class CreatePurchaseCommandHandler : CommandHandler, IRequestHandler<CreatePurchaseCommand, ValidationResult>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IProductRepository _productRepository;

        public CreatePurchaseCommandHandler(IPurchaseRepository purchaseRepository, IProductRepository productRepository)
        {
            _purchaseRepository = purchaseRepository;
            _productRepository = productRepository;
        }

        public async Task<ValidationResult> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var purchasedProducts = new List<PurchasedProduct>();
            foreach (var item in request.PurchasedProducts)
            {
                // Opcional: validar se o produto existe
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                {
                    AddError($"Produto {item.ProductId} não encontrado.");
                    return ValidationResult;
                }
                purchasedProducts.Add(new PurchasedProduct
                {
                    Id = Guid.NewGuid(),
                    ProductId = item.ProductId,
                    Amount = item.Amount,
                    Price = item.Price,
                    Product = product
                });
            }

            var purchase = new Purchase
            {
                Id = Guid.NewGuid(),
                PurchasedProducts = purchasedProducts,
                IsComplete = false
            };

            _purchaseRepository.Add(purchase);

            purchase.AddDomainEvent(new CreatedPurchaseEvent(purchase.Id));

            return await Commit(_purchaseRepository.UnitOfWork);
        }
    }
} 