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
    public async Task SendEmail(string subject,string content)
    {
        try
        {
            var message = new MimeMessage();
            var from = new MailboxAddress("Jimmy Tjotola", "tjotolajimmy@gmail.com");
            message.From.Add(from);
            var to = new MailboxAddress("Jimmy Tjotola", "tjotolajimmy@gmail.com");
            message.To.Add(to);
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = content
            };
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("thatotjotola@gmail.com", "ibyc eegp clgn xjpm");
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
           logger.LogError(ex, "Error sending email");
        }

    }
}

