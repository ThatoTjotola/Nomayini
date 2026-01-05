using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Core.Authentication;

namespace Portfolio.Api.Feature.Auth.Login
{
    /// <summary>
    /// Logic for login component 
    /// </summary>
    /// <param name="db"></param>
    /// <param name="hasher"></param>
    /// <param name="jwtService"></param>
    /// <param name="logger"></param>
    public sealed class LoginQueryHandler(IAppDbContext db,IPasswordHasher hasher,IJwtService jwtService,ILogger<LoginQueryHandler> logger) : IRequestHandler<LoginQuery, LoginResponse>
    {
        public async Task<LoginResponse> Handle(LoginQuery query,CancellationToken cancellationToken)
        {

            logger.LogInformation("Login attempt for {Email}", query.Email);
            var user = await db.Users
                .FirstOrDefaultAsync(u => u.Email == query.Email, cancellationToken);

            if (user is null || !hasher.VerifyPassword(user.PasswordHash, query.Password))
            {
                logger.LogWarning("Invalid credentials for please verify password and : {Email}", query.Email);
                throw new UnauthorizedException("Invalid credentials");
            }

            var token = jwtService.GenerateToken(user);
            logger.LogInformation("Login successful for {Email}", query.Email);

            return new LoginResponse(token);
        }
    }
}

