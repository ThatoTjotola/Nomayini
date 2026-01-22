using Confluent.Kafka;

namespace Portfolio.Producer
{
    //Gonna delete the following class later replace with a producer for RabbitMQ 
    public class Worker(ILogger<Worker> logger, IKafkaProducerService producerService) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (logger.IsEnabled(LogLevel.Information))
                {
                        Random ran = new Random();
                        var nextString = ran.Next(10000);
                        await producerService.SendMessageAsync("reaching-out", nextString.ToString());
                        logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
