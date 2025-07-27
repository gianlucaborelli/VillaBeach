using Api.Core.Events.Messaging;
using FluentValidation;

namespace Api.Domain.Commands.ProductCommands
{
    public class DeleteProductCommand : Command
    {
        public Guid Id { get; set; }

        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class DeleteProductCommandValidation : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Id é obrigatório.");
        }
    }
} 