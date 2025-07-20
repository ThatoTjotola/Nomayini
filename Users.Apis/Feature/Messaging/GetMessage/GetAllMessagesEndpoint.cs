using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Users.Apis.Feature.Messaging.GetMessage
{
    public class GetAllMessagesEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/getallposts", async (IMediator mediator) =>
            {
                var response = await mediator.Send(new GetAllMessagesQuery());

                return Results.Ok(response);
            }).RequireAuthorization()
            .WithSummary("Retrieves all posted messages by users")
            .WithDescription("This just retrieves all post by all the users youll need too be authenticated in first")
            .Produces(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status400BadRequest)
             .Produces(StatusCodes.Status401Unauthorized)
            .WithOpenApi();
        

        }

    }
}
