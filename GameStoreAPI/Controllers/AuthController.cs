using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStoreAPI.Data;
using GameStoreAPI.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GameStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly GameStoreDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _securityKey;

        public AuthController(GameStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            
            var jwtKey = configuration["Jwt:Key"] ?? 
                throw new InvalidOperationException("JWT Key is not configured in appsettings.json");
            
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT Key cannot be empty");
            }
            
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(SignupRequest request)
        {
            try
            {
                Console.WriteLine($"Attempting to register user: {request.Email}");
                Console.WriteLine($"Request data: Name={request.Name}, Email={request.Email}");
                
                // Validate request
                if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                {
                    Console.WriteLine("Invalid request data: Missing required fields");
                    return BadRequest("Name, email, and password are required");
                }

                // Check if user exists
                Console.WriteLine("Checking if user already exists...");
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
                if (existingUser != null)
                {
                    Console.WriteLine($"User with email {request.Email} already exists");
                    return BadRequest("User already exists");
                }

                // Create new user
                var user = new User
                {
                    Name = request.Name,
                    Email = request.Email,
                    PasswordHash = HashPassword(request.Password)
                };

                Console.WriteLine($"Creating new user: {user.Name} ({user.Email})");
                
                try
                {
                    Console.WriteLine("Adding user to database context...");
                    _context.Users.Add(user);
                    
                    Console.WriteLine("Saving changes to database...");
                    var result = await _context.SaveChangesAsync();
                    Console.WriteLine($"SaveChanges result: {result} rows affected");
                    
                    if (result <= 0)
                    {
                        Console.WriteLine("Failed to save user to database");
                        return StatusCode(500, "Failed to save user to database");
                    }
                    
                    Console.WriteLine($"User {user.Email} successfully registered with ID: {user.Id}");
                    
                    // Verify user was saved
                    var savedUser = await _context.Users.FindAsync(user.Id);
                    if (savedUser == null)
                    {
                        Console.WriteLine("Error: User not found after saving");
                        return StatusCode(500, "Error verifying user registration");
                    }
                    Console.WriteLine($"Verified saved user: {savedUser.Name} ({savedUser.Email})");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving user to database: {ex.Message}");
                    Console.WriteLine($"Stack trace: {ex.StackTrace}");
                    return StatusCode(500, "Error saving user to database");
                }

                // Generate token
                var token = GenerateJwtToken(user);
                Console.WriteLine("JWT token generated successfully");

                return Ok(new AuthResponse
                {
                    Token = token,
                    User = new UserResponse
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error during registration: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return StatusCode(500, "An unexpected error occurred during registration");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
        {
            try
            {
                Console.WriteLine($"Attempting login for user: {request.Email}");
                
                Console.WriteLine("Querying database for user...");
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
                if (user == null)
                {
                    Console.WriteLine($"User with email {request.Email} not found");
                    return Unauthorized("Invalid email or password");
                }

                Console.WriteLine($"User found: {user.Name} ({user.Email})");
                
                if (!VerifyPassword(request.Password, user.PasswordHash))
                {
                    Console.WriteLine("Invalid password");
                    return Unauthorized("Invalid email or password");
                }

                Console.WriteLine("Password verified successfully");
                var token = GenerateJwtToken(user);
                Console.WriteLine("JWT token generated successfully");

                return Ok(new AuthResponse
                {
                    Token = token,
                    User = new UserResponse
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error during login: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return StatusCode(500, "An unexpected error occurred during login");
            }
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }

        private string GenerateJwtToken(User user)
        {
            var credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"] ?? "GameStoreAPI",
                audience: _configuration["Jwt:Audience"] ?? "GameStoreUsers",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class SignupRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public UserResponse User { get; set; } = new();
    }

    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
} 