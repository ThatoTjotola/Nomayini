namespace Nomayini.Apis.Core.Authentication;
public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string hash, string password);
}
