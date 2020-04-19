namespace Movies.Web.Models.Movies
{
    public class MovieSummary
    {
        public string Title { get; set; }

        public string Year { get; set; }

        public string imdbID { get; set; }

        public string Type { get; set; }

        public string ImageUrl { get; set; }
        
        public string SearchResultsUrl { get; set; }
    }
}