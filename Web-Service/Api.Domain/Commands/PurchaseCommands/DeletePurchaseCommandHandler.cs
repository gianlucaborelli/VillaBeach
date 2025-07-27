using Api.Core.Events.Messaging;
using Api.Domain.Entities;
using Api.Domain.Events.Purchase;
using Api.Domain.Interface;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Domain.Commands.PurchaseCommands
{
    public class DeletePurchaseCommandHandler : CommandHandler, IRequestHandler<DeletePurchaseCommand, ValidationResult>
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public DeletePurchaseCommandHandler(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<ValidationResult> Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var purchase = await _purchaseRepository.GetByIdAsync(request.Id);
            if (purchase == null)
            {
                AddError("Purchase não encontrada.");
                return ValidationResult;
            }

            _purchaseRepository.Delete(request.Id);

            purchase.AddDomainEvent(new DeletedPurchaseEvent(purchase.Id));

            return await Commit(_purchaseRepository.UnitOfWork);
        }
    }
} 