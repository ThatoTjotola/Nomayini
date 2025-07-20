using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Users.Apis.Feature.UploadImage.PostImage;
public class PostImageEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/postimage", async (IMediator mediator, [FromForm] PostImageCommand command) =>
        {
            var result = await mediator.Send(command);
            return Results.Created();
        })
         .DisableAntiforgery()
         .RequireAuthorization()
         .Accepts<PostImageCommand>("multipart/form-data")
        .WithName("UploadImage")
        .WithSummary("Upload an Image")
         .WithDescription("Authenticated user can upload image or media to store on my raspberry pi storage ,Lol why pay for cloud storage when " +
         "you have this.")
        .Produces(StatusCodes.Status400BadRequest)
       .Produces(StatusCodes.Status401Unauthorized)
        .WithOpenApi();
    }
}

