using MediatR;
using Nomayini.Apis.Core.Authentication;
using Nomayini.Apis.Feature.Auth.Register;

namespace Nomayini.Apis.Feature.Auth;

public sealed class RegisterCommandHandler(
        AppDbContext _context,
        IPasswordHasher _passwordHasher,
        IJwtService _jwtService) : IRequestHandler<RegisterCommand, AuthResponse>
{

    public async Task<AuthResponse> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        var user = new User
        {
            Email = request.Email,
            PasswordHash = _passwordHasher.HashPassword(request.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return new AuthResponse(_jwtService.GenerateToken(user));
    }
}