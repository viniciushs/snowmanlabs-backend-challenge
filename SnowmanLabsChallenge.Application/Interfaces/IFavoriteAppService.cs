namespace SnowmanLabsChallenge.Application.Interfaces
{
    using SnowmanLabsChallenge.Application.Filters;
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Models;

    /// <summary>
    ///     Interface de contrato de Favorite Application Service
    /// </summary>
    public interface IFavoriteAppService : IBaseAppService<FavoriteViewModel, FavoriteFilter, Favorite>
    {
    }
}
