using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.Business.Services;

namespace Movies.Business
{
    public static class Configuration
    {
        public static void ConfigureBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Options
            services.Configure<Settings.OmdbApi>(configuration.GetSection(nameof(Settings.OmdbApi)));

            // Services
            services.AddScoped<IMovieService, MovieService>();
        }
    }
}