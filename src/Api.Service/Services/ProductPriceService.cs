using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.ProductPrice;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.ProductPrice;
using Api.Domain.Models;
using Api.Domain.Repository;
using AutoMapper;

namespace Api.Service.Services
{
    public class ProductPriceService : IProductPriceService
    {
        private IProductPriceRepository _repository;

        private readonly IMapper _mapper;

        public ProductPriceService(IProductPriceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductPriceDto> Get(Guid id)
        {
            var entity = await _repository.SelectAsync(id);
            return _mapper.Map<ProductPriceDto>(entity);
        }

        public async Task<ProductPriceDto> GetCurrentProductPriceByProductId(Guid id)
        {
            var entity = await _repository.FindCurrentProductPriceByProductId(id);
            return _mapper.Map<ProductPriceDto>(entity);
        }

        public async Task<IEnumerable<ProductPriceDto>?> GetAllByProductId(Guid id)
        {
            var entity = await _repository.FindByProductId(id);
            return _mapper.Map<IEnumerable<ProductPriceDto>>(entity);
        }

        public async Task<ProductPriceDtoCreateResult> Post(ProductPriceDtoCreateRequest productPrice)
        {
            var model = _mapper.Map<ProductPriceModel>(productPrice);
            var entity = _mapper.Map<ProductPriceEntity>(model);

            await _repository.UpdatePricesToNotCurrentByIdProduct(entity.ProductId);

            entity.Current = true;
            var result = await _repository.InsertAsync(entity);
            return _mapper.Map<ProductPriceDtoCreateResult>(result);

        }

        public async Task<ProductPriceDtoUpdateResult> Put(ProductPriceDtoUpdateRequest productPrice)
        {
            var model = _mapper.Map<ProductPriceModel>(productPrice);
            var entity = _mapper.Map<ProductPriceEntity>(model);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<ProductPriceDtoUpdateResult>(result);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}