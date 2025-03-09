using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restan.Data;
using restan.Models;
using restan.ViewModels;

namespace restan.Controllers
{
    public class BlogController(MealContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Post> postList = await _context.Posts.ToListAsync();
            HomeVM vm = new HomeVM()
            {
                Posts = postList
            };
            return View(vm);
        }
    }
}
