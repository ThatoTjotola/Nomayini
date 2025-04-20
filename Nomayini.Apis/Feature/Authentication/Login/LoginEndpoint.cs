using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nomayini.Apis.Shared.Exceptions;

namespace Nomayini.Apis.Feature.Auth.Login
{
    public class LoginEndpoint
    {

        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/login", async (
                IMediator mediator,
                [FromBody] LoginQuery query) =>
            {
                try
                {
                    var result = await mediator.Send(query);
                    return Results.Ok(result);
                }
                catch (ProblemDetailsException ex)
                {
                    return Results.Problem(
                        detail: ex.Message,
                        statusCode: ex.StatusCode,
                        title: ex.Title);
                }
            })
            .Produces<LoginResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .WithOpenApi(operation =>
            {
                operation.Summary = "User login endpoint";
                operation.Description = "Authenticates user and returns JWT token";
                return operation;
            });
        }
    }
}
