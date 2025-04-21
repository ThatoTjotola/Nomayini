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
            }).AllowAnonymous()
            .WithName("RegisterUser")
            .WithSummary("Registers a new user")
            .WithDescription("""
                Registers a new user account. Successful registration returns:
                - User ID
                - Email address
                """)
            .Produces(StatusCodes.Status201Created, contentType: "application/json")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .WithOpenApi();
        }

    }
}
