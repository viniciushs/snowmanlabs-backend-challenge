namespace SnowmanLabsChallenge.Application.AutoMapper
{
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Models;
    using global::AutoMapper;
    using System.Collections.Generic;
    using System.Linq;

    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            
            // AddNewConf //
            this.CreateMap<Vote, VoteViewModel>().MaxDepth(1);

            this.CreateMap<Category, CategoryViewModel>().MaxDepth(1);

            this.CreateMap<Favorite, FavoriteViewModel>().MaxDepth(1);

            this.CreateMap<Picture, PictureViewModel>().MaxDepth(1)
                .ForMember(dest => dest.Base64, opt => opt.Ignore());

            this.CreateMap<Comment, CommentViewModel>().MaxDepth(1);

            this.CreateMap<TouristSpot, TouristSpotViewModel>().MaxDepth(1)
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom((src, dest) => src.Location.Y))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom((src, dest) => src.Location.X));
        }
    }
}
