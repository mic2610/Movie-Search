using Newtonsoft.Json;

namespace Movies.Web.Models.StructuredData
{
    public class ReviewRating
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        
        public string RatingValue { get; set; }
    }
}