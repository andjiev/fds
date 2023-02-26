namespace FDS.Update.Service.Infrasturcture.Startup
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Data;
    using System.Data.SqlClient;

    public static class DataContextConfig
    {
        public static IServiceCollection AddDataContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddTransient<IDbConnection>(_ => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
