using MediatR;

namespace Nomayini.Apis.Feature.UploadImage.GetImage;

public sealed record GetImageQuery () : IRequest<string>;
