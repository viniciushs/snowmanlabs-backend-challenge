namespace SnowmanLabsChallenge.Application.Interfaces
{
    using SnowmanLabsChallenge.Application.Filters;
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Models;

    /// <summary>
    ///     Interface de contrato de Category Application Service
    /// </summary>
    public interface ICategoryAppService : IBaseAppService<CategoryViewModel, CategoryFilter, Category>
    {
    }
}
