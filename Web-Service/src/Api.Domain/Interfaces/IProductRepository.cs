using Api.Core.Data;
using Api.Domain.Entities;

namespace Api.Domain.Interface
{
    public interface IProductRepository: IRepository<Product>
    {
        Task<IEnumerable<Product>?> FindByName (string name);
    }
}