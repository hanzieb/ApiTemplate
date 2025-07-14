using ApiTemplate.Business.Repositories;
using ApiTemplate.Business.ViewModels;
using ApiTemplate.Model.EF.Entities;
using ApiTemplate.Model.Models;

namespace ApiTemplate.Business.AppServices
{
    public interface IPetGalleryService
    {
        /// <summary>
        /// Resets the in memory datastore back to default
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task SeedorResetDataStoreToDefault(CancellationToken token);

        /// <summary>
        /// Returns all animals
        /// <param name="token"></param>
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AnimalViewModel>> GetAll(CancellationToken token);
    }

    public class PetGalleryService : IPetGalleryService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IPhotoRepository _photoRepository;

        public PetGalleryService(IAnimalRepository animalRepository, IPhotoRepository photoRepository)
        {
            _animalRepository = animalRepository;
            _photoRepository = photoRepository;
        }

        public async Task<IEnumerable<AnimalViewModel>> GetAll(CancellationToken token)
        {
            IEnumerable<AnimalViewModel> rtn = null;

            rtn = _animalRepository.Query()
                .Select(x => new AnimalViewModel(x,
                    x.Photos.Select(y => new PhotoViewModel(y)))
            );

            return rtn;
        }

        public async Task SeedorResetDataStoreToDefault(CancellationToken token)
        {
            // No need to process further if canceled
            token.ThrowIfCancellationRequested();

            //reset the data stores
            await _photoRepository.ResetPhotos();
            await _animalRepository.ResetAnimals();

            const string IMAGES_FOLDER = "/images";

            //re instance new models
            var animalBugsy = new Animal("Bugsy", "My childhood black cat", "American Shorthair", AnimalTypes.Cat, new List<Photo>()
            { 
                new Photo($"{IMAGES_FOLDER}/Bugsy/1.jpg", 0, 768, 576)
            });
            var animalDuke = new Animal("Duke", "My old man mini dachshund", "Mini-Dachshund", AnimalTypes.Dog, new List<Photo>()
            {
                new Photo($"{IMAGES_FOLDER}/Duke/1.jpg", 0, 1960, 1470),
                new Photo($"{IMAGES_FOLDER}/Duke/2.jpg", 1, 2348, 1761),
                new Photo($"{IMAGES_FOLDER}/Duke/3.jpg", 2, 1960, 4032),
                new Photo($"{IMAGES_FOLDER}/Duke/4.jpg", 3, 1960, 4032),
            });
            var animalFrankie = new Animal("Frankie", "My lil princess mini dachshund", "Mini-Dachshund", AnimalTypes.Dog, new List<Photo>()
            {
                new Photo($"{IMAGES_FOLDER}/Frankie/1.jpg", 0, 3096, 2322),
                new Photo($"{IMAGES_FOLDER}/Frankie/2.jpg", 1, 4032, 1960),
                new Photo($"{IMAGES_FOLDER}/Frankie/3.jpg", 2, 1960, 4032),
                new Photo($"{IMAGES_FOLDER}/Frankie/4.jpg", 3, 3072, 2304),
            });
            var animalToby = new Animal("Toby", "Officially the bestest boy ever", "Aussie Cattle Dog", AnimalTypes.Dog, new List<Photo>()
            {
                new Photo($"{IMAGES_FOLDER}/Toby/1.jpg", 0, 1870, 1870),
                new Photo($"{IMAGES_FOLDER}/Toby/2.jpg", 1, 2614, 1960),
                new Photo($"{IMAGES_FOLDER}/Toby/3.jpg", 2, 1960, 4032),
                new Photo($"{IMAGES_FOLDER}/Toby/4.jpg", 3, 1870, 1870),
            });

            //add to the context and save
            await _animalRepository.AddAsync(animalBugsy, token);
            await _animalRepository.AddAsync(animalDuke, token);
            await _animalRepository.AddAsync(animalFrankie, token);
            await _animalRepository.AddAsync(animalToby, token);
            await _animalRepository.SaveChangesAsync(token);
        }
    }
}
