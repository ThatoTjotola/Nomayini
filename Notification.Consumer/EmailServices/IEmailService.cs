namespace Notification.Consumer;

public interface IEmailService
{
    Task SendEmail(string body ,string content);
}

