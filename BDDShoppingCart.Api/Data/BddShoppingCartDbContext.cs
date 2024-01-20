using BDDShoppingCart.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BDDShoppingCart.Api.Data;

public class BddShoppingCartDbContext : DbContext
{
    public DbSet<Product?> Products { get; set; }
    public DbSet<ShoppingCart?> ShoppingCarts { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseInMemoryDatabase("BddShoppingCartDb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasData(new List<Product>
            {
                new() { Id = Guid.NewGuid(), Name = "Computer 1", ProductCode = "Computer01", UnitPrice = 1100m },
                new() { Id = Guid.NewGuid(), Name = "Computer 2", ProductCode = "Computer02", UnitPrice = 1600m },
                new() { Id = Guid.NewGuid(), Name = "Keyboard 1", ProductCode = "Keyboard01", UnitPrice = 50m, },
                new() { Id = Guid.NewGuid(), Name = "Keyboard 2", ProductCode = "Keyboard02", UnitPrice = 25m, },
                new() { Id = Guid.NewGuid(), Name = "Mouse 1", ProductCode = "Mouse01", UnitPrice = 30m },
                new() { Id = Guid.NewGuid(), Name = "Mouse 2", ProductCode = "Mouse02", UnitPrice = 35m }
            });
    }
}