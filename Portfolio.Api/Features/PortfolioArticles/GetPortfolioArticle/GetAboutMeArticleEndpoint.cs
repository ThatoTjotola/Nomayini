using MediatR;
using Portfolio.Api.KafkaServices;
using Scalar.AspNetCore;

namespace Portfolio.Api.Features.PortfolioArticles.GetPortfolioArticle
{
    public class GetAboutMeArticleEndpoint
    {
        //Added to Keep user count
        public static List<int> myList = new List<int>();
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/getaboutmearticle", async (IMediator mediator, IKafkaProducerService service) =>
            {
              
                var response = await mediator.Send(new GetAboutMeArticleQuery());
                foreach (var item in response)
                {
                    myList.Add(1);
                }
                await service.SendMessageAsync("reaching-outs", $"someone is learning about you at this time user number {myList.Count()}");
                return Results.Ok(response);
            }).AllowAnonymous()
            .WithSummary("About Me")
            .Stable()
            .WithDescription("All you need to know About Me and stuff like that ")
            .Produces(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();
        }
    }
}
