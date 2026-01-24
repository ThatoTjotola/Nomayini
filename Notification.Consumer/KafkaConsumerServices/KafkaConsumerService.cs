using Confluent.Kafka;

namespace Notification.Consumer.KafkaConsumerServices;
public class KafkaConsumerService : IKafkaConsumerService
{
    private readonly IConsumer<Null, string> _consumer;
    private readonly IEmailService _emailService;

    public KafkaConsumerService(IEmailService emailService)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "192.168.8.101:9092",
            GroupId = "my-notification-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<Null, string>(config).Build();
        _emailService = emailService;
    }

    public async Task ConsumeMessages(string topic)
    {
        _consumer.Subscribe(topic);

        try
        {
            while (true)
            {
                var consumeResult = await Task.Run(() => _consumer.Consume());
                Console.WriteLine($"Consumed message: {consumeResult.Message.Value}");
                _consumer.Commit(consumeResult);
                //send email after consumption 
                await _emailService.SendEmail();
            }
        }
        catch (ConsumeException e)
        {
            _consumer.Close();
            Console.WriteLine($"Error consuming message: {e.Error.Reason}");
        }
    }
}