namespace SnowmanLabsChallenge.Application.Services
{
    using global::AutoMapper;
    using NetTopologySuite.Geometries;
    using SnowmanLabsChallenge.Application.Filters;
    using SnowmanLabsChallenge.Application.Interfaces;
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Interfaces;
    using SnowmanLabsChallenge.Domain.Models;
    using SnowmanLabsChallenge.Infra.CrossCutting.Core.Messages;
    using SnowmanLabsChallenge.Infra.CrossCutting.Utils.Builders;
    using System;
    using System.Linq.Expressions;

    /// <summary>
    ///     Implementação da <see cref="ITouristSpotAppService"/>.
    /// </summary>
    public class TouristSpotAppService : BaseAppService<TouristSpotViewModel, TouristSpotFilter, TouristSpot>, ITouristSpotAppService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TouristSpotAppService"/> class.
        ///     Construtor padrão de <see cref="TouristSpotAppService"/>.
        /// </summary>
        /// <param name="uow">
        ///     Contrato do Unit of Work. Veja <see cref="IUnitOfWork"/>.
        /// </param>
        /// <param name="mapper">
        ///     Contrato do AutoMapper. Veja <see cref="IMapper"/>.
        /// </param>
        /// <param name="repository">
        ///     O repositório da entidade TouristSpot. Veja <see cref="ITouristSpotRepository"/>.
        /// </param>

        public TouristSpotAppService(
            IUnitOfWork uow,
            IMapper mapper,
            ITouristSpotRepository repository)
            : base(uow, mapper, repository)
        {
        }

        public override Expression<Func<TouristSpot, bool>> Filter(TouristSpotFilter filter)
        {
            var expression = base.Filter(filter);

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    expression = expression.And(f => f.Name.Contains(filter.Name));
                }

                if (filter.RadiusMeters.HasValue && filter.Latitude.HasValue && filter.Longitude.HasValue)
                {
                    var searchLocationPoint = new Point(filter.Longitude.Value, filter.Latitude.Value) { SRID = 4326 };
                    expression = expression.And(f => f.Location.Distance(searchLocationPoint) <= filter.RadiusMeters);
                }
            }

            return expression;
        }

        public override Func<TouristSpot, object> OrderBy(TouristSpotFilter filter)
        {
            Func<TouristSpot, object> orderBy;

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
