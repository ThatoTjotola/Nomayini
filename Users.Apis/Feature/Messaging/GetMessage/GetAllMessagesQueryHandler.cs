using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Users.Apis.Feature.Messaging.GetMessage;
public sealed class GetAllMessagesQueryHandler(IAppDbContext db)
: IRequestHandler<GetAllMessagesQuery, List<GetAllMessagesResponse>>
{
    public async Task<List<GetAllMessagesResponse>> Handle(
        GetAllMessagesQuery query,
        CancellationToken cancellationToken)
    {
        return await db.Messages
            .Include(m => m.User)
            .OrderByDescending(m => m.CreatedAt)
            .Select(m => new GetAllMessagesResponse(
                m.Content,
                m.CreatedAt,
                m.User.Email))
            .ToListAsync(cancellationToken);
    }
}
