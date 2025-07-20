using MediatR;

namespace Users.Apis.Feature.UploadImage.PostImage;

public sealed record PostImageCommand(IFormFile image): IRequest<string>;
