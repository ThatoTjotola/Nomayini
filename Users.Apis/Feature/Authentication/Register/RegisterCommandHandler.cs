using MediatR;
using Users.Apis.Core.Authentication;
using Users.Apis.Feature.Auth.Register;

namespace Users.Apis.Feature.Auth;
/// <summary>
/// Registration component 
/// </summary>
/// <param name="context"></param>
/// <param name="passwordHasher"></param>
/// <param name="jwtService"></param>
public sealed class RegisterCommandHandler( IAppDbContext context,IPasswordHasher passwordHasher, IJwtService jwtService) : IRequestHandler<RegisterCommand, AuthResponse>
{

    public async Task<AuthResponse> Handle(RegisterCommand request,CancellationToken cancellationToken)
    {
        var user = new User
        {
            Email = request.Email,
            PasswordHash = passwordHasher.HashPassword(request.Password)
        };

        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);

        return new AuthResponse(jwtService.GenerateToken(user));
    }
}