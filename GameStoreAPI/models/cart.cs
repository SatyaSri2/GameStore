using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }

       
        public int UserId { get; set; }

       
        public string GameTitle { get; set; } = string.Empty;

        public string GameDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
