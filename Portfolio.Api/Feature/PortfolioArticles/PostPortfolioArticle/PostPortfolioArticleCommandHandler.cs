using MediatR;
using Portfolio.Api.Feature.PortfolioArticles;

namespace Portfolio.Api.Feature.PortfolioArticles.PostPortfolioArticle
{
    public sealed class PostPortfolioArticleCommandHandler(IAppDbContext appDbContext) : IRequestHandler<PostPortfolioArticleCommand, string>;
    {
        public async Task<string> Handle(PostPortfolioArticleCommand command, CancellationToken cancellationToken)
        {

        }
    }
}
