namespace Users.Apis.Core.Authentication;
public interface IJwtService
{
    string GenerateToken(User user);
}
