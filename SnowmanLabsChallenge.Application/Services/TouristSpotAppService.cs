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
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    ///     Implementação da <see cref="ITouristSpotAppService"/>.
    /// </summary>
    public class TouristSpotAppService : BaseAppService<TouristSpotViewModel, TouristSpotFilter, TouristSpot>, ITouristSpotAppService
    {
        private readonly IPictureAppService pictureAppService;

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
            ITouristSpotRepository repository,
            IPictureAppService pictureAppService)
            : base(uow, mapper, repository)
        {
            this.pictureAppService = pictureAppService;
        }

        public override TouristSpotViewModel Add(TouristSpotViewModel model, bool commit = true)
        {
            this.Validate(model);

            var entity = this.mapper.Map<TouristSpot>(model);
            this.Validate(entity);

            this.repository.Add(entity);
            this.Commit(commit);

            var pictures = new List<PictureViewModel>();
            foreach(var picture in model.Pictures)
            {
                picture.TouristSpotId = entity.Id;
                var pictureModel = this.pictureAppService.Add(picture);
                pictures.Add(pictureModel);
            }

            this.mapper.Map<TouristSpot, TouristSpotViewModel>(entity, model);
            model.Pictures = pictures;

            return model;
        }

        public override void Remove(int id, bool commit = true)
        {
            var entity = this.repository.GetBy(ts => ts.Id == id, ts => ts.Pictures).FirstOrDefault();
            if (entity == null)
            {
                throw new SnowmanLabsChallengeException(Messages.NotFound);
            }

            var pictureIds = entity.Pictures.Select(p => p.Id).ToList();
            this.pictureAppService.Remove(pictureIds, false);

            this.repository.Remove(id);
            this.Commit(commit);
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
