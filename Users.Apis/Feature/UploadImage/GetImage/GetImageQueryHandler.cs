using System.Security.Claims;
using System.Text.Json;
using MediatR;

namespace Users.Apis.Feature.UploadImage.GetImage;
public sealed class GetImageQueryHandler(IHttpContextAccessor context, IWebHostEnvironment env) : IRequestHandler<GetImageQuery, string>
{
    public async Task<string> Handle(GetImageQuery request, CancellationToken cancellation)
    {
        var userId = context.HttpContext?.User?.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        string imagePath = Path.Combine(env.WebRootPath, "images");

        if (!Directory.Exists(imagePath))
        {
            return JsonSerializer.Serialize(Array.Empty<string>());

        }

        //WIP CAN BE NOT YET DONE

       var userImageFiles = Directory.GetFiles(imagePath)
          .Select(Path.GetFileName)
            .Where(fileName => fileName.StartsWith($"{userId}_", StringComparison.OrdinalIgnoreCase))
            .ToArray();
        return JsonSerializer.Serialize(userImageFiles);
    }

}

