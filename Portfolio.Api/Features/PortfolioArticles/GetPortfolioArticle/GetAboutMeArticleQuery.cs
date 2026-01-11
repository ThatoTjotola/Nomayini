using MediatR;

namespace Portfolio.Api.Features.PortfolioArticles.GetPortfolioArticle
{
    public sealed record GetAboutMeArticleQuery: IRequest<List<GetAboutMeArticleResponse>>;
}

