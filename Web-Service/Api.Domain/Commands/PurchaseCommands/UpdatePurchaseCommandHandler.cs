using Api.Core.Events.Messaging;
using Api.Domain.Entities;
using Api.Domain.Events.Purchase;
using Api.Domain.Interface;
using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Domain.Commands.PurchaseCommands
{
    public class UpdatePurchaseCommandHandler : CommandHandler, IRequestHandler<UpdatePurchaseCommand, ValidationResult>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IProductRepository _productRepository;

        public UpdatePurchaseCommandHandler(IPurchaseRepository purchaseRepository, IProductRepository productRepository)
        {
            _purchaseRepository = purchaseRepository;
            _productRepository = productRepository;
        }

        public async Task<ValidationResult> Handle(UpdatePurchaseCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var purchase = await _purchaseRepository.GetByIdAsync(request.Id);
            if (purchase == null)
            {
                AddError("Purchase não encontrada.");
                return ValidationResult;
            }

            var purchasedProducts = new List<PurchasedProduct>();
            foreach (var item in request.PurchasedProducts)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                {
                    AddError($"Produto {item.ProductId} não encontrado.");
                    return ValidationResult;
                }
                purchasedProducts.Add(new PurchasedProduct
                {
                    Id = System.Guid.NewGuid(),
                    ProductId = item.ProductId,
                    Amount = item.Amount,
                    Price = item.Price,
                    Product = product
                });
            }

            purchase.PurchasedProducts = purchasedProducts;

            _purchaseRepository.Update(purchase);

            purchase.AddDomainEvent(new UpdatedPurchaseEvent(purchase.Id));

            return await Commit(_purchaseRepository.UnitOfWork);
        }
    }
} 