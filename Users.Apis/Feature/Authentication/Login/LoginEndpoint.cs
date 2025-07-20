using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Apis.Shared.Exceptions;

namespace Users.Apis.Feature.Auth.Login
{
    public class LoginEndpoint
    {

        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/login", async (
                IMediator mediator,
                [FromBody] LoginQuery query) =>
            {
                var result = await mediator.Send(query);
                return Results.Ok(result);
            })
            .AllowAnonymous()
            .WithName("AuthenticateUser")
            .WithSummary("Logins a user authentication")
            .WithDescription("""
                Authenticates user credentials and returns JWT token for authorization.
                **Required Fields:**
                - `email`: Registered email address
                - `password`: Account password
                **Response Includes:**
                - Access token (JWT)
                """)
            .Produces<LoginResponse>(StatusCodes.Status200OK, contentType: "application/json")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .WithOpenApi();
    }
}
}
