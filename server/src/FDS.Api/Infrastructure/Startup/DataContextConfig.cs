namespace FDS.Api.Infrastructure.Startup
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Npgsql;
    using System.Data;

    public static class DataContextConfig
    {
        public static IServiceCollection AddDataContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddTransient<IDbConnection>(_ => new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
