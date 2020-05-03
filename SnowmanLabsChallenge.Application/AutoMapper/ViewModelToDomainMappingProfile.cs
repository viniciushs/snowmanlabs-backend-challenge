namespace SnowmanLabsChallenge.Application.AutoMapper
{
    using global::AutoMapper;
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Models;

    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            // Nao remova ou edite a linha abaixo. Utilizado para gerar codigo automatico
            // AddNewConf //
            this.CreateMap<VoteViewModel, Vote>();

            this.CreateMap<CategoryViewModel, Category>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ConstructUsing(src => new Category(
                    src.Id,
                    src.Uuid,
                    src.CreatedOn,
                    src.Active,
                    src.Name)
                );

            this.CreateMap<FavoriteViewModel, Favorite>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ConstructUsing(src => new Favorite(
                    src.Id,
                    src.Uuid,
                    src.CreatedOn,
                    src.Active,
                    src.UserId,
                    src.TouristSpotId)
                );

            this.CreateMap<PictureViewModel, Picture>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ConstructUsing(src => new Picture(
                    src.Id,
                    src.Uuid,
                    src.CreatedOn,
                    src.Active,
                    src.OwnerId,
                    src.TouristSpotId,
                    src.Url)
                );

            this.CreateMap<CommentViewModel, Comment>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ConstructUsing(src => new Comment(
                    src.Id,
                    src.Uuid,
                    src.CreatedOn,
                    src.Active,
                    src.OwnerId,
                    src.TouristSpotId,
                    src.Content)
                );

            this.CreateMap<TouristSpotViewModel, TouristSpot>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ConstructUsing(src => new TouristSpot(
                    src.Id,
                    src.Uuid,
                    src.CreatedOn,
                    src.Active,
                    src.OwnerId,
                    src.Name,
                    src.CategoryId,
                    src.Latitude,
                    src.Longitude)
                );
        }
    }
}
