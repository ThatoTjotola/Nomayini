using MediatR;

namespace Portfolio.Api.Feature.Messaging.PostMessage
{
    public sealed record PostMessageCommand(string Content) : IRequest<string>;
}
