using ApiTemplate.Model.EF;
using ApiTemplate.Model.Models;
using System.Diagnostics.CodeAnalysis;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace ApiTemplate.Business.Repositories.Abstract
{
    public abstract class UowBaseRepositoryAsync<TContextFactory, TContext, TEntity, TId> : IUowRepositoryAsync<TEntity, TId>, IQueryRepository<TEntity>
        where TContextFactory : IScopedDbContextFactory<TContext>
        where TContext : DbContext
        where TEntity : TableEntity
    {
        protected readonly TContextFactory _contextFactory;
        protected readonly TContext _context;
        private readonly Microsoft.EntityFrameworkCore.DbSet<TEntity> _objectSet;

        [SuppressMessage("ReSharper", "PublicConstructorInAbstractClass")]
        public UowBaseRepositoryAsync(TContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _context = contextFactory.CreateDbContext();
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

        public async Task AddAsync(TEntity obj, CancellationToken token)
        {
            await _objectSet.AddAsync(obj, token);
        }

        public async Task<TEntity> GetByIdAsync(TId id, CancellationToken token)
        {
            var item = await _objectSet.FindAsync(id, token);
            if (item != null && !token.IsCancellationRequested)
            {
                if (_context.Entry(item).State != EntityState.Unchanged)
                    await _context.Entry(item).GetDatabaseValuesAsync(token);
            }
            return item;
        }

        public async Task RemoveAsync(TId id, CancellationToken token)
        {
            var obj = await GetByIdAsync(id, token);

            token.ThrowIfCancellationRequested();
            _context.Remove(obj);

            return;
        }

        public async Task Remove(TEntity obj)
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

        public async virtual Task<int> SaveChangesAsync(CancellationToken token)
        {
            return await _context.SaveChangesAsync(token);
        }
    }
}

