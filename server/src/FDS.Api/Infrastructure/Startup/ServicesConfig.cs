namespace FDS.Api.Infrastructure.Startup
{
    using FDS.Api.Infrastructur.Services;
    using FDS.Package.Service.Commands;
    using FDS.Package.Service.Commands.Handlers;
    using FDS.Package.Service.Queries;
    using FDS.Package.Service.Queries.Handlers;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Collections.Generic;
    using Models = FDS.Common.Models;

    public static class ServicesConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IRequestHandler<GetPackagesQuery, List<Models.Package>>, GetPackagesQueryHandler>()
                .AddTransient<IRequestHandler<UpdatePackageCommand, Models.Package>, UpdatePackageCommandHandler>()
                .AddTransient<IRequestHandler<ImportPackagesCommand, Unit>, ImportPackagesCommandHandler>()
                .AddTransient<IRequestHandler<CreatePackageCommand, Unit>, CreatePackageCommandHandler>()
                .AddTransient<IRequestHandler<UpdateSelectedPackagesCommand, List<Models.Package>>, UpdateSelectedPackagesCommandHandler>()
                .AddTransient<IRequestHandler<DeleteSelectedPackagesCommand, List<Models.Package>>, DeleteSelectedPackagesCommandHandler>();

            services.AddSingleton<IHostedService, BusService>();

            return services;
        }
    }
}
