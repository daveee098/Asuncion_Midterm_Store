using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Asuncion_Midterm_Store.Models;

namespace Asuncion_Midterm_Store.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Gaming Mouse",
                    Price = 799,
                    Description = "Wireless gaming mouse",
                    Category = "Accessories"
                },

                new Product
                {
                    Id = 2,
                    Name = "Mechanical Keyboard",
                    Price = 1499,
                    Description = "RGB mechanical keyboard",
                    Category = "Accessories"
                },

                new Product
                {
                    Id = 3,
                    Name = "Gaming Headset",
                    Price = 999,
                    Description = "Gaming headset with microphone",
                    Category = "Accessories"
                },

                new Product
                {
                    Id = 4,
                    Name = "USB Cable",
                    Price = 199,
                    Description = "USB charging and data cable",
                    Category = "Accessories"
                },

                new Product
                {
                    Id = 5,
                    Name = "Laptop Stand",
                    Price = 599,
                    Description = "Adjustable laptop stand",
                    Category = "Accessories"
                }
            );
        }
    }
}