namespace SnowmanLabsChallenge.Application.Interfaces
{
    using SnowmanLabsChallenge.Application.Filters;
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Models;

    /// <summary>
    ///     Interface de contrato de Vote Application Service
    /// </summary>
    public interface IVoteAppService : IBaseAppService<VoteViewModel, VoteFilter, Vote>
    {
        VoteCountViewModel Count(VoteFilter filter);
    }
}
