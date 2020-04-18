using System.Collections.Generic;
using Newtonsoft.Json;

namespace Movies.Web.Models.StructuredData
{
    public class MovieListing
    {
        [JsonProperty("@context")]
        public string Context { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }

        public List<ItemListElement> ItemListElement { get; set; }
    }

    public class ItemListElement
    {
        [JsonProperty("@type")]
        public string Type { get; set; }

        public string Position { get; set; }

        public MovieItem Item { get; set; }
    }
}