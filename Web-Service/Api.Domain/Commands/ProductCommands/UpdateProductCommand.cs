using Api.Core.Events.Messaging;
using FluentValidation;

namespace Api.Domain.Commands.ProductCommands
{
    public class UpdateProductCommand : Command
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? BarCode { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public UpdateProductCommand(Guid id, string name, string? description, string? barCode, decimal price, int stock)
        {
            Id = id;
            Name = name;
            Description = description;
            BarCode = barCode;
            Price = price;
            Stock = stock;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Id é obrigatório.");

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