using ApiTemplate.Business.AppServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Web.Controllers
{
    public class GalleryController : Controller
    {
        protected readonly IPetGalleryService _galleryService;
        public GalleryController(IPetGalleryService petGalleryService) 
        {
            _galleryService = petGalleryService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(CancellationToken token)
        {
            await _galleryService.SeedorResetDataStoreToDefault(token);
            var animals = await _galleryService.GetAll(token);
            return View(animals);
        }
    }
}
