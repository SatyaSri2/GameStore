using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using GameStoreAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Game Store API", Version = "v1" });
    
    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configure SQLite
builder.Services.AddDbContext<GameStoreDbContext>(options =>
{
    var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "GameStore.db");
    Console.WriteLine($"Database path: {dbPath}");
    options.UseSqlite($"Data Source={dbPath}");
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithExposedHeaders("Authorization"));
});

// Configure JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"] ?? 
    throw new InvalidOperationException("JWT Key is not configured in appsettings.json");
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "GameStoreAPI";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "GameStoreUsers";

if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException("JWT Key cannot be empty");
}

var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = securityKey
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Ensure database is created and migrations are applied
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<GameStoreDbContext>();
        Console.WriteLine("Checking database...");
        
        // Ensure database directory exists
        var dbDirectory = Path.GetDirectoryName(Path.Combine(Directory.GetCurrentDirectory(), "GameStore.db"));
        if (!Directory.Exists(dbDirectory))
        {
            Directory.CreateDirectory(dbDirectory);
            Console.WriteLine($"Created database directory: {dbDirectory}");
        }
        
        // Ensure database is created
        context.Database.EnsureCreated();
        Console.WriteLine("Database check completed successfully");
        
        // Verify Users table exists
        var usersTableExists = context.Database.ExecuteSqlRaw("SELECT name FROM sqlite_master WHERE type='table' AND name='Users'");
        Console.WriteLine($"Users table exists: {usersTableExists != 0}");
        
        if (usersTableExists == 0)
        {
            Console.WriteLine("Users table does not exist. Creating...");
            context.Database.ExecuteSqlRaw(@"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Email TEXT NOT NULL UNIQUE,
                    PasswordHash TEXT NOT NULL
                )");
            Console.WriteLine("Users table created successfully");
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while setting up the database.");
        Console.WriteLine($"Database error: {ex.Message}");
        Console.WriteLine($"Stack trace: {ex.StackTrace}");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Game Store API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run(); 