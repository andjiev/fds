namespace FDS.Update.Service.Infrasturcture.Startup
{
    using FDS.Common.Messages;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class SettingsConfig
    {
        public static IServiceCollection AddSettingsServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSingleton<IRabbitMQConfiguration>(x => new Configuration
                {
                    RabbitMQName = configuration.GetValue<string>("RabbitMQ:Name"),
                    RabbitMQAddress = configuration.GetValue<string>("RabbitMQ:Address"),
                    RabbitMQVHost = configuration.GetValue<string>("RabbitMQ:VHost"),
                    RabbitMQPassword = configuration.GetValue<string>("RabbitMQ:Password")
                });

            return services;
        }
    }
}
