using MediatR;

namespace Nomayini.Apis.Feature.Messaging.GetMessage
{
    public sealed record GetAllMessagesQuery : IRequest<List<GetAllMessagesResponse>>;
}
