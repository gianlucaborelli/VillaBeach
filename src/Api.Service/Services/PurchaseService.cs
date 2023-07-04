using Api.Domain.Dtos.Purchase;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.Purchase;
using Api.Domain.Models;
using Api.Domain.Repository;
using AutoMapper;

namespace Api.Service.Services
{
    public class PurchaseService : IPurchaseService
    {
        private IPurchaseRepository _repository;
        private readonly IMapper _mapper;

        public PurchaseService(IPurchaseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }                

        public async Task<PurchaseDto> Get(Guid id)
        {
            var result = await _repository.SelectAsync();
            return _mapper.Map<PurchaseDto>(result);
        }

        public async Task<IEnumerable<PurchaseDto>> GetAll()
        {
            var result = await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<PurchaseDto>>(result);
        }

        public async Task<IEnumerable<PurchaseDto>?> GetAllIncomplete()
        {
            var result = await _repository.SelectAllIncomplete();
            return _mapper.Map<IEnumerable<PurchaseDto>?>(result);
        }

        public async Task<IEnumerable<PurchaseDto>?> GetAllIncompleteByUser(Guid userId)
        {
            var result = await _repository.SelectAllIncompleteByUser(userId);
            return _mapper.Map<IEnumerable<PurchaseDto>?>(result);
        }

        public async Task<IEnumerable<PurchaseDto>?> FindByUserId(Guid userId)
        {
            var result = await _repository.FindByUserId(userId);
            return _mapper.Map<IEnumerable<PurchaseDto>?>(result);
        }

        public async Task<PurchaseDtoCreateResult> Post(PurchaseDtoCreateRequest user)
        {
            var model = _mapper.Map<PurchaseModel>(user);
            var entity = _mapper.Map<PurchaseEntity>(model);
            var result = await _repository.InsertAsync(entity);
            return _mapper.Map<PurchaseDtoCreateResult>(result);
        }

        public async Task<PurchaseDtoUpdateResult> Put(PurchaseDtoUpdateRequest user)
        {
            var model = _mapper.Map<PurchaseModel>(user);
            var entity = _mapper.Map<PurchaseEntity>(model);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<PurchaseDtoUpdateResult>(result);
        }

        public async Task<PurchaseDto> SetPurchaseAsComplete(Guid id)
        {
            var result = await _repository.SetComplete(id);
            return _mapper.Map<PurchaseDto>(result);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}