namespace Portfolio.Api.Core.Entities;
public class Message
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }

    public PortfolioUser ?User { get; set; }
}

