using MediatR;

namespace Portfolio.Api.Feature.Messaging.GetMessage
{
    public sealed record GetAllMessagesQuery : IRequest<List<GetAllMessagesResponse>>;
}
