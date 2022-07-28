using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using productMgtApi.Domain.Models;

namespace productMgtApi.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<Category>().HasKey(p => p.Id);
            builder.Entity<Category>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Category>().Property(p => p.Description).IsRequired();

            builder.Entity<Product>().ToTable("Products");
            builder.Entity<Product>().HasKey(p => p.Id);
            builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Product>().Property(p => p.Description).IsRequired();
            builder.Entity<Product>().Property(p => p.Price).IsRequired();
            builder.Entity<Product>().Property(p => p.Disabled).IsRequired();
            builder.Entity<Product>().Property(p => p.AvaliableStock).IsRequired();
            builder.Entity<Product>().Property(p => p.CreatedAt).IsRequired();

            builder.Entity<Category>().HasData(
                new Category {Id = 1, Name = "Food", Description = "Food items"},
                new Category {Id = 2,  Name = "Electronics", Description = "Household appliances"}
            );

            builder.Entity<Product>().HasData(
                new Product {Id = 1, Name = "Rice", Description = "bag of rice", CreatedAt = DateTime.UtcNow, Price = 20000, AvaliableStock = 5, CategoryId = 1}
            );

        }
    }
}