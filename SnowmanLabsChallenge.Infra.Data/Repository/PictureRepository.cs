namespace SnowmanLabsChallenge.Infra.Data.Repository
{
    using SnowmanLabsChallenge.Domain.Interfaces;
    using SnowmanLabsChallenge.Domain.Models;
    using SnowmanLabsChallenge.Infra.Data.Context;

    /// <summary>
    ///     Implementação da <see cref="IPictureRepository"/>.
    /// </summary>
    public class PictureRepository : BaseRepository<Picture>, IPictureRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PictureRepository"/> class.
        ///     Construtor padrão de <see cref="PictureRepository"/>.
        /// </summary>
        /// <param name="context">
        ///     O contexto do repositório. Veja <see cref="DefaultContext"/>.
        /// </param>
        public PictureRepository(DefaultContext context)
            : base(context)
        {
        }
    }
}
