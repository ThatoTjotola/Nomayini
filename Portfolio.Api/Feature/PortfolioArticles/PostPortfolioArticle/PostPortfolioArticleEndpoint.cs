using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Api.Feature.PortfolioArticles.PostPortfolioArticle
{
    /// <summary>
    /// For portfolio articles and stuff 
    /// </summary>
    public class PostPortfolioArticleEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("postarticle", async (IMediator mediator, [FromBody] PostPortfolioArticleCommand command) =>
            {
                await mediator.Send(command);
            }).WithDisplayName("Add a article")
            .WithSummary("Adds a article to portfolio");
        }
    }
}
