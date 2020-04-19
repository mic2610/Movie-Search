using System.Collections.Generic;

namespace Movies.Business.Models
{
    public class MovieSearchResults
    {
        public IList<MovieSummary> Search { get; set; }

        public string TotalResults { get; set; }

        public string Response { get; set; }
    }
}