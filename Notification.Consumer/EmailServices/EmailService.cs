using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Notification.Consumer.Settings;
namespace Notification.Consumer;
public class EmailService(ILogger<EmailService> logger , IOptions<GoogleSettings> googleSettings, IConfiguration configuration) :IEmailService
{
    private readonly GoogleSettings _googleSettings = googleSettings.Value;
    const string myEmail = "tjotolajimmy@gmail.com";
    /// <summary>
    /// email sending logic here to my google account , invoked when someone consumes a message from kafka topic
    /// </summary>
    /// <returns></returns>
    public async Task SendEmail(string subject,string content)
    {
        try
        {
            var message = new MimeMessage();
            var from = new MailboxAddress("Jimmy Tjotola", myEmail);
            message.From.Add(from);
            var to = new MailboxAddress("Jimmy Tjotola", myEmail);
            message.To.Add(to);
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = content
            };
            //use a scoped using here for memory managements
            using (SmtpClient smtp = new SmtpClient())
            {
                //figure out a way to do parrallel processing here or something more efficient 
                await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("thatotjotola@gmail.com", configuration["Google:Password"] ?? _googleSettings.GoogleAppPassword);
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }
        }
        catch (Exception ex)
        {
           logger.LogError(ex, "Error sending email");
        }

    }
}

