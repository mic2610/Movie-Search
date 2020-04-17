using System.Threading.Tasks;
using Movies.Business.Models;
using Movies.Core.Api;

namespace Movies.Business.Api
{
    public class OmdbApiClient : BaseApiClient
    {
        private readonly string _baseUrl;
        private readonly string _key;

        public OmdbApiClient(string baseUrl, string key)
        {
            _baseUrl = baseUrl;
            _key = key;
        }

        public Task<MovieSearchResults> GetMovieSearchResults(string searchTitle, string year = null)
        {
            if (string.IsNullOrWhiteSpace(searchTitle))
                return Task.FromResult<MovieSearchResults>(null);

            // Only query year if it has been passed in
            var query = $"{_baseUrl}?apikey={_key}&s={searchTitle}{(!string.IsNullOrWhiteSpace(year) ? $"&y={year}" : string.Empty)}";
            return GetAsync<MovieSearchResults>(query);
        }
    }
}