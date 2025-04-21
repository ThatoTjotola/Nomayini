using MediatR;

namespace Nomayini.Apis.Feature.Messaging.PostMessage
{
    public sealed record PostMessageCommand(string Content, Guid UserId) : IRequest<Unit>;
}
