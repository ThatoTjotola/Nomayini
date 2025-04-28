using MediatR;

namespace Nomayini.Apis.Feature.UploadImage.PostImage;

public sealed record PostImageCommand(IFormFile image): IRequest<Unit>;
