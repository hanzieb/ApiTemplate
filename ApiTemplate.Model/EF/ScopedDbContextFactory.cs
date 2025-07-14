using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Model.EF
{
    public interface IScopedDbContextFactory<TContext>
        where TContext : DbContext
    {
        TContext CreateDbContext();
    }

    public class ScopedDbMemContextFactory : IDbContextFactory<DbMemContext>, IScopedDbContextFactory<DbMemContext>
    {
        private readonly IDbContextFactory<DbMemContext> _pooledFactory;

        public ScopedDbMemContextFactory(IDbContextFactory<DbMemContext> pooledFactory)
        {
            _pooledFactory = pooledFactory;
        }

        public DbMemContext CreateDbContext()
        {
            var context = _pooledFactory.CreateDbContext();
            return context;
        }
    }
}
