namespace SnowmanLabsChallenge.Application.Services
{
    using global::AutoMapper;
    using SnowmanLabsChallenge.Application.Filters;
    using SnowmanLabsChallenge.Application.Interfaces;
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Interfaces;
    using SnowmanLabsChallenge.Domain.Models;
    using System.Linq.Expressions;
    using System;
    using SnowmanLabsChallenge.Infra.CrossCutting.Utils.Builders;

    /// <summary>
    ///     Implementação da <see cref="ICommentAppService"/>.
    /// </summary>
    public class CommentAppService : BaseAppService<CommentViewModel, CommentFilter, Comment>, ICommentAppService
    {
        private readonly ITouristSpotRepository touristSpotRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentAppService"/> class.
        ///     Construtor padrão de <see cref="CommentAppService"/>.
        /// </summary>
        /// <param name="uow">
        ///     Contrato do Unit of Work. Veja <see cref="IUnitOfWork"/>.
        /// </param>
        /// <param name="mapper">
        ///     Contrato do AutoMapper. Veja <see cref="IMapper"/>.
        /// </param>
        /// <param name="repository">
        ///     O repositório da entidade Comment. Veja <see cref="ICommentRepository"/>.
        /// </param>

        public CommentAppService(
            IUnitOfWork uow,
            IMapper mapper,
            ICommentRepository repository,
            ITouristSpotRepository touristSpotRepository)
            : base(uow, mapper, repository)
        {
            this.touristSpotRepository = touristSpotRepository;
        }

        public override Expression<Func<Comment, bool>> Filter(CommentFilter filter)
        {
            var expression = base.Filter(filter);

            if (filter != null)
            {
                if (filter.TouristSpotId.HasValue)
                {
                    expression = expression.And(f => f.TouristSpotId == filter.TouristSpotId);
                }

                if (!string.IsNullOrEmpty(filter.Content))
                {
                    expression = expression.And(f => f.Content.Contains(filter.Content));
                }
            }

            return expression;
        }

        public override void Validate(Comment model)
        {
            base.Validate(model);

            var touristSpot = this.touristSpotRepository.GetById(model.TouristSpotId);
            if (touristSpot == null)
            {
                throw new SnowmanLabsChallengeException("Invalid tourist spot.");
            }
        }
    }
}
