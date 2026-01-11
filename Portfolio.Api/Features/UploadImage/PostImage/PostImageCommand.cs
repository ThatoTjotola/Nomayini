using MediatR;

namespace Portfolio.Api.Feature.UploadImage.PostImage;

public sealed record PostImageCommand(IFormFile image): IRequest<string>;
