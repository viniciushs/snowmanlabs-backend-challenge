namespace SnowmanLabsChallenge.Infra.Data.Repository
{
    using SnowmanLabsChallenge.Domain.Interfaces;
    using SnowmanLabsChallenge.Domain.Models;
    using SnowmanLabsChallenge.Infra.Data.Context;

    /// <summary>
    ///     Implementação da <see cref="ITouristSpotRepository"/>.
    /// </summary>
    public class TouristSpotRepository : BaseRepository<TouristSpot>, ITouristSpotRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TouristSpotRepository"/> class.
        ///     Construtor padrão de <see cref="TouristSpotRepository"/>.
        /// </summary>
        /// <param name="context">
        ///     O contexto do repositório. Veja <see cref="DefaultContext"/>.
        /// </param>
        public TouristSpotRepository(DefaultContext context)
            : base(context)
        {
        }
    }
}
