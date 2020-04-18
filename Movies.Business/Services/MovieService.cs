using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Movies.Business.Api;
using Movies.Business.Models;

namespace Movies.Business.Services
{
    public class MovieService : IMovieService
    {
        // Options
        private readonly Settings.OmdbApi _omdbApiSettings;

        // ApiClients
        private OmdbApiClient _omdbApiClient;

        public MovieService(IOptions<Settings.OmdbApi> omdbApiSettings)
        {
            _omdbApiSettings = omdbApiSettings.Value;
        }

        public Task<MovieSearchResults> GetMovieSearchResults(string searchTitle, string year = null)
        {
            if (string.IsNullOrWhiteSpace(searchTitle))
                return Task.FromResult<MovieSearchResults>(null);

            var apiClient = GetOmdbApiClient();
            return apiClient.GetMovieSearchResults(searchTitle, year);
        }

        public Task<Movie> GetMovie(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return Task.FromResult<Movie>(null);

            var apiClient = GetOmdbApiClient();
            return apiClient.GetMovie(id);
        }

        private OmdbApiClient GetOmdbApiClient()
        {
            return _omdbApiClient ?? (_omdbApiClient = new OmdbApiClient(_omdbApiSettings.BaseUrl, _omdbApiSettings.Key));
        }
    }
}