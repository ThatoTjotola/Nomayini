﻿using System.Security.Claims;
using MediatR;

namespace Users.Apis.Feature.UploadImage.PostImage;

//we wannna change this later too upload media whether its video or image , and also retrieve with efficiency.
public sealed class PostImageCommandHandler(IHttpContextAccessor context, IWebHostEnvironment env)
    : IRequestHandler<PostImageCommand, string>
{
    public async Task<string> Handle(PostImageCommand command, CancellationToken cancellationToken)
    {
        var userId = context.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        string currentTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");

        string path = Path.Combine(env.WebRootPath, "images");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var extension = Path.GetExtension(command.image.FileName);
        var filePath = Path.Combine(path, $"{userId + currentTime}{extension}");

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await command.image.CopyToAsync(stream, cancellationToken);
        }

        return "Media stored successfully";
    }
}
