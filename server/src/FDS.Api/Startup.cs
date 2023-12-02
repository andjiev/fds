namespace FDS.Api
{
    using AutoMapper;
    using FDS.Api.Infrastructure.Startup;
    using FDS.Package.Service.Hubs;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Rewrite;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Reflection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(Package.Domain.Profiles.PackageProfile), typeof(FDS.Common.Profiles.PackageProfile));
            services.AddSwaggerServices();

            services.AddCors();
            services.AddSignalR();

            services
                .AddSettingsServices(Configuration)
                .AddMessageQueueConfiguration(Configuration)
                .AddDataContextServices(Configuration)
                .AddRepositories()
                .AddServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseAllowAllCors(Configuration)
                .UseCustomSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app
              .UseRewriter(new RewriteOptions()
              .AddRedirect("^$", "swagger/index.html"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<PackageHub>("/hubs/package");
            });
        }
    }
}
