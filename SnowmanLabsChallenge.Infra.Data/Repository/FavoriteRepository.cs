namespace SnowmanLabsChallenge.Infra.Data.Repository
{
    using SnowmanLabsChallenge.Domain.Interfaces;
    using SnowmanLabsChallenge.Domain.Models;
    using SnowmanLabsChallenge.Infra.Data.Context;

    /// <summary>
    ///     Implementação da <see cref="IFavoriteRepository"/>.
    /// </summary>
    public class FavoriteRepository : BaseRepository<Favorite>, IFavoriteRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FavoriteRepository"/> class.
        ///     Construtor padrão de <see cref="FavoriteRepository"/>.
        /// </summary>
        /// <param name="context">
        ///     O contexto do repositório. Veja <see cref="DefaultContext"/>.
        /// </param>
        public FavoriteRepository(DefaultContext context)
            : base(context)
        {
        }
    }
}
