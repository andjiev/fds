namespace FDS.Update.Service.Infrasturcture.Startup
{
    using FDS.Update.Repository.Repositories;
    using FDS.Update.Domain.Repositories;
    using Microsoft.Extensions.DependencyInjection;

    public static class RepositoriesConfig
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient<IPackageRepository, PackageRepository>();

            return services;
        }
    }
}
