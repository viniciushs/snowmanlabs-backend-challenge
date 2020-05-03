namespace SnowmanLabsChallenge.Infra.Data.Repository
{
    using SnowmanLabsChallenge.Domain.Interfaces;
    using SnowmanLabsChallenge.Domain.Models;
    using SnowmanLabsChallenge.Infra.Data.Context;

    /// <summary>
    ///     Implementação da <see cref="IVoteRepository"/>.
    /// </summary>
    public class VoteRepository : BaseRepository<Vote>, IVoteRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VoteRepository"/> class.
        ///     Construtor padrão de <see cref="VoteRepository"/>.
        /// </summary>
        /// <param name="context">
        ///     O contexto do repositório. Veja <see cref="DefaultContext"/>.
        /// </param>
        public VoteRepository(DefaultContext context)
            : base(context)
        {
        }
    }
}
