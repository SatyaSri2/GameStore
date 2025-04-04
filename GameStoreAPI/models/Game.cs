namespace GameStoreAPI.Models;
public class Game {
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Category { get; set; }= "";
    public decimal Price { get; set; }
    public string Description { get; set; }="";
    public string Image { get; set; }="";
    public double Rating { get; set; }
    public int Reviews { get; set; }
    public int Downloads { get; set; }
    public string Badge { get; set; }="";
    public string Genre { get; set; }="";
    public bool IsFavorite { get; set; }
    public required string[] Platforms { get; set; }
}
