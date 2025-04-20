using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Nomayini.Apis.Feature.Auth.Register
{
    public class RegisterEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/register", async (IMediator mediator, [FromBody] RegisterCommand command) =>
            {
                var result = await mediator.Send(command);
                return Results.Created();
            });
        }

    }
}
