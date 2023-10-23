namespace FDS.Common.Extensions
{
    using FDS.Common.Infrastructure.MessageQueue;
    using FDS.Common.Messages;
    using System;

    public static class ConsumerConfigurationExtensions
    {
        public static Uri GetEndpointUrl(this IRabbitMQConfiguration configuration, Uri address, string queue) => UrlBuilder.GetEndpointUrl(address, UrlBuilder.GetRoute(configuration.RabbitMQName, queue));
    }
}
