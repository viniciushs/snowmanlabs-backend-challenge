namespace SnowmanLabsChallenge.Infra.Data.Repository
{
    using SnowmanLabsChallenge.Domain.Interfaces;
    using SnowmanLabsChallenge.Domain.Models;
    using SnowmanLabsChallenge.Infra.Data.Context;

    /// <summary>
    ///     Implementação da <see cref="ICategoryRepository"/>.
    /// </summary>
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CategoryRepository"/> class.
        ///     Construtor padrão de <see cref="CategoryRepository"/>.
        /// </summary>
        /// <param name="context">
        ///     O contexto do repositório. Veja <see cref="DefaultContext"/>.
        /// </param>
        public CategoryRepository(DefaultContext context)
            : base(context)
        {
        }
    }
}
