using MediatR;

namespace Users.Apis.Feature.Messaging.PostMessage
{
    public sealed record PostMessageCommand(string Content) : IRequest<string>;
}
