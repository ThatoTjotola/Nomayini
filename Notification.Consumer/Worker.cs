using Notification.Consumer.KafkaConsumerServices;

namespace Notification.Consumer;

//Gonna delete the following class later replace with a producer for RabbitMQ 
public class Worker(ILogger<Worker> logger, IKafkaConsumerService consumerService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                await consumerService.ConsumeMessages("reaching-out");
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
        }
    }
}

