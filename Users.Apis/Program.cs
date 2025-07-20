using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Users.Apis.Core.Authentication;
using Users.Apis.Feature.Auth.Login;
using Users.Apis.Feature.Auth.Register;
using Users.Apis.Feature.Messaging.GetMessage;
using Users.Apis.Feature.Messaging.PostMessage;
using Users.Apis.Feature.UploadImage.GetImage;
using Users.Apis.Feature.UploadImage.PostImage;
using Users.Apis.Infrastructure.Middleware;
using Users.Apis.Shared.Behaviours;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAntiforgery();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, _) =>
    {
        document.Info = new()
        {
            Title = "User.Api",
            Version = "v1",
            Description = "User microservice",
            Contact = new()
            {
                Name = "API support – contact me if you need help",
                Email = "tjotolajimmy@gmail.com",
                Url = new Uri("https://www.linkedin.com/in/jimmy-tjotola-91766126a/")
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
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<IJwtService, JwtService>();

// Configure EF Core with SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")))
    .AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

// Register MediatR and custom pipeline behaviors
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
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
//configure opentelemetry here for monitoring and logging move away from NETDATA

var app = builder.Build();
app.UseStaticFiles();
// Map OpenAPI endpoint);
app.UseRouting();
app.UseAntiforgery();
app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options
        .WithTitle("Users Api")
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
PostImageEndpoint.MapEndpoint(app);
GetImageEndpoint.MapEndpoint(app);
// Database initialization
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();
}

app.Run();
