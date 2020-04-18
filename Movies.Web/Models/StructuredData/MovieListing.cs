using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Item Item { get; set; }
    }

    public class Item
    {
        [JsonProperty("@type")]
        public string Type { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }
        
        public string Image { get; set; }
        
        public string DateCreated { get; set; }
        
        public Director Director { get; set; }
        
        public Review Review { get; set; }

        public AggregateRating AggregateRating { get; set; }
    }

    public class Director
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        
        public string Name { get; set; }
    }

    public class ReviewRating
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        
        public string RatingValue { get; set; }
    }

    public class Author
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        
        public string Name { get; set; }
    }

    public class Review
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        
        public ReviewRating ReviewRating { get; set; }
        
        public Author Author { get; set; }

        public string ReviewBody { get; set; }
    }

    public class AggregateRating
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
        
        public string RatingValue { get; set; }
        
        public string BestRating { get; set; }
        
        public string RatingCount { get; set; }
    }
}