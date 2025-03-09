using Microsoft.AspNetCore.Mvc;

namespace restan.Controllers
{
    public class ChefController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
