namespace Api.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}