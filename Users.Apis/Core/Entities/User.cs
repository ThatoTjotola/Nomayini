
using System.ComponentModel.DataAnnotations;
using Users.Apis.Core.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

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

// i need to use open closed principle here 
enum UserType
{
   SuperUser ,
   Viewer
    
}