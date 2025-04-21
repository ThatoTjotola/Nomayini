namespace Nomayini.Apis.Feature.Messaging.GetMessage
{
    public sealed record GetAllMessagesResponse(
    string Content,
    DateTime CreatedAt,
    string AuthorEmail);
}