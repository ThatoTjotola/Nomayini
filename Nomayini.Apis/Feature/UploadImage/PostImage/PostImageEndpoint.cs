using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Nomayini.Apis.Feature.UploadImage.PostImage;
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
        .WithSummary("User Uploads an Image")
        .WithOpenApi();
    }
}

