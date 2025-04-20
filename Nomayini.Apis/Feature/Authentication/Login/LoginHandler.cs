using MediatR;
using Microsoft.EntityFrameworkCore;
using Nomayini.Apis.Core.Authentication;

namespace Nomayini.Apis.Feature.Auth.Login
{
    // Features/Auth/Login/Handler.cs
    public sealed class LoginQueryHandler(
        AppDbContext db,
        IPasswordHasher hasher,
        IJwtService jwtService,
        ILogger<LoginQueryHandler> logger)
        : IRequestHandler<LoginQuery, LoginResponse>
    {
        public async Task<LoginResponse> Handle(
            LoginQuery query,
            CancellationToken cancellationToken)
        {
            logger.LogInformation("Login attempt for {Email}", query.Email);

            var user = await db.Users
                .FirstOrDefaultAsync(u => u.Email == query.Email, cancellationToken);

            if (user is null)
            {
                logger.LogWarning("User not found: {Email}", query.Email);
                throw new UnauthorizedException("Invalid credentials");
            }

            if (!hasher.VerifyPassword(user.PasswordHash, query.Password))
            {
                logger.LogWarning("Invalid password for {Email}", query.Email);
                throw new UnauthorizedException("Invalid credentials");
            }

            var token = jwtService.GenerateToken(user);
            logger.LogInformation("Login successful for {Email}", query.Email);

            return new LoginResponse(token);
        }
    }
}

