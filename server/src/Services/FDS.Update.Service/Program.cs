namespace FDS.Update.Service
{
    using FDS.Update.Service.Infrasturcture.Startup;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseConsoleLifetime()
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddSettingsServices(hostContext.Configuration)
                        .AddAutoMapper(typeof(FDS.Common.Profiles.PackageProfile))
                        .AddDataContextServices(hostContext.Configuration)
                        .AddConsumerServices(hostContext.HostingEnvironment)
                        .AddRepositories()
                        .AddHostedService<Worker>();
                });
    }
}
