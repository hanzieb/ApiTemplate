using ApiTemplate.Model.Models;

namespace ApiTemplate.Business.Repositories.Abstract
{
    public interface ICrudRepositoryAsync<TEntity, TId>
    where TEntity : TableEntity
    {
        /// <summary>
        /// documentation still recommends not using async add, so we have both included
        /// </summary>
        /// <param name="obj"></param>
        void Add(TEntity obj);
        Task AddAsync(TEntity obj, CancellationToken token);
        Task RemoveAsync(TId id, CancellationToken token);
        Task Remove(TEntity obj);
        Task<TEntity> GetByIdAsync(TId id, CancellationToken token);
    }
}