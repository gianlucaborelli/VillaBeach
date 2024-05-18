using Api.Domain.Dtos.Purchase;
using Api.Service.Interfaces;

namespace Api.Service.Services
{
    public class PurchaseService : IPurchaseService
    {
        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PurchaseDto>?> FindByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<PurchaseDto> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PurchaseDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PurchaseDto>?> GetAllIncomplete()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PurchaseDto>?> GetAllIncompleteByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<PurchaseDtoCreateResult> Post(PurchaseDtoCreateRequest user)
        {
            throw new NotImplementedException();
        }

        public Task<PurchaseDtoUpdateResult> Put(PurchaseDtoUpdateRequest user)
        {
            throw new NotImplementedException();
        }

        public Task<PurchaseDto> SetPurchaseAsComplete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PurchaseDto> SetPurchaseAsIncomplete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}