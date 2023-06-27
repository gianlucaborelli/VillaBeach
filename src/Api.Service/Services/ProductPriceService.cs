using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.ProductPrice;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.ProductPrice;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Service.Services
{
    public class ProductPriceService : IProductPriceService
    {
        private IRepository<ProductPriceEntity> _repository;

        private readonly IMapper _mapper;

        public ProductPriceService(IRepository<ProductPriceEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<ProductPriceDto> Get(Guid id)
        {
            var entity =  await _repository.SelectAsync(id);
            return _mapper.Map<ProductPriceDto>(entity);
        }

        public async Task<IEnumerable<ProductPriceDto>> GetAll()
        {
            var entity =  await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<ProductPriceDto>>(entity);
        }

        public async Task<ProductPriceDtoCreateResult> Post(ProductPriceDtoCreateRequest user)
        {
            var model= _mapper.Map<ProductPriceModel>(user);
            var entity = _mapper.Map<ProductPriceEntity>(model);
            var result = await _repository.InsertAsync(entity);
            return _mapper.Map<ProductPriceDtoCreateResult>(result);
        }

        public async Task<ProductPriceDtoUpdateResult> Put(ProductPriceDtoUpdateRequest user)
        {
            var model= _mapper.Map<ProductPriceModel>(user);
            var entity = _mapper.Map<ProductPriceEntity>(model);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<ProductPriceDtoUpdateResult>(result);
        }
    }
}