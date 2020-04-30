namespace SnowmanLabsChallenge.Application.Interfaces
{
    using SnowmanLabsChallenge.Application.Filters;
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Models;

    /// <summary>
    ///     Interface de contrato de Comment Application Service
    /// </summary>
    public interface ICommentAppService : IBaseAppService<CommentViewModel, CommentFilter, Comment>
    {
    }
}
