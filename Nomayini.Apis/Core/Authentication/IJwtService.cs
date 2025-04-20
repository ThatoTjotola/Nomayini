namespace Nomayini.Apis.Core.Authentication;
public interface IJwtService
{
    string GenerateToken(User user);
}
