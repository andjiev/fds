namespace FDS.Api.Infrastructure.Startup
{
    using FDS.Package.Repository.Repositories;
    using FDS.Package.Domain.Repositories;
    using Microsoft.Extensions.DependencyInjection;

    public static class RepositoriesConfig
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient<IPackageRepository, PackageRepository>()
                .AddTransient<IVersionRepository, VersionRepository>();

            return services;
        }
    }
}
