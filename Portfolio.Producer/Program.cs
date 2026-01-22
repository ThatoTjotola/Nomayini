using Portfolio.Producer;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<IKafkaProducerService, KafkaProducerService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
