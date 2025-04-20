// BCryptHasher.cs
namespace Nomayini.Apis.Core.Authentication;
class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);

    public bool VerifyPassword(string hash, string password) =>
        BCrypt.Net.BCrypt.Verify(password, hash);
}
