namespace GameStoreAPI.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
        public double Rating { get; set; }
        public int Reviews { get; set; }
        public int Downloads { get; set; }
        public string Badge { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public bool IsFavorite { get; set; }
        public string Platforms { get; set; } = string.Empty; // Stored as comma-separated string
    }
} 