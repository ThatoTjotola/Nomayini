using Notification.Consumer;
using Notification.Consumer.KafkaConsumerServices;


var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<IKafkaConsumerService,KafkaConsumerService>();   
builder.Services.AddTransient<IEmailService,EmailService>();
builder.Services.AddHostedService<Worker>();
var host = builder.Build();
host.Run();
