namespace SnowmanLabsChallenge.Infra.Data.Repository
{
    using SnowmanLabsChallenge.Domain.Interfaces;
    using SnowmanLabsChallenge.Domain.Models;
    using SnowmanLabsChallenge.Infra.Data.Context;

    /// <summary>
    ///     Implementação da <see cref="ICommentRepository"/>.
    /// </summary>
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommentRepository"/> class.
        ///     Construtor padrão de <see cref="CommentRepository"/>.
        /// </summary>
        /// <param name="context">
        ///     O contexto do repositório. Veja <see cref="DefaultContext"/>.
        /// </param>
        public CommentRepository(DefaultContext context)
            : base(context)
        {
        }
    }
}
