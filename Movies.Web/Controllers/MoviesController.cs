using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Movies.Business;
using Movies.Business.Services;
using Movies.Web.Models.Movies;

namespace Movies.Web.Controllers
{
    public class MoviesController : Controller
    {
        // Options
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await GetMovieSearchResults("Avengers");
            return View(model);
        }

        public async Task<IActionResult> SearchResults(string title, string year = null)
        {
            var model = await GetMovieSearchResults(title, year);
            return View(model);
        }

        private async Task<MovieSearchResults> GetMovieSearchResults(string title, string year = null)
        {
            var movieSearchResults = await _movieService.GetMovieSearchResults(title, year);
            return new MovieSearchResults
            {
                Movies = movieSearchResults?.Search?.Select(m => new MovieSummary { ImageUrl = m.Poster, imdbID = m.imdbID, Title = m.Title, Year = m.Year }).ToList(),
                TotalResults = movieSearchResults?.TotalResults,
                Year = year,
                Title = title
            };
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
