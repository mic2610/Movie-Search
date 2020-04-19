using Newtonsoft.Json;

namespace Movies.Web.Models.StructuredData
{
    public class MovieItem
    {
        [JsonProperty("@context")]
        public string Context { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }
        
        public string Image { get; set; }
        
        public string DateCreated { get; set; }
        
        public Director Director { get; set; }
        
        public Review Review { get; set; }

        public string Description { get; set; }
    }
}