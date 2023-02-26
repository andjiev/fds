namespace FDS.Common.Messages
{
    public class Configuration : IRabbitMQConfiguration
    {
        public string RabbitMQName { get; set; }

        public string RabbitMQAddress { get; set; }

        public string RabbitMQVHost { get; set; }

        public string RabbitMQPassword { get; set; }
    }
}
