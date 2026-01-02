using MediatR;

namespace Portfolio.Api.Feature.UploadImage.GetImage;

public sealed record GetImageQuery () : IRequest<string>;
