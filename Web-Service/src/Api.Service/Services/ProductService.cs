
using Api.Domain.Dtos.Product;
using Api.Service.Interfaces;
using Api.Domain.Interface;
using AutoMapper;

namespace Api.Service.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _repository;

        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDto>?> FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDtoAvailableResult>> GetAvailableProducts()
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDtoCreateResult> Post(ProductDtoCreateRequest user)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDtoUpdateResult> Put(ProductDtoUpdateRequest user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}