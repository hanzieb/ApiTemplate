using ApiTemplate.Model.Models;

namespace ApiTemplate.Business.Repositories.Abstract
{
    public interface ICrudRepositoryAsync<TEntity, TId>
    where TEntity : TableEntity
    {
        Task<TEntity> GetByIdAsync(TId id);
        /// <summary>
        /// documentation still recommends not using async add, so we have both included
        /// </summary>
        /// <param name="obj"></param>
        void Add(TEntity obj);
        Task AddAsync(TEntity obj);
        Task RemoveAsync(TId id);
        Task RemoveAsync(TEntity obj);
    }
}