using System.Security.Claims;
using MediatR;
using Nomayini.Apis.Core.Entities;

namespace Nomayini.Apis.Feature.Messaging.PostMessage
{
    public sealed class PostMessageCommandHandler(AppDbContext db, IHttpContextAccessor context)
    : IRequestHandler<PostMessageCommand, Unit>
    {
        public async Task<Unit> Handle(
            PostMessageCommand command,
            CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var message = new Message
            {
                Content = command.Content,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            db.Messages.Add(message);
            await db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
