namespace SnowmanLabsChallenge.Application.AutoMapper
{
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Models;
    using global::AutoMapper;

    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            // Nao remova ou edite a linha abaixo. Utilizado para gerar codigo automatico
            // AddNewConf //
            this.CreateMap<Category, CategoryViewModel>().MaxDepth(1);

            this.CreateMap<Favorite, FavoriteViewModel>().MaxDepth(1);

            this.CreateMap<Picture, PictureViewModel>().MaxDepth(1);

            this.CreateMap<Comment, CommentViewModel>().MaxDepth(1);

            this.CreateMap<TouristSpot, TouristSpotViewModel>().MaxDepth(1)
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom((src, dest) => src.Location.Y))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom((src, dest) => src.Location.X));
        }
    }
}
