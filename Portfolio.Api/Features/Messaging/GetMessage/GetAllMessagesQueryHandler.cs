using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Api.Feature.Messaging.GetMessage;

/// <summary>
/// Logic to return all messages accordingly added no tracking for performance boost
/// </summary>
/// <param name="db"></param>
public sealed class GetAllMessagesQueryHandler(IAppDbContext db): IRequestHandler<GetAllMessagesQuery, List<GetAllMessagesResponse>>
{
    public async Task<List<GetAllMessagesResponse>> Handle(
        GetAllMessagesQuery query,
        CancellationToken cancellationToken)
    {
        //Added no tracking for performance boost
        return await db.Messages
            .Include(m => m.User)
            .OrderByDescending(m => m.CreatedAt)
            .Select(m => new GetAllMessagesResponse(
                m.Content,
                m.CreatedAt,
                m.User.Email))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
