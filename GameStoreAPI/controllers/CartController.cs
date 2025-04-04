using Microsoft.AspNetCore.Mvc;
using GameStoreAPI.Data;
using GameStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace GameStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly GameStoreDbContext _context;

        public CartController(GameStoreDbContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        [Authorize]
        public IActionResult AddToCart(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
            return Ok(new { message = "Game added to cart" });
        }

        [HttpGet("{userId}")]
        [Authorize]
        public IActionResult GetCart(int userId)
        {
            var cartItems = _context.Carts.Where(c => c.UserId == userId).ToList();
            return Ok(cartItems);
        }
    }
}
