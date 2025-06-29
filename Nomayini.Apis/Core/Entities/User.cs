
using System.ComponentModel.DataAnnotations;
using Nomayini.Apis.Core.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    //possibly remove this use fluentvalidation
    [Required]
    [MaxLength(254)]
    public string Email { get; set; } = string.Empty;

    [Required]
    UserType UserType { get; set; }

    [Required]
    [MaxLength(256)]
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public ICollection<Message> Messages { get; set; } = new List<Message>();

}

enum UserType
{
    Boss,
    Worker
}