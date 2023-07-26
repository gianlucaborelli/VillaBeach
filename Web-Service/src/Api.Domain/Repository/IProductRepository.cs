using Api.Domain.Dtos.Product;
using Api.Domain.Entities;
using Api.Domain.Interfaces;

namespace Api.Domain.Repository
{
    public interface IProductRepository: IRepository<ProductEntity>
    {
        Task<IEnumerable<ProductEntity>?> FindByName (string name);
    }
}