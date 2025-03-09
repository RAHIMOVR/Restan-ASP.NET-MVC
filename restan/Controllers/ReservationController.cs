using Microsoft.AspNetCore.Mvc;
using restan.Data;
using restan.Models;

namespace restan.Controllers
{
    public class ReservationController : Controller
    {
        private readonly MealContext _context;

        public ReservationController(MealContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Reservations.Add(reservation);
                _context.SaveChanges();
                return RedirectToAction("Success");
            }
            return View(reservation);
        }

        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }

}
