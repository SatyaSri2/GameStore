using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStoreAPI.Data;
using GameStoreAPI.Models;

namespace GameStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GameStoreDbContext _context;

        public GamesController(GameStoreDbContext context)
        {
            _context = context;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            return await _context.Games.ToListAsync();
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        // GET: api/Games/category/{category}
        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGamesByCategory(string category)
        {
            return await _context.Games
                .Where(g => g.Category.ToLower() == category.ToLower())
                .ToListAsync();
        }

        // GET: api/Games/genre/{genre}
        [HttpGet("genre/{genre}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGamesByGenre(string genre)
        {
            return await _context.Games
                .Where(g => g.Genre.ToLower() == genre.ToLower())
                .ToListAsync();
        }

        // GET: api/Games/platform/{platform}
        [HttpGet("platform/{platform}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGamesByPlatform(string platform)
        {
            return await _context.Games
                .Where(g => g.Platforms.ToLower().Contains(platform.ToLower()))
                .ToListAsync();
        }

        // PUT: api/Games/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Games
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
} 