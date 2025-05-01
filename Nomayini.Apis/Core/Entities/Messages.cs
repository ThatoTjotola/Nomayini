namespace Nomayini.Apis.Core.Entities;
public class Message
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }

    public User ?User { get; set; }
}

