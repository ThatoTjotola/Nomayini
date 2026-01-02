using Portfolio.Api.Core.Entities;

namespace Portfolio.Api.Core.Authentication;
public interface IJwtService
{
    string GenerateToken(PortfolioUser user);
}
