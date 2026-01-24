using MediatR;
using Portfolio.Api.KafkaServices;

namespace Portfolio.Api.Features.PortfolioArticles.GetPortfolioArticle
{
    public class GetAboutMeArticleEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/getaboutmearticle", async (IMediator mediator, IKafkaProducerService service) =>
            {
                var response = await mediator.Send(new GetAboutMeArticleQuery());
                await service.SendMessageAsync("reaching-out", "someone is learning about you at this time" + DateTimeOffset.Now.ToString());
                return Results.Ok(response);
            }).AllowAnonymous()
            .WithSummary("About Me")
            .WithDescription("All you need to know About Me and stuff like that ")
            .Produces(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();
        }
    }
}
