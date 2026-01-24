namespace Portfolio.Api.KafkaServices;

public interface IKafkaProducerService
{
    Task SendMessageAsync(string topic, string message);
}
