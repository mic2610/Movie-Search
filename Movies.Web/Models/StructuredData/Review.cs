using Newtonsoft.Json;

namespace Movies.Web.Models.StructuredData
{
    public class Review
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        
        public ReviewRating ReviewRating { get; set; }
        
        public Author Author { get; set; }

        public string ReviewBody { get; set; }
    }
}