using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string GameTitle { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
