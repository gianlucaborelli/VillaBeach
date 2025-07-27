using Api.Core.Events.Messaging;
using FluentValidation;
using System;

namespace Api.Domain.Commands.PurchaseCommands
{
    public class DeletePurchaseCommand : Command
    {
        public Guid Id { get; set; }

        public DeletePurchaseCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeletePurchaseCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class DeletePurchaseCommandValidation : AbstractValidator<DeletePurchaseCommand>
        {
            public DeletePurchaseCommandValidation()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }
    }
} 