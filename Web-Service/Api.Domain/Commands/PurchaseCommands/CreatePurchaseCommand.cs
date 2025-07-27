using Api.Core.Events.Messaging;
using Domain.Dtos.Product;
using FluentValidation;

namespace Api.Domain.Commands.PurchaseCommands
{
    public class CreatePurchaseCommand : Command
    {
        public List<PurchasedProductDto> PurchasedProducts { get; set; } = new List<PurchasedProductDto>();

        public CreatePurchaseCommand() { }
        public CreatePurchaseCommand(List<PurchasedProductDto> purchasedProducts)
        {
            PurchasedProducts = purchasedProducts;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreatePurchaseCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }      

        public class CreatePurchaseCommandValidation : AbstractValidator<CreatePurchaseCommand>
        {
            public CreatePurchaseCommandValidation()
            {
                RuleFor(x => x.PurchasedProducts)
                    .NotNull().WithMessage("PurchasedProducts is required")
                    .Must(x => x.Count > 0).WithMessage("At least one product is required");
                RuleForEach(x => x.PurchasedProducts).SetValidator(new PurchasedProductDtoValidator());
            }
        }

        public class PurchasedProductDtoValidator : AbstractValidator<PurchasedProductDto>
        {
            public PurchasedProductDtoValidator()
            {
                RuleFor(x => x.ProductId).NotEmpty();
                RuleFor(x => x.Amount).GreaterThan(0);
                RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            }
        }
    }
} 