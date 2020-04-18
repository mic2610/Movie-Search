using AutoMapper;

namespace Movies.Web.Mappings
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<Business.Models.Rating, Models.Movies.Rating>();
        }
    }
}