using Api.Core.Events.Messaging;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Api.Domain.Commands.PurchaseCommands
{
    public class UpdatePurchaseCommand : Command
    {
        public Guid Id { get; set; }
        public List<PurchasedProductData> PurchasedProducts { get; set; }

        public UpdatePurchaseCommand(Guid id, List<PurchasedProductData> purchasedProducts)
        {
            Id = id;
            PurchasedProducts = purchasedProducts;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdatePurchaseCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class PurchasedProductData
        {
            public Guid ProductId { get; set; }
            public int Amount { get; set; }
            public decimal Price { get; set; }
        }

        public class UpdatePurchaseCommandValidation : AbstractValidator<UpdatePurchaseCommand>
        {
            public UpdatePurchaseCommandValidation()
            {
                RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.PurchasedProducts)
                    .NotNull().WithMessage("PurchasedProducts is required")
                    .Must(x => x.Count > 0).WithMessage("At least one product is required");
                RuleForEach(x => x.PurchasedProducts).SetValidator(new PurchasedProductDataValidator());
            }
        }

        public class PurchasedProductDataValidator : AbstractValidator<PurchasedProductData>
        {
            public PurchasedProductDataValidator()
            {
                RuleFor(x => x.ProductId).NotEmpty();
                RuleFor(x => x.Amount).GreaterThan(0);
                RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            }
        }
    }
} 