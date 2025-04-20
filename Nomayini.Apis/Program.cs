using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Nomayini.Apis.Core.Authentication;
using Nomayini.Apis.Feature.Auth.Login;
using Nomayini.Apis.Feature.Auth.Register;
using Nomayini.Apis.Infrastructure.Middleware;
using Nomayini.Apis.Shared.Behaviours;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Register built-in authorization and OpenAPI tools
builder.Services.AddAuthorization();
builder.Services.AddOpenApi();

// Register custom authentication services
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtService, JwtService>();

// Configure EF Core with SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register MediatR and custom pipeline behaviors
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionPipelineBehavior<,>));

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret not configured");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero  // Optional: Remove default clock skew for strict expiration validation
        };
    });

var app = builder.Build();
app.MapOpenApi();
app.MapScalarApiReference(options =>
    {
        options
        .WithTitle("JimmyJams APIs")
        .WithTheme(ScalarTheme.Mars)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
});
// Map application endpoints
RegisterEndpoint.MapEndpoint(app);
LoginEndpoint.MapEndpoint(app);

// Middleware order is critical for proper authentication flow
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Authentication & Authorization must come before endpoint mapping
app.UseAuthentication();
app.UseAuthorization();

// Map Scalar API Reference UI 

// Database initialization
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();
}

app.Run();