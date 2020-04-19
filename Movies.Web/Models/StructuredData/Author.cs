using Newtonsoft.Json;

namespace Movies.Web.Models.StructuredData
{
    public class Author
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        
        public string Name { get; set; }
    }
}