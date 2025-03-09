using restan.Models;

namespace restan.ViewModels
{
    public class HomeVM
    {
        public List<Models.Meal> Meals { get; set; }
        public List<Models.Post> Posts { get; set; }
        public List<AppUser> AppUsers { get; set; }

    }
}
