using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.Product;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.Product;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Service.Services
{
    public class ProductService : IProductService
    {
        private IRepository<ProductEntity> _repository;

        private readonly IMapper _mapper;

        public ProductService(IRepository<ProductEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<ProductDto> Get(Guid id)
        {
            var entity =  await _repository.SelectAsync(id);
            return _mapper.Map<ProductDto>(entity);
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var entity =  await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(entity);
        }

        public async Task<IEnumerable<ProductDtoAvailableResult>> GetAvailableProducts()
        {
            var entity =  await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<ProductDtoAvailableResult>>(entity);
        }

        public async Task<ProductDtoCreateResult> Post(ProductDtoCreateRequest user)
        {
            var model= _mapper.Map<ProductModel>(user);
            var entity = _mapper.Map<ProductEntity>(model);
            var result = await _repository.InsertAsync(entity);
            return _mapper.Map<ProductDtoCreateResult>(result);
        }

        public async Task<ProductDtoUpdateResult> Put(ProductDtoUpdateRequest user)
        {
            var model= _mapper.Map<ProductModel>(user);
            var entity = _mapper.Map<ProductEntity>(model);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<ProductDtoUpdateResult>(result);
        }
    }
}