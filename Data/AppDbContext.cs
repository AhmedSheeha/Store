using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.Data.Models;
using Store.Models.Models;

namespace Store.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
       public AppDbContext(DbContextOptions<AppDbContext>  options) : base(options) 
        { 
        
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
