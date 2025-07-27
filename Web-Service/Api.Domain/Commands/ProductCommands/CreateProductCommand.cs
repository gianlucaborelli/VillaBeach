using Api.Core.Events.Messaging;
using FluentValidation;

namespace Api.Domain.Commands.ProductCommands
{
    public class CreateProductCommand : Command
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? BarCode { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public CreateProductCommand(string name, string? description, string? barCode, decimal price, int stock)
        {
            Name = name;
            Description = description;
            BarCode = barCode;
            Price = price;
            Stock = stock;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CreateProductCommandValidation : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MaximumLength(50).WithMessage("Nome deve ter no máximo 50 caracteres.");

            RuleFor(c => c.Description)
                .MaximumLength(50).WithMessage("Descrição deve ter no máximo 50 caracteres.");

            RuleFor(c => c.BarCode)
                .NotEmpty().WithMessage("Código de barras é obrigatório.")
                .MaximumLength(15).WithMessage("Código de barras deve ter no máximo 15 caracteres.");

            RuleFor(c => c.Price)
                .GreaterThan(0).WithMessage("Preço deve ser maior que zero.");

            RuleFor(c => c.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Estoque deve ser maior ou igual a zero.");
        }
    }
} 