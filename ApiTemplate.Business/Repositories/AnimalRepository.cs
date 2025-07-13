using ApiTemplate.Business.Repositories.Abstract;
using ApiTemplate.Model.EF;
using System.Data.Entity;

namespace ApiTemplate.Business.Repositories
{
    public interface IAnimalRepository : IUowRepositoryAsync<Model.EF.Animal, int>
    {
        IQueryable<Animal> Get();
        Task<Animal> GetByIdAsync(int id);
        Task ResetAnimals();
    }

    public class AnimalRepository : UowBaseRepositoryAsync<DbMemContext, Animal, int>, IAnimalRepository
    {
        private readonly DbMemContext _context;
        public AnimalRepository(DbMemContext context) : base(context) 
        {
            _context = context;
        }

        public IQueryable<Animal> Get()
        {
            return NewQuery();
        }

        public async Task<Animal> GetByIdAsync(int id)
        {
            return await NewQuery().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task ResetAnimals()
        {
            //NOTE: Only use this for very small < 1k record sets. Its inherently very slow and only used here for sample data staging
            _context.Animals.RemoveRange(_context.Animals);
            return Task.CompletedTask;
        }
    }
}
