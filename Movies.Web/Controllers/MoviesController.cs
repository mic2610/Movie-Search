using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Business.Services;
using Movies.Core.Collections;
using Movies.Core.Extensions;
using Movies.Web.Extensions;
using Movies.Web.Models.Movies;
using Movies.Web.Models.StructuredData;
using Newtonsoft.Json;
using Movie = Movies.Business.Models.Movie;

namespace Movies.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public MoviesController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string title = "Avengers", string year = null, int page = 1)
        {
            var model = await GetMovieSearchResults(title, year, page);
            return View(model);
        }

        public async Task<IActionResult> Details(string id, string searchResultsUrl)
        {
            var movieDto = await _movieService.GetMovie(id);
            if (movieDto == null)
                return NotFound();

            var movieStructuredData = BuildMovie(movieDto);
            var movie = _mapper.Map<Business.Models.Movie, Web.Models.Movies.Movie>(movieDto);
            var movieDetails = new MovieDetails
            {
                MoveStructuredData = movieStructuredData != null ? JsonConvert.SerializeObject(movieStructuredData) : null,
                Movie = movie,
                SearchResultsUrl = searchResultsUrl
            };

            return View(movieDetails);
        }

        private async Task<MovieSearchResults> GetMovieSearchResults(string title, string year = null, int page = 1)
        {
            var movieSearchResults = await _movieService.GetMovieSearchResults(title, year, page);
            var movieListing = movieSearchResults != null ? BuildMovieListing(movieSearchResults.Search) : null;
            var moveListingStructuredData = movieListing != null ? JsonConvert.SerializeObject(movieListing) : null;
            var pageSize = 10;
            return new MovieSearchResults
            {
                // Pass movies into a PageCollection, there is no need to skip using .Skip() or take using .Take() as it is handled by the API
                Movies = movieSearchResults != null && !movieSearchResults.Search.IsNullOrEmpty()
                    ? new PagedCollection<MovieSummary>(
                        movieSearchResults.Search?.Select(m => new MovieSummary { ImageUrl = m.Poster, imdbID = m.imdbID, Title = m.Title, Year = m.Year, SearchResultsUrl = Url.Action(nameof(Index), new { title = title, year = year, page = page }) }),
                        page,
                        pageSize,
                        !string.IsNullOrWhiteSpace(movieSearchResults.TotalResults) ? Convert.ToInt32(movieSearchResults.TotalResults) : (int?)null)
                    : null,
                Year = year,
                Title = title,
                MoveListingStructuredData = moveListingStructuredData
            };
        }

        private MovieListing BuildMovieListing(IList<Business.Models.MovieSummary> movieSummaries)
        {
            if (movieSummaries.IsNullOrEmpty())
                return null;

            var movieListing = new MovieListing
            {
                Context = "https://schema.org",
                Type = "ItemList",
                ItemListElement = new List<ItemListElement>(movieSummaries.Count)
            };

            var positionIncrement = 0;

            // Absolute url
            //var searchResultsUrl = Url.AbsoluteAction(nameof(Index), "Movies");

            // Relative Url was used instead of the above absolute as the above absolute url was not valid within the testing tool: https://search.google.com/structured-data/testing-tool
            var searchResultsUrl = Url.Action(nameof(Index), "Movies");
            foreach (var movieSummary in movieSummaries)
            {
                movieListing.ItemListElement.Add(new ItemListElement
                {
                    Type = "ListItem",
                    Position = $"{++positionIncrement}",
                    Item = new MovieItem
                    {
                        Name = movieSummary.Title,
                        Type = "Movie",
                        Image = movieSummary.Poster,

                        // Movie summary only has a single year, so to avoid warnings within the structured data testing tool: https://search.google.com/structured-data/testing-tool , we must place defaults for the dateCreated
                        DateCreated = $"{movieSummary.Year}-01-01",

                        // Url must be unique for each result using the action tag
                        Url = $"{searchResultsUrl}#{movieSummary.imdbID}"
                    }
                });
            }

            return movieListing;
        }

        private MovieItem BuildMovie(Movie movie)
        {
            if (movie == null)
                return null;

            return new MovieItem
            {
                Context = "http://schema.org",
                Type = "Movie",
                Name = movie.Title,
                Image = movie.Poster,
                DateCreated = movie.Released,
                Director = new Director { Name = movie.Director, Type = "Person" },
                Url = $"{Url.Action(nameof(Details), "Movies", new { id = movie.imdbID })}#{movie.imdbID}",
                Description = movie.Plot
            };
        }
    }
}