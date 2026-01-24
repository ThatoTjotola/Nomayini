using Confluent.Kafka;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
namespace Notification.Consumer;
public class EmailService(ILogger<EmailService> logger) :IEmailService
{
    /// <summary>
    /// email sending logic here to my google account , invoked when someone consumes a message from kafka topic
    /// </summary>
    /// <returns></returns>
    public async Task SendEmail()
    {
        try
        {
            var message = new MimeMessage();
            var from = new MailboxAddress("Jimmy Tjotola", "tjotolajimmy@gmail.com");
            message.From.Add(from);
            var to = new MailboxAddress("Jimmy Tjotola", "tjotolajimmy@gmail.com");
            message.To.Add(to);
            message.Subject = "Someone has learnt something about you";
            message.Body = new TextPart("plain")
            {
                Text = "This is a notification email sent from the Kafka consumer service notifiying you my good someone has learnt something about you."
            };
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("tjotolajimmy@gmail.com", "xggp voeb oetu knqz");
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
           logger.LogError(ex, "Error sending email");
        }

    }
}

