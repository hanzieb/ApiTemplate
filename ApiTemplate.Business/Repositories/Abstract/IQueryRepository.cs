namespace ApiTemplate.Business.Repositories.Abstract
{
    public interface IQueryRepository<out TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> NewQuery();
    }
}