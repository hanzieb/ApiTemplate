using ApiTemplate.Business.Repositories.Abstract;
using ApiTemplate.Model.EF;
using ApiTemplate.Model.EF.Entities;
using System.Data.Entity;

namespace ApiTemplate.Business.Repositories
{
    public interface IAnimalRepository : IUowRepositoryAsync<Animal, int>
    {
        IQueryable<Animal> Query();
        Task<Animal> GetByIdAsync(int id, CancellationToken token);
        Task ResetAnimals();
    }

    public class AnimalRepository : UowBaseRepositoryAsync<IScopedDbContextFactory<DbMemContext>, DbMemContext, Animal, int>, IAnimalRepository
    {
        public AnimalRepository(IScopedDbContextFactory<DbMemContext> contextFactory) : base(contextFactory) { }

        public IQueryable<Animal> Query()
        {
            return NewQuery();
        }

        public async Task<Animal> GetByIdAsync(int id, CancellationToken token)
        {
            return await NewQuery().Where(x => x.Id == id).FirstOrDefaultAsync(token);
        }

        public Task ResetAnimals()
        {
            //NOTE: Only use this for very small < 1k record sets. Its inherently very slow and only used here for sample data staging
            _context.Animals.RemoveRange(_context.Animals);
            return Task.CompletedTask;
        }
    }
}
