using System.Threading.Tasks;
using Movies.Business.Models;

namespace Movies.Business.Services
{
    public interface IMovieService
    {
        Task<MovieSearchResults> GetMovieSearchResults(string searchTitle, string year = null);
    }
}