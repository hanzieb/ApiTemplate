using ApiTemplate.Model.Models;

namespace ApiTemplate.Business.Repositories.Abstract
{
    public interface IUowRepositoryAsync<T, TId> : ICrudRepositoryAsync<T, TId>
        where T : TableEntity
    {
        void SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken token);
    }
}
