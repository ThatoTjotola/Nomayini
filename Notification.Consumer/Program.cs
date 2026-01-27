using Notification.Consumer;
using Notification.Consumer.KafkaConsumerServices;
using Notification.Consumer.Settings;


var builder = Host.CreateApplicationBuilder(args);
var GoogleSettings = builder.Configuration.GetSection("GoogleSettings").Get<GoogleSettings>();
builder.Services.AddSingleton<IKafkaConsumerService,KafkaConsumerService>();   
builder.Services.AddTransient<IEmailService,EmailService>();
builder.Services.AddHostedService<Worker>();
var host = builder.Build();
host.Run();
