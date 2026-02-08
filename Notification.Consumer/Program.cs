using Notification.Consumer;
using Notification.Consumer.KafkaConsumerServices;
using Notification.Consumer.Settings;

var builder = Host.CreateApplicationBuilder(args);
var GoogleSettings = builder.Configuration.GetSection("GoogleSettings").Get<GoogleSettings>();
var EmailToggle = builder.Configuration.GetSection("NotifyerSettings").Get<NotifyerSettings>();
builder.Services.AddSingleton<IKafkaConsumerService, KafkaConsumerService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddHostedService<Worker>();
var host = builder.Build();
host.Run();
