using MediatR;
using Portfolio.Api.Core.Entities;

namespace Portfolio.Api.Feature.PortfolioArticles.PostPortfolioArticle
{
    public sealed class PostPortfolioArticleCommandHandler(IAppDbContext db) : IRequestHandler<PostPortfolioArticleCommand, string>
    {
        /// <summary>
        /// Logic for add a article to the portfolio
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> Handle(PostPortfolioArticleCommand command, CancellationToken cancellationToken)
        {
            var portfolio = new PortfolioArticle
            {
                PortfolioContent = command.article,
                Id = command.id
            };

            db.PortfolioArticles.Add(portfolio);
            await db.SaveChangesAsync(cancellationToken);
            return "ArticleSavedSuccessfully";
        }
    }
}
