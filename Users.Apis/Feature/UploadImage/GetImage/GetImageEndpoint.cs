using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Users.Apis.Feature.UploadImage.GetImage;
public class GetImageEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/getUserImages", async (IMediator mediator) =>
        {
            var query = new GetImageQuery();
            var result = await mediator.Send(query);
            return Results.Ok(result);
        }).RequireAuthorization()
        .WithName("GetUserImages")
        .WithSummary("WIP please ignore ")
         .WithOpenApi();
    }
}
