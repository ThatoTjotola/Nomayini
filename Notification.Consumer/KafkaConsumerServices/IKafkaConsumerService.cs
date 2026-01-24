namespace Notification.Consumer.KafkaConsumerServices;

public interface IKafkaConsumerService
{
    Task ConsumeMessages(string topic);
}
