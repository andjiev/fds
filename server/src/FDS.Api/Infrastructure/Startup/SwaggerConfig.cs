namespace FDS.Api.Infrastructure.Startup
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            return services
               .AddSwaggerGen(c =>
               {
                   c.CustomSchemaIds(s => s.FullName);
                   c.SwaggerDoc("v1", new OpenApiInfo { Title = "FDS API", Version = "v1" });
               });
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder application)
        {
            return application
                .UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
        }
    }
}
