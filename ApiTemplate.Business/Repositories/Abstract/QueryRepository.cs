using ApiTemplate.Model.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace ApiTemplate.Business.Repositories.Abstract
{

    public abstract class QueryRepository<TContext, TEntity> : IQueryRepository<TEntity>
        where TContext : DbContext
        where TEntity : BaseEntity
    {
        protected readonly TContext _context;
        private readonly DbSet<TEntity> _objectSet;

        [SuppressMessage("ReSharper", "PublicConstructorInAbstractClass")]
        public QueryRepository(TContext context)
        {
            _context = context;
            _objectSet = _context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> NewQuery()
        {
            return _objectSet.AsQueryable().AsNoTracking();
        }

    }
}