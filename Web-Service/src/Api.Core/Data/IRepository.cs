using Api.Core.Domain;

namespace Api.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        void Add(T item);

        void Update(T item);

        void Delete (Guid id);

        Task<T?> GetByIdAsync (Guid id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<bool> ExistAsync(Guid id);
    }
}