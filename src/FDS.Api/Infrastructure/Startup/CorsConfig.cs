namespace FDS.Api.Infrastructure.Startup
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.Linq;

    public static class CorsConfig
    {
        public static IApplicationBuilder UseAllowAllCors(this IApplicationBuilder application, IConfiguration configuration)
        {
            var origins = (configuration?.GetSection("AllowedOrigins:Origins")?.GetChildren()?.Select(x => x.Value) ?? new List<string>()).ToArray();

            application
                .UseCors(builder =>
                {
                    builder
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .WithOrigins(origins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });

            return application;
        }
    }
}
