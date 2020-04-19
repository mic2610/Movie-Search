using Newtonsoft.Json;

namespace Movies.Web.Models.StructuredData
{
    public class Director
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        
        public string Name { get; set; }
    }
}