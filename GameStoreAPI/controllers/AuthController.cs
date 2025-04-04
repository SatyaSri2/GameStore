using Microsoft.AspNetCore.Mvc;
using GameStoreAPI.Data;
using GameStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GameStoreAPI.Controllers
{
    [Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly GameStoreDbContext _context;

    public AuthController(GameStoreDbContext context)
    {
        _context = context;
    }

     [HttpGet]
        public IActionResult Get()
        {
            return Ok("Auth endpoint is working!");
        }

    // [HttpPost("login")]
    // public async Task<IActionResult> Login([FromBody] LoginRequest login)
    // {
    //     var user = await _context.Users
    //         .FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);

    //     if (user == null) return Unauthorized("Invalid credentials.");

    //     return Ok(new { message = "Login successful", user });
    // }
    [HttpPost("login")]
public async Task<IActionResult> Login([FromBody] LoginRequest login)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);

    if (user == null)
        return Unauthorized("Invalid credentials.");

    return Ok(new { message = "Login successful", user });
    // var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);
    // if (user == null) return Unauthorized("Invalid credentials.");

    // // JWT Token creation
    // var claims = new[]
    // {
    //     new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    //     new Claim(ClaimTypes.Email, user.Email)
    // };

    // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super_secret_key_here")); // Put in appsettings.json ideally
    // var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    // var token = new JwtSecurityToken(
    //     issuer: "yourdomain.com",
    //     audience: "yourdomain.com",
    //     claims: claims,
    //     expires: DateTime.Now.AddHours(1),
    //     signingCredentials: creds
    // );

    // var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

    // return Ok(new { token = tokenString, user });
}

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequest signup)
    {
        var exists = await _context.Users.AnyAsync(u => u.Email == signup.Email);
        if (exists) return BadRequest("Email already exists.");

        var user = new User
        {
            Name = signup.Name,
            Email = signup.Email,
            Password = signup.Password
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Signup successful", user });
    }
}

}
