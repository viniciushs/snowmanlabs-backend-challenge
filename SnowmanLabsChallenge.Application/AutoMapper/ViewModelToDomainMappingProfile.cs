namespace SnowmanLabsChallenge.Application.AutoMapper
{
    using global::AutoMapper;
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Models;
    using System;

    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            // Nao remova ou edite a linha abaixo. Utilizado para gerar codigo automatico
            // AddNewConf //
            this.CreateMap<CategoryViewModel, Category>();

            this.CreateMap<FavoriteViewModel, Favorite>();

            this.CreateMap<PictureViewModel, Picture>();

            this.CreateMap<CommentViewModel, Comment>();

            this.CreateMap<TouristSpotViewModel, TouristSpot>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ConstructUsing(src => new TouristSpot(
                    src.Id,
                    src.Uuid,
                    src.CreatedOn,
                    src.Active,
                    src.Name,
                    src.CategoryId,
                    src.Latitude,
                    src.Longitude)
                );
        }
    }
}
