using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Nomayini.Apis.Feature.Messaging.PostMessage;
public class PostMessageEndpoint
{

    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/postmessage", async (IMediator mediator, [FromBody] PostMessageCommand command) =>
        {
            await mediator.Send(command);

        }).RequireAuthorization()
        .DisableAntiforgery()
        .WithSummary("Post a message too all the other users")
            .WithDescription("This just posts a message(post) too users youll need too be authenticated in first")
            .Produces(StatusCodes.Status201Created)
             .Produces(StatusCodes.Status400BadRequest)
             .Produces(StatusCodes.Status401Unauthorized)
            .WithOpenApi();
    }
}
