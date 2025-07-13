using ApiTemplate.Model.Models;
using System.Diagnostics.CodeAnalysis;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace ApiTemplate.Business.Repositories.Abstract
{
    public abstract class UowBaseRepositoryAsync<TContext, TEntity, TId> : IUowRepositoryAsync<TEntity, TId>, IQueryRepository<TEntity>
        where TContext : DbContext
        where TEntity : TableEntity
    {
        protected readonly TContext _context;
        private readonly Microsoft.EntityFrameworkCore.DbSet<TEntity> _objectSet;

        [SuppressMessage("ReSharper", "PublicConstructorInAbstractClass")]
        public UowBaseRepositoryAsync(TContext context)
        {
            _context = context;
            _objectSet = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> NewQuery()
        {
            return _objectSet.AsQueryable();
        }

        public void Add(TEntity obj)
        {
            _objectSet.Add(obj);
        }

        public async Task AddAsync(TEntity obj)
        {
            await _objectSet.AddAsync(obj);
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            var item = await _objectSet.FindAsync(id);
            if (item != null)
            {
                if (_context.Entry(item).State != EntityState.Unchanged)
                    await _context.Entry(item).GetDatabaseValuesAsync();
            }
            return item;
        }

        public async Task RemoveAsync(TId id)
        {
            var obj = await GetByIdAsync(id);
            _context.Remove(obj);
            return;
        }

        public async Task RemoveAsync(TEntity obj)
        {
            if (_context.Entry(obj).State == EntityState.Detached)
                _objectSet.Attach(obj);

            _objectSet.Remove(obj);
            return;
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async virtual Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

