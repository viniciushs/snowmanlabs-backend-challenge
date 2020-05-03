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
    using SnowmanLabsChallenge.Infra.CrossCutting.Core.Messages;
    using SnowmanLabsChallenge.Infra.CrossCutting.Utils.Builders;

    /// <summary>
    ///     Implementação da <see cref="IFavoriteAppService"/>.
    /// </summary>
    public class FavoriteAppService : BaseAppService<FavoriteViewModel, FavoriteFilter, Favorite>, IFavoriteAppService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FavoriteAppService"/> class.
        ///     Construtor padrão de <see cref="FavoriteAppService"/>.
        /// </summary>
        /// <param name="uow">
        ///     Contrato do Unit of Work. Veja <see cref="IUnitOfWork"/>.
        /// </param>
        /// <param name="mapper">
        ///     Contrato do AutoMapper. Veja <see cref="IMapper"/>.
        /// </param>
        /// <param name="repository">
        ///     O repositório da entidade Favorite. Veja <see cref="IFavoriteRepository"/>.
        /// </param>

        public FavoriteAppService(
            IUnitOfWork uow,
            IMapper mapper,
            IFavoriteRepository repository)
            : base(uow, mapper, repository)
        {
        }

        public override FavoriteViewModel Add(FavoriteViewModel model, bool commit = true)
        {
            this.Validate(model);

            var entity = this.repository.GetBy(f =>
                f.UserId == model.UserId &&
                f.TouristSpotId == model.TouristSpotId,
                true
            ).FirstOrDefault();

            if (entity != null)
            {
                this.mapper.Map<Favorite, FavoriteViewModel>(entity, model);
            }
            else
            {
                model = base.Add(model, commit);
            }

            return model;
        }

        public void Remove(int touristSpotId, Guid requesterId, bool commit = true)
        {
            var entity = this.repository.GetBy(f => f.TouristSpotId == touristSpotId && f.UserId == requesterId).FirstOrDefault();
            if (entity == null)
            {
                return;
            }

            var id = entity.Id;
            this.Remove(id, commit);
        }

        public override Expression<Func<Favorite, bool>> Filter(FavoriteFilter filter)
        {
            var expression = base.Filter(filter);

            if (filter != null)
            {
                if (filter.TouristSpotId.HasValue)
                {
                    expression = expression.And(f => f.TouristSpotId == filter.TouristSpotId.Value);
                }

                if (filter.UserId.HasValue)
                {
                    expression = expression.And(f => f.UserId == filter.UserId.Value);
                }
            }

            return expression;
        }

        public override Func<Favorite, object> OrderBy(FavoriteFilter filter)
        {
            Func<Favorite, object> orderBy;

            switch (filter.SortBy.ToLower())
            {
                // Adicione outras ordenações
                // case "descricao":
                //     orderBy = (x => x.Descricao);
                //     break;
                default:
                    orderBy = base.OrderBy(filter);
                    break;
            }

            return orderBy;
        }
    }
}
