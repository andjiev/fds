namespace FDS.Common.Infrastructure.MessageQueue
{
    using System;

    public static class UrlBuilder
    {
        public static Uri GetEndpointUrl(Uri address, string route) => new Uri($"rabbitmq://{address.Host}/{route}");

        public static string GetRoute(string name, string queue) => !string.IsNullOrWhiteSpace(name) ? $"{name}_{queue}" : queue;
    }
}
