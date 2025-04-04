using Microsoft.EntityFrameworkCore;
using GameStoreAPI.Models;

namespace GameStoreAPI.Data
{
    public class GameStoreDbContext : DbContext
    {
        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
