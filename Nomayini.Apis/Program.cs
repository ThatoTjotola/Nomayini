using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IPasswordHasher, BCryptHasher>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Secret"])),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/register", async (
    [FromBody] RegisterRequest request,
    AppDbContext db,
    IPasswordHasher hasher) =>
{

    if (await db.Users.AnyAsync(u => u.Email == request.Email))
        return Results.Conflict("User already exists");

    var user = new User
    {
        Email = request.Email,
        PasswordHash = hasher.HashPassword(request.Password)
    };

    db.Users.Add(user);
    await db.SaveChangesAsync();

    return Results.Created();
});

// Login Endpoint
app.MapPost("/login", async (
    [FromBody] LoginRequest request,
    AppDbContext db,
    IPasswordHasher hasher,
    IJwtService jwt) =>
{
    var user = await db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

    if (user is null || !hasher.VerifyPassword(user.PasswordHash, request.Password))
        return Results.Unauthorized();

    var token = jwt.GenerateToken(user);
    return Results.Ok(new { token });
});



app.Run();
