using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Nomayini.Apis.Core.Authentication;
using Nomayini.Apis.Feature.Auth.Login;
using Nomayini.Apis.Feature.Auth.Register;
using Nomayini.Apis.Feature.Messaging.GetMessage;
using Nomayini.Apis.Feature.Messaging.PostMessage;
using Nomayini.Apis.Infrastructure.Middleware;
using Nomayini.Apis.Shared.Behaviours;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Register built-in authorization and OpenAPI tools
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, _) =>
    {
        document.Info = new()
        {
            Title = "Nomayini.Apis",
            Version = "v1",
            Description = """
                This is my Jimmy Tjotola's On Premise Hosted Vertical slice utlizing MediatR API on my Raspberry Pi for managing authentication and various features i just added messaging.
                I'm sure you're wondering what's the purpose of this—really, it’s just for testing CI/CD, Docker Compose, and on-prem API usage.
                Hosting my own projects and managing resources efficently , this fundamently a challenge for myself , how efficient can i write my code 
                too the point where my little raspberry pi which is sitting beside my desk at the moment doesnt struggle even under load.How too manager containers efficiently
                on it after deploying from github using github actions and it as a github runner , later too be managed by my docker compose which is running netdata too make sure everything is still cool.
                Later features might include a "let me tell you about me" endpoint or similar fun additions. 
                This API is intended primarily for my personal projects and increasing my ability too write efficient code, also i cant afford microsoft and amazon subscriptions.
                So its gonna be cloudflare tunnels and my little containers for now, might just mess around and install Kubernettes k3s.
                """,
            Contact = new()
            {
                Name = "API support – contact me if you need help",
                Email = "jimmytjotola@gmail.com",
                Url = new Uri("https://www.linkedin.com/in/your-profile")
            }
        };

        document.Servers = new List<OpenApiServer>
        {
            new OpenApiServer { Url = "https://jimmytjotola.org" }
        };

        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes.Add("Bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "JWT Authorization header using the Bearer scheme."
        });

        document.SecurityRequirements.Add(new OpenApiSecurityRequirement
        {
            [new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            }] = new List<string>()
        });

        return Task.CompletedTask;
    });
});

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
            ClockSkew = TimeSpan.Zero  // Remove default clock skew for strict expiration validation
        };
    });

var app = builder.Build();

// Map OpenAPI endpoint);
app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options
        .WithTitle("JimmyJams APIs")
        .WithTheme(ScalarTheme.Mars)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);

});
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
// Map application endpoints
RegisterEndpoint.MapEndpoint(app);
LoginEndpoint.MapEndpoint(app);
PostMessageEndpoint.MapEndpoint(app);
GetAllMessagesEndpoint.MapEndpoint(app);


// Database initialization
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();
}

app.Run();
