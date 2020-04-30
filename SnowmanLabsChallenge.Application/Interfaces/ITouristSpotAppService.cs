namespace SnowmanLabsChallenge.Application.Interfaces
{
    using SnowmanLabsChallenge.Application.Filters;
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Models;

    /// <summary>
    ///     Interface de contrato de TouristSpot Application Service
    /// </summary>
    public interface ITouristSpotAppService : IBaseAppService<TouristSpotViewModel, TouristSpotFilter, TouristSpot>
    {
    }
}
