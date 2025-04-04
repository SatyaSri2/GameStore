
using Microsoft.EntityFrameworkCore;
using GameStoreAPI.Models;
namespace GameStoreAPI.Data;

public class GameStoreDbContext : DbContext
{
    public GameStoreDbContext (DbContextOptions<GameStoreDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Game>().HasData(
        
        new Game
        {
            Id = 1,
            Name = "Atomfall",
            Category = "action",
            Price = 59.99m,
            Description = "A complete meal",
            Image = "https://pub-f354ec240bea480db7320bd0e29d972e.r2.dev/sites/2/2025/03/Atomfall-hero-d25f8e2fb0c6e97799e1.jpg",
            Rating = 4.5,
            Reviews = 100,
            Downloads = 500,
            Badge = "New",
            Genre = "Vegetarian",
           Platforms = new[] { "PC", "Xbox" }

        }
    );
}

}
