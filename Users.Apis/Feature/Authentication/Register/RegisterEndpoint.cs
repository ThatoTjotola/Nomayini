﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Users.Apis.Feature.Auth.Register
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
            .WithName("Register a user")
            .WithSummary("Registers a new user")
            .WithDescription("""
                Registers a new user account.After success go and login with the provided credentials here to get your jwt token for authentication:
                """)
            .Produces(StatusCodes.Status201Created, contentType: "application/json")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .WithOpenApi();
        }

    }
}
