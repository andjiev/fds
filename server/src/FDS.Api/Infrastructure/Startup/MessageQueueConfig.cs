namespace FDS.Api.Infrastructure.Startup
{
    using FDS.Common.Infrastructure.MessageQueue;
    using FDS.Package.Service.Consumers;
    using MassTransit;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class MessageQueueConfig
    {
        public static IServiceCollection AddMessageQueueConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddTransient<PackageUpdatedConsumer>()
                .AddTransient<SyncPackagesCompletedConsumer>()
                .AddTransient<PackageInstalledConsumer>()
                .AddTransient<PackageDeletedConsumer>();

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration.GetValue<string>("RabbitMQ:Address"), 5672, configuration.GetValue<string>("RabbitMQ:VHost"), h =>
                    {
                        h.Password(configuration.GetValue<string>("RabbitMQ:Password"));
                    });

                    cfg.ReceiveEndpoint(UrlBuilder.GetRoute(configuration.GetValue<string>("RabbitMQ:Name"), "PackageUpdated"), e =>
                    {
                        e.Consumer<PackageUpdatedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint(UrlBuilder.GetRoute(configuration.GetValue<string>("RabbitMQ:Name"), "SyncPackagesCompleted"), e =>
                    {
                        e.Consumer<SyncPackagesCompletedConsumer>(context);
                    });
                    
                    cfg.ReceiveEndpoint(UrlBuilder.GetRoute(configuration.GetValue<string>("RabbitMQ:Name"), "PackageInstalled"), e =>
                    {
                        e.Consumer<PackageInstalledConsumer>(context);
                    });

                    cfg.ReceiveEndpoint(UrlBuilder.GetRoute(configuration.GetValue<string>("RabbitMQ:Name"), "PackageDeleted"), e =>
                    {
                        e.Consumer<PackageDeletedConsumer>(context);
                    });
                });
            });

            return services;
        }
    }
}
