using MediatR;
using Nomayini.Apis.Core.Entities;

namespace Nomayini.Apis.Feature.Messaging.PostMessage
{
    public sealed class PostMessageCommandHandler(AppDbContext db)
    : IRequestHandler<PostMessageCommand, Unit>
    {
        public async Task<Unit> Handle(
            PostMessageCommand command,
            CancellationToken cancellationToken)
        {
            var message = new Message
            {
                Content = command.Content,
                UserId = command.UserId,
                CreatedAt = DateTime.UtcNow
            };

            db.Messages.Add(message);
            await db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
