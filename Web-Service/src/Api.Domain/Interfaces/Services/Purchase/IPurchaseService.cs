using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.Purchase;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services.Purchase
{
    public interface IPurchaseService
    {
        Task<PurchaseDto> Get(Guid id);
        Task<IEnumerable<PurchaseDto>> GetAll();
        Task<IEnumerable<PurchaseDto>?> GetAllIncomplete();
        Task<IEnumerable<PurchaseDto>?> GetAllIncompleteByUser(Guid userId);        
        Task<IEnumerable<PurchaseDto>?> FindByUserId(Guid userId);
        Task<PurchaseDto> SetPurchaseAsComplete(Guid id);
        Task<PurchaseDto> SetPurchaseAsIncomplete(Guid id);
        Task<PurchaseDtoCreateResult> Post(PurchaseDtoCreateRequest user);
        Task<PurchaseDtoUpdateResult> Put(PurchaseDtoUpdateRequest user);
        Task<bool> Delete(Guid id);
    }
}