using Microsoft.EntityFrameworkCore;
using GameStoreAPI.Models;

namespace GameStoreAPI.Data
{
    public class GameStoreDbContext : DbContext
    {
        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Game entity
            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Image).HasMaxLength(500);
                entity.Property(e => e.Genre).HasMaxLength(50);
                entity.Property(e => e.Category).HasMaxLength(50);
                entity.Property(e => e.Badge).HasMaxLength(50);
                entity.Property(e => e.Platforms).HasMaxLength(100);
            });

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Seed initial games data
            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    Id = 1,
                    Name = "Atomfall",
                    Category = "action",
                    Price = 59.99m,
                    Description = "A futuristic action-packed RPG with cyber-enhanced warriors.",
                    Image = "https://pub-f354ec240bea480db7320bd0e29d972e.r2.dev/sites/2/2025/03/Atomfall-hero-d25f8e2fb0c6e97799e1.jpg",
                    Rating = 4.8,
                    Reviews = 1500,
                    Downloads = 200000,
                    Badge = "Top Seller",
                    Genre = "Action RPG",
                    IsFavorite = false,
                    Platforms = "PC,PS5,Xbox"
                },
                new Game
                {
                    Id = 2,
                    Name = "Racing Rivals",
                    Category = "racing",
                    Price = 49.99m,
                    Description = "High-speed adrenaline-fueled racing with realistic physics.",
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTrGksnJpXiB0xcE4ROvTcnFjS7rZziRL4wWw&s",
                    Rating = 4.5,
                    Reviews = 980,
                    Downloads = 500000,
                    Badge = "Best Racing Game",
                    Genre = "Racing",
                    IsFavorite = false,
                    Platforms = "PC,PS5,Xbox"
                },
                new Game
                {
                    Id = 3,
                    Name = "Mystic Quest",
                    Category = "adventure",
                    Price = 39.99m,
                    Description = "An open-world fantasy adventure filled with mythical creatures.",
                    Image = "https://thefinalfantasy.net/gallery/wallpaper/ff-mystic-quest/ff-mystic-quest-wallpaper-2.jpg",
                    Rating = 4.7,
                    Reviews = 2000,
                    Downloads = 300000,
                    Badge = "Editor's Choice",
                    Genre = "Adventure RPG",
                    IsFavorite = false,
                    Platforms = "PC,Switch"
                },
                new Game
                {
                    Id = 4,
                    Name = "Mini Royale: The Ultimate Toy Soldier Shooter!",
                    Category = "shooter",
                    Price = 69.99m,
                    Description = "Mini Royale elevates the beloved toy soldier experience with creative weapons, gadgets, and dynamic movement. Swing from curtain rods, dodge between towering action figures, and zip across the room using the game's signature grapple gun. Every match offers fresh and exciting ways to outmaneuver your opponents.",
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQXVIFt6G3VaWO1iR0G4bjdn-bna6nFddmRrg&s",
                    Rating = 4.6,
                    Reviews = 3000,
                    Downloads = 1000000,
                    Badge = "Top Multiplayer",
                    Genre = "Shooter",
                    IsFavorite = false,
                    Platforms = "PC,PS5,Xbox"
                },
                new Game
                {
                    Id = 5,
                    Name = "Assassin's Creed Shadows",
                    Category = "adventure",
                    Price = 29.99m,
                    Description = "Experience an epic action-adventure story set in feudal Japan! Become a lethal shinobi assassin and powerful legendary samurai as you explore a beautiful open world in a time of chaos.",
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT4M0BgASIn8R5h1LlbKEjSbLP4Yj2VBnnmQw&s",
                    Rating = 4.4,
                    Reviews = 800,
                    Downloads = 150000,
                    Badge = "Best Strategy Game",
                    Genre = "Strategy",
                    IsFavorite = false,
                    Platforms = "PC"
                }
            );
        }
    }
} 