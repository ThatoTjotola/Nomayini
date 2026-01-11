using MediatR;

namespace Portfolio.Api.Feature.PortfolioArticles
{
    public sealed record PostPortfolioArticleCommand(string article , int id) : IRequest<string>;
}
