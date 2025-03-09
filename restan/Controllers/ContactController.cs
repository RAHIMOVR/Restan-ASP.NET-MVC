using Microsoft.AspNetCore.Mvc;

namespace restan.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
