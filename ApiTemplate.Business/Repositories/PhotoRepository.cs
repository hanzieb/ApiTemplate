using ApiTemplate.Business.Repositories.Abstract;
using ApiTemplate.Model.EF;
using ApiTemplate.Model.EF.Entities;
using System.Data.Entity;

namespace ApiTemplate.Business.Repositories
{
    public interface IPhotoRepository : IUowRepositoryAsync<Photo, int>
    {
        IQueryable<Photo> Query();
        Task<Photo> GetByIdAsync(int id, CancellationToken token);
        Task ResetPhotos();
    }

    public class PhotoRepository : UowBaseRepositoryAsync<IScopedDbContextFactory<DbMemContext>, DbMemContext, Photo, int>, IPhotoRepository
    {
        public PhotoRepository(IScopedDbContextFactory<DbMemContext> contextFactory) : base(contextFactory) { }

        public IQueryable<Photo> Query()
        {
            return NewQuery();
        }

        public async Task<Photo> GetByIdAsync(int id, CancellationToken token)
        {
            return await NewQuery().Where(x => x.Id == id).FirstOrDefaultAsync(token);
        }

        public Task ResetPhotos()
        {
            //NOTE: Only use this for very small < 1k record sets. Its inherently very slow and only used here for sample data staging
            _context.Photos.RemoveRange(_context.Photos);
            return Task.CompletedTask;
        }
    }
}
