using MediatR;

namespace Users.Apis.Feature.Messaging.GetMessage
{
    public sealed record GetAllMessagesQuery : IRequest<List<GetAllMessagesResponse>>;
}
