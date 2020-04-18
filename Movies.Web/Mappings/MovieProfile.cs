using AutoMapper;

namespace Movies.Web.Mappings
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Business.Models.Rating, Models.Movies.Rating>();
            CreateMap<Business.Models.Movie, Models.Movies.Movie>();
        }
    }
}