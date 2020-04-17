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
            var apiClient = GetOmdbApiClient();
            return apiClient.GetMovieSearchResults(searchTitle, year);
        }

        private OmdbApiClient GetOmdbApiClient()
        {
            return _omdbApiClient ?? (_omdbApiClient = new OmdbApiClient(_omdbApiSettings.BaseUrl, _omdbApiSettings.Key));
        }
    }
}