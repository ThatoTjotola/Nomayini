using MediatR;

namespace Users.Apis.Feature.UploadImage.GetImage;

public sealed record GetImageQuery () : IRequest<string>;
