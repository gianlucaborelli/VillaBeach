using Api.Domain.Dtos.Product;

namespace Api.Service.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> Get(Guid id);

        Task<IEnumerable<ProductDto>> GetAll();

        Task<IEnumerable<ProductDto>?> FindByName(string name);

        Task<IEnumerable<ProductDtoAvailableResult>> GetAvailableProducts();

        Task<ProductDtoCreateResult> Post(ProductDtoCreateRequest user);

        Task<ProductDtoUpdateResult> Put(ProductDtoUpdateRequest user);

        Task<bool> Delete(Guid id);
    }
}