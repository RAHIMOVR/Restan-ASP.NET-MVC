using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using restan.Models;

namespace restan.Data
{
    public class MealContext: IdentityDbContext<AppUser>
    {
        public MealContext(DbContextOptions<MealContext> options): base(options)
        {
            
        }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Meal>()
                .Property(m => m.Price)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
