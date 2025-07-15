using ApiTemplate.Business.AppServices;
using ApiTemplate.Business.Repositories;
using ApiTemplate.Model.EF.Entities;
using ApiTemplate.Model.Models;
using NSubstitute;
using NUnit.Framework;
using System.Collections;

namespace ApiTemplate.Tests.Unit.Services
{
    [TestFixture]
    public class PetGalleryServiceTests
    {
        private PetGalleryService _testService;

        private IAnimalRepository _mockAnimalRepository;
        private IPhotoRepository _mockPhotoRepository;

        private Photo _testPhoto;
        private Animal _testAnimal;

        [SetUp]
        public void SetUp()
        {
            _testPhoto = new Photo("test.jpg", 0, 768, 1024);
            _testAnimal = new Animal("Test",
                "The Test Animal", "Test", AnimalTypes.Dog, new List<Photo>() { _testPhoto });

            _mockAnimalRepository = Substitute.For<IAnimalRepository>();
            _mockAnimalRepository.Query()
                .Returns(new List<Animal>() { _testAnimal }.AsQueryable());

            _mockPhotoRepository = Substitute.For<IPhotoRepository>();

            _testService = new PetGalleryService(_mockAnimalRepository, _mockPhotoRepository);
        }

        [Test]
        public async Task ShouldReturnViewModelCollection()
        {
            var result = await _testService.GetAll(CancellationToken.None);
            var record = result.FirstOrDefault();

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(record, Is.Not.Null);

            Assert.That(record.Name, Is.EqualTo(_testAnimal.Name));
            Assert.That(record.Description, Is.EqualTo(_testAnimal.Description));
            Assert.That(record.Breed, Is.EqualTo(_testAnimal.Breed));
            Assert.That(record.AnimalType, Is.EqualTo(_testAnimal.AnimalType));

            var photo = record.Photos.FirstOrDefault();
            Assert.That(record.Photos.Count(), Is.EqualTo(1));
            Assert.That(photo, Is.Not.Null);
            Assert.That(photo.FilePath, Is.EqualTo(_testPhoto.FilePath));
            Assert.That(photo.Index, Is.EqualTo(_testPhoto.Index));
            Assert.That(photo.Width, Is.EqualTo(_testPhoto.Width));
            Assert.That(photo.Height, Is.EqualTo(_testPhoto.Height));
        }
    }
}
