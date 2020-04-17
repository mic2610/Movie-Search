using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.Web.Models.Movies
{
    public class MovieSearchResults
    {
        public IList<MovieSummary> Movies { get; set; }

        public string TotalResults { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "At least 3 characters are required and no more than 60")]
        public string Title { get; set; }

        public string Year { get; set; }
    }
}