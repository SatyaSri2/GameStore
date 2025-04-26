using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Image = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Rating = table.Column<double>(type: "REAL", nullable: false),
                    Reviews = table.Column<int>(type: "INTEGER", nullable: false),
                    Downloads = table.Column<int>(type: "INTEGER", nullable: false),
                    Badge = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Genre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Category = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    IsFavorite = table.Column<bool>(type: "INTEGER", nullable: false),
                    Platforms = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Badge", "Category", "Description", "Downloads", "Genre", "Image", "IsFavorite", "Name", "Platforms", "Price", "Rating", "Reviews" },
                values: new object[,]
                {
                    { 1, "Top Seller", "action", "A futuristic action-packed RPG with cyber-enhanced warriors.", 200000, "Action RPG", "https://pub-f354ec240bea480db7320bd0e29d972e.r2.dev/sites/2/2025/03/Atomfall-hero-d25f8e2fb0c6e97799e1.jpg", false, "Atomfall", "PC,PS5,Xbox", 59.99m, 4.7999999999999998, 1500 },
                    { 2, "Best Racing Game", "racing", "High-speed adrenaline-fueled racing with realistic physics.", 500000, "Racing", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTrGksnJpXiB0xcE4ROvTcnFjS7rZziRL4wWw&s", false, "Racing Rivals", "PC,PS5,Xbox", 49.99m, 4.5, 980 },
                    { 3, "Editor's Choice", "adventure", "An open-world fantasy adventure filled with mythical creatures.", 300000, "Adventure RPG", "https://thefinalfantasy.net/gallery/wallpaper/ff-mystic-quest/ff-mystic-quest-wallpaper-2.jpg", false, "Mystic Quest", "PC,Switch", 39.99m, 4.7000000000000002, 2000 },
                    { 4, "Top Multiplayer", "shooter", "Mini Royale elevates the beloved toy soldier experience with creative weapons, gadgets, and dynamic movement. Swing from curtain rods, dodge between towering action figures, and zip across the room using the game's signature grapple gun. Every match offers fresh and exciting ways to outmaneuver your opponents.", 1000000, "Shooter", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQXVIFt6G3VaWO1iR0G4bjdn-bna6nFddmRrg&s", false, "Mini Royale: The Ultimate Toy Soldier Shooter!", "PC,PS5,Xbox", 69.99m, 4.5999999999999996, 3000 },
                    { 5, "Best Strategy Game", "adventure", "Experience an epic action-adventure story set in feudal Japan! Become a lethal shinobi assassin and powerful legendary samurai as you explore a beautiful open world in a time of chaos.", 150000, "Strategy", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT4M0BgASIn8R5h1LlbKEjSbLP4Yj2VBnnmQw&s", false, "Assassin's Creed Shadows", "PC", 29.99m, 4.4000000000000004, 800 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
