using System.Security.Claims;
using MediatR;
using Users.Apis.Core.Entities;
using Users.Apis.Shared.Exceptions;

namespace Users.Apis.Feature.Messaging.PostMessage
{
    public sealed class PostMessageCommandHandler(IAppDbContext db, IHttpContextAccessor context, ILogger<PostMessageCommandHandler> logger)
    : IRequestHandler<PostMessageCommand, string>
    {
        public async Task<string> Handle(
            PostMessageCommand command,
            CancellationToken cancellationToken)
        {
            var userIdGuid = context.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdGuid == null)
            {
               logger.LogError("user id passed is null");
                return "failed due to null in Guid";
            }
            var userId = Guid.Parse(userIdGuid);
            var message = new Message
            {
                Content = command.Content,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            db.Messages.Add(message);
            await db.SaveChangesAsync(cancellationToken);

            return "Message posted friend";
        }
    }
}
