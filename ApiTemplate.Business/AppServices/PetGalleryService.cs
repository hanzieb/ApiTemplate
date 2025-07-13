using ApiTemplate.Business.Repositories;
using ApiTemplate.Business.ViewModels;
using ApiTemplate.Model.EF;
using ApiTemplate.Model.Models;

namespace ApiTemplate.Business.AppServices
{
    public interface IPetGalleryService
    {
        /// <summary>
        /// Resets the in memory datastore back to default
        /// </summary>
        /// <returns></returns>
        Task SeedorResetDataStoreToDefault();

        /// <summary>
        /// Returns all animals
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AnimalViewModel>> GetAll();
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

        public async Task<IEnumerable<AnimalViewModel>> GetAll()
        {
            IEnumerable<AnimalViewModel> rtn = null;

            rtn = _animalRepository.Get()
                .Select(x => new AnimalViewModel(x,
                    x.Photos.Select(y => new PhotoViewModel(y)))
            );

            return rtn;
        }

        public Task SeedorResetDataStoreToDefault()
        {
            // This will get the current PROJECT directory
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string imagesFolder = Path.Combine(projectDirectory, "/Images");

            //reset the data stores
            _photoRepository.ResetPhotos();
            _animalRepository.ResetAnimals();

            //re instance new models
            var animalBugsy = _MakeAnimal("Bugsy", "My childhood black cat", AnimalTypes.Cat, new List<Photo>()
            { 
                _MakePhoto(File.ReadAllBytes(Path.Combine(imagesFolder, "/Bugsy/1.jpg")), 0, 768, 576)
            });
            var animalDuke = _MakeAnimal("Duke", "My old man mini dachshund", AnimalTypes.Dog, new List<Photo>()
            {
                _MakePhoto(File.ReadAllBytes(Path.Combine(imagesFolder, "/Duke/1.jpg")), 0, 1960, 1470),
                _MakePhoto(File.ReadAllBytes(Path.Combine(imagesFolder, "/Duke/2.jpg")), 1, 2348, 1761),
                _MakePhoto(File.ReadAllBytes(Path.Combine(imagesFolder, "/Duke/3.jpg")), 2, 1960, 4032),
                _MakePhoto(File.ReadAllBytes(Path.Combine(imagesFolder, "/Duke/4.jpg")), 3, 1960, 4032),
            });
            var animalFrankie = _MakeAnimal("Frankie", "My lil princess mini dachshund", AnimalTypes.Dog, new List<Photo>()
            {
                _MakePhoto(File.ReadAllBytes(Path.Combine(imagesFolder, "/Frankie/1.jpg")), 0, 3096, 2322),
                _MakePhoto(File.ReadAllBytes(Path.Combine(imagesFolder, "/Frankie/2.jpg")), 1, 4032, 1960),
                _MakePhoto(File.ReadAllBytes(Path.Combine(imagesFolder, "/Frankie/3.jpg")), 2, 1960, 4032),
                _MakePhoto(File.ReadAllBytes(Path.Combine(imagesFolder, "/Frankie/4.jpg")), 3, 3072, 2304),
            });
            var animalToby = _MakeAnimal("Toby", "Officially the bestest boy ever", AnimalTypes.Dog, new List<Photo>()
            {
                _MakePhoto(File.ReadAllBytes(Path.Combine(imagesFolder, "/Toby/1.jpg")), 0, 1870, 1870),
                _MakePhoto(File.ReadAllBytes(Path.Combine(imagesFolder, "/Toby/2.jpg")), 1, 2614, 1960),
                _MakePhoto(File.ReadAllBytes(Path.Combine(imagesFolder, "/Toby/3.jpg")), 2, 1960, 4032),
                _MakePhoto(File.ReadAllBytes(Path.Combine(imagesFolder, "/Toby/4.jpg")), 3, 1870, 1870),
            });

            //add to the context and save
            _animalRepository.Add(animalBugsy);
            _animalRepository.Add(animalDuke);
            _animalRepository.Add(animalFrankie);
            _animalRepository.Add(animalToby);
            _animalRepository.SaveChanges();

            return Task.CompletedTask;
        }

        private Animal _MakeAnimal(string name, string desc, AnimalTypes type, List<Photo> photos)
        {
            Animal animal = new Animal()
            {
                Name = name,
                Description = desc,
                AnimalType = type,
                Photos = photos
            };
            return animal;
        }

        private Photo _MakePhoto(byte[] fileContents, int index, int width, int height)
        {
            Photo photo = new Photo()
            {
                FileContents = fileContents,
                Height = height,
                Width = width,
                Index = index
            };
            return photo;
        }
    }
}
