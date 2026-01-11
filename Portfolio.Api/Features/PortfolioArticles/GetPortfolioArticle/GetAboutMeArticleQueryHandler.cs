using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Api.Features.PortfolioArticles.GetPortfolioArticle
{
    public sealed class GetAboutMeArticleQueryHandler(IAppDbContext db) : IRequestHandler<GetAboutMeArticleQuery, List<GetAboutMeArticleResponse>>
    {
        public async Task<List<GetAboutMeArticleResponse>> Handle(
            GetAboutMeArticleQuery query,
            CancellationToken cancellationToken)
        {
            return await db.PortfolioArticles
                 .Where(pa => pa.Id == 10)
                 .Select(m => new GetAboutMeArticleResponse(
                     m.PortfolioContent
                     ))
              .ToListAsync(cancellationToken);
        }
    }
}
