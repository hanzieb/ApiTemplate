using ApiTemplate.Business.AppServices;
using ApiTemplate.Business.ViewModels;
using ApiTemplate.Model.EF.Entities;
using ApiTemplate.Model.Models;
using ApiTemplate.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;

namespace ApiTemplate.Tests.Unit.Controller
{
    [TestFixture]
    public class GalleryControllerTests
    {
        private GalleryController _testableController;
        private IPetGalleryService _mockPetGalleryService;

        private AnimalViewModel _testAnimalViewModel;

        [SetUp]
        public void SetUp()
        {
            var _testPhoto = new Photo("test.jpg", 0, 768, 1024);
            var _testAnimal = new Animal("Test",
                "The Test Animal", "Test", AnimalTypes.Dog, new List<Photo>() { _testPhoto });
            _testAnimalViewModel = new AnimalViewModel(_testAnimal);

            _mockPetGalleryService = Substitute.For<IPetGalleryService>();
            _mockPetGalleryService.GetAll(CancellationToken.None)
                .Returns(new AnimalViewModel[] { _testAnimalViewModel });

            _testableController = new GalleryController(_mockPetGalleryService);
        }

        [TearDown]
        public void TearDown()
        {
            _testableController.Dispose();
        }

        [Test]
        public async Task DoesReturnViewModel()
        {
            var view = await _testableController.Index(CancellationToken.None);
            object? result = ((ViewResult)view).ViewData.Model;
            //data accuracy testing is done at the service level...
            //and doesn't need to be repeated at the controller...
            //unless additional transformations happen during the call warranting testing on.
            AnimalViewModel[] viewModel = result as AnimalViewModel[];
            Assert.That(result, Is.TypeOf<AnimalViewModel[]>());
            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel.Count(), Is.EqualTo(1));
        }
    }
}
