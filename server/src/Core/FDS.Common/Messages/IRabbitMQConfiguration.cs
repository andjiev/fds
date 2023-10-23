namespace FDS.Common.Messages
{
    public interface IRabbitMQConfiguration
    {
        string RabbitMQName { get; }

        string RabbitMQAddress { get; }

        string RabbitMQVHost { get; }

        string RabbitMQPassword { get; }
    }
}
