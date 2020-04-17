using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Movies.Business;
using Movies.Web.Models.Movies;

namespace Movies.Web.Controllers
{
    public class MoviesController : Controller
    {
        // Options
        private readonly Settings.OmdbApi _omdbApiSettings;

        public MoviesController(IOptions<Settings.OmdbApi> omdbApiSettings)
        {
            _omdbApiSettings = omdbApiSettings.Value;
        }

        public IActionResult Index()
        {
            var key = _omdbApiSettings.Key;
            var model = new SearchResults();
            model.LoadSampleData();
            return View(model);
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
