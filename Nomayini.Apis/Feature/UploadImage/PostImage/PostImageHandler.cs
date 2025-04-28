using System.Security.Claims;
using MediatR;

namespace Nomayini.Apis.Feature.UploadImage.PostImage;

public sealed class PostImageCommandHandler(AppDbContext dbContext, IHttpContextAccessor context, IWebHostEnvironment env)
    : IRequestHandler<PostImageCommand, Unit>
{
    public async Task<Unit> Handle(PostImageCommand command, CancellationToken cancellationToken)
    {
        var userId = context.HttpContext?.User?.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        string currentTime = DateTime.Now.ToString("HH:mm:ss tt");

        string path = Path.Combine(env.WebRootPath, "images");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var extension = Path.GetExtension(command.image.FileName);
        var filePath = Path.Combine(path, $"{userId}{extension}");

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await command.image.CopyToAsync(stream, cancellationToken);
        }

        return Unit.Value;
    }
}
