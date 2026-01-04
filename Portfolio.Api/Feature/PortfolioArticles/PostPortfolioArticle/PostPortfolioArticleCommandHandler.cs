using MediatR;
using Portfolio.Api.Core.Entities;

namespace Portfolio.Api.Feature.PortfolioArticles.PostPortfolioArticle
{
    public sealed class PostPortfolioArticleCommandHandler(IAppDbContext db) : IRequestHandler<PostPortfolioArticleCommand, string>
    {
        public async Task<string> Handle(PostPortfolioArticleCommand command, CancellationToken cancellationToken)
        {
            var portfolio = new PortfolioArticle
            {
                PortfolioContent = command.article,
                Id = 5
            };

            db.PortfolioArticles.Add(portfolio);
            await db.SaveChangesAsync(cancellationToken);
            return "ArticleSavedSuccessfully";
        }
    }
}
