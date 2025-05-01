using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Nomayini.Apis.Core.Entities;

namespace Nomayini.Apis.Feature.Messaging.PostMessage
{
    public sealed class PostMessageCommandHandler(AppDbContext db, IHttpContextAccessor context)
    : IRequestHandler<PostMessageCommand, string>
    {
        public async Task<string> Handle(
            PostMessageCommand command,
            CancellationToken cancellationToken)
        {
            var userIdGuid = context.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdGuid == null)
            {
                Console.WriteLine("user id passed is null failed");
                return "unable too post";
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
