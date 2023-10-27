namespace FDS.Api.Infrastructure.Startup
{
    using FDS.Api.Infrastructur.Services;
    using FDS.Package.Service.Commands;
    using FDS.Package.Service.Commands.Handlers;
    using FDS.Package.Service.Models;
    using FDS.Package.Service.Queries;
    using FDS.Package.Service.Queries.Handlers;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Collections.Generic;

    public static class ServicesConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IRequestHandler<GetPackagesQuery, List<Package>>, GetPackagesQueryHandler>()
                .AddTransient<IRequestHandler<UpdatePackageVersionCommand, Package>, UpdatePackageVersionCommandHandler>()
                .AddTransient<IRequestHandler<UpdateAllPackagesCommand, List<Package>>, UpdateAllPackagesCommandHandler>()
                .AddTransient<IRequestHandler<UpdatePackagesToInitialStateCommand, List<Package>>, UpdatePackagesToInitialStateCommandHandler>()
                .AddTransient<IRequestHandler<CreatePackageVersionCommand, Package>, CreatePackageVersionCommandHandler>();

            services.AddSingleton<IHostedService, BusService>();

            return services;
        }
    }
}
