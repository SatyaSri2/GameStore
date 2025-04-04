namespace GameStoreAPI.Models;
public class Order
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public Game? Game{ get; set; }
    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; }
}
