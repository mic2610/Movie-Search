using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Movies.Business
{
    public static class Configuration
    {
        public static void ConfigureBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Options
            services.Configure<Settings.OmdbApi>(config => configuration.GetSection(nameof(Settings.OmdbApi)));
        }
    }
}