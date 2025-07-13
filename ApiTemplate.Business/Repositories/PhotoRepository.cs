using ApiTemplate.Business.Repositories.Abstract;
using ApiTemplate.Model.EF;
using System.Data.Entity;

namespace ApiTemplate.Business.Repositories
{
    public interface IPhotoRepository : IUowRepositoryAsync<Model.EF.Photo, int>
    {
        IQueryable<Photo> Get();
        Task<Photo> GetByIdAsync(int id);
        Task ResetPhotos();
    }

    public class PhotoRepository : UowBaseRepositoryAsync<DbMemContext, Photo, int>, IPhotoRepository
    {
        public PhotoRepository(DbMemContext context) : base(context) { }

        public IQueryable<Photo> Get()
        {
            return NewQuery();
        }

        public async Task<Photo> GetByIdAsync(int id)
        {
            return await NewQuery().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task ResetPhotos()
        {
            //NOTE: Only use this for very small < 1k record sets. Its inherently very slow and only used here for sample data staging
            _context.Photos.RemoveRange(_context.Photos);
            return Task.CompletedTask;
        }
    }
}
