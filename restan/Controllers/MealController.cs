using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restan.Data;
using restan.Models;
using restan.ViewModels;

namespace restan.Controllers
{
    public class MealController(MealContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Meal> mealList = await _context.Meals.ToListAsync(); 
            HomeVM vm = new HomeVM()
            {
                Meals = mealList
            };
            return View(vm);
        }
    }
}
