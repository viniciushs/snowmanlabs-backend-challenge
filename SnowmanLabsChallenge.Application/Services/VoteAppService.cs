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
    using System.Linq;
    using SnowmanLabsChallenge.Infra.CrossCutting.Utils.Builders;

    /// <summary>
    ///     Implementação da <see cref="IVoteAppService"/>.
    /// </summary>
    public class VoteAppService : BaseAppService<VoteViewModel, VoteFilter, Vote>, IVoteAppService
    {
        private readonly ITouristSpotRepository touristSpotRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="VoteAppService"/> class.
        ///     Construtor padrão de <see cref="VoteAppService"/>.
        /// </summary>
        /// <param name="uow">
        ///     Contrato do Unit of Work. Veja <see cref="IUnitOfWork"/>.
        /// </param>
        /// <param name="mapper">
        ///     Contrato do AutoMapper. Veja <see cref="IMapper"/>.
        /// </param>
        /// <param name="repository">
        ///     O repositório da entidade Vote. Veja <see cref="IVoteRepository"/>.
        /// </param>
        public VoteAppService(
            IUnitOfWork uow,
            IMapper mapper,
            IVoteRepository repository,
            ITouristSpotRepository touristSpotRepository)
            : base(uow, mapper, repository)
        {
            this.touristSpotRepository = touristSpotRepository;
        }

        public override VoteViewModel Add(VoteViewModel model, bool commit = true)
        {
            this.Validate(model);

            var entity = this.repository.GetBy(f =>
                f.UserId == model.UserId &&
                f.TouristSpotId == model.TouristSpotId,
                true
            ).FirstOrDefault();

            if (entity == null)
            {
                model = base.Add(model, commit);
            }
            else
            {
                model.Id = entity.Id;
                this.Update(model);
            }

            return model;
        }

        public VoteCountViewModel Count(VoteFilter filter)
        {
            if (filter == null)
            {
                throw new SnowmanLabsChallengeException("The filter is empty.");
            }

            if (!filter.TouristSpotId.HasValue)
            {
                throw new SnowmanLabsChallengeException("Invalid tourist spot identifier.");
            }

            var touristSpot = this.touristSpotRepository.GetById(filter.TouristSpotId.Value);
            if (touristSpot == null)
            {
                throw new SnowmanLabsChallengeException("The tourist spot doesn't exist.");
            }

            var result = new VoteCountViewModel();

            var upVotesFilter = new VoteFilter { TouristSpotId = filter.TouristSpotId, Up = true };
            var downVotesFilter = new VoteFilter { TouristSpotId = filter.TouristSpotId, Down = true };

            var upVotesResult = this.GetBy(upVotesFilter);
            var downVotesResult = this.GetBy(downVotesFilter);

            result.TouristSpot = this.mapper.Map<TouristSpotViewModel>(touristSpot);
            result.Up = upVotesResult.Page.TotalElements;
            result.Down = downVotesResult.Page.TotalElements;

            return result;
        }

        public override Expression<Func<Vote, bool>> Filter(VoteFilter filter)
        {
            var expression = base.Filter(filter);

            if (filter != null)
            {
                if (filter.UserId.HasValue)
                {
                    expression = expression.And(f => f.UserId == filter.UserId.Value);

                }

                if (filter.TouristSpotId.HasValue)
                {
                    expression = expression.And(f => f.TouristSpotId == filter.TouristSpotId.Value);
                }

                if (filter.Up.HasValue)
                {
                    expression = expression.And(f => f.Up == filter.Up.Value);
                }

                if (filter.Down.HasValue)
                {
                    expression = expression.And(f => f.Down == filter.Down.Value);
                }
            }

            return expression;
        }
    }
}
