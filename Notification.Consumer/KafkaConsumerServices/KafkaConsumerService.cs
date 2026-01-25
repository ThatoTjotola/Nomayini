using Confluent.Kafka;

namespace Notification.Consumer.KafkaConsumerServices;
public class KafkaConsumerService : IKafkaConsumerService
{
    private readonly IConsumer<Null, string> _consumer;
    //Gotta think about making this a singleton in the future and inject service factory here
    private readonly IEmailService _emailService;

    public KafkaConsumerService(IEmailService emailService, IConfiguration configuration)
    {
        var bootstrapServers = configuration["Kafka:BootstrapServers"] ?? "localhost:9092";
        var config = new ConsumerConfig
        {
            BootstrapServers = bootstrapServers,
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
                await _emailService.SendEmail(consumeResult.Message.Value.ToString(), consumeResult.Message.Value.ToString());
            }
        }
        catch (ConsumeException e)
        {
            _consumer.Close();
            Console.WriteLine($"Error consuming message: {e.Error.Reason}");
            //send email of excepiton in regards to consumption
            await _emailService.SendEmail(e.Error.Reason, e.ToString());
        }
    }
}