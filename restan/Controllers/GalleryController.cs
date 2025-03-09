using Microsoft.AspNetCore.Mvc;

namespace restan.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
