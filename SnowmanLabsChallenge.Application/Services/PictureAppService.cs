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
    using SnowmanLabsChallenge.Infra.CrossCutting.Core.Messages;
    using SnowmanLabsChallenge.Infra.CrossCutting.Core.Interfaces;
    using SnowmanLabsChallenge.Infra.CrossCutting.Utils.Builders;
    using System.Configuration;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    ///     Implementação da <see cref="IPictureAppService"/>.
    /// </summary>
    public class PictureAppService : BaseAppService<PictureViewModel, PictureFilter, Picture>, IPictureAppService
    {
        private readonly IFileService fileService;
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="PictureAppService"/> class.
        ///     Construtor padrão de <see cref="PictureAppService"/>.
        /// </summary>
        /// <param name="uow">
        ///     Contrato do Unit of Work. Veja <see cref="IUnitOfWork"/>.
        /// </param>
        /// <param name="mapper">
        ///     Contrato do AutoMapper. Veja <see cref="IMapper"/>.
        /// </param>
        /// <param name="repository">
        ///     O repositório da entidade Picture. Veja <see cref="IPictureRepository"/>.
        /// </param>

        public PictureAppService(
            IUnitOfWork uow,
            IMapper mapper,
            IPictureRepository repository,
            IFileService fileService,
            IConfiguration configuration)
            : base(uow, mapper, repository)
        {
            this.fileService = fileService;
            this.configuration = configuration;
        }

        public override PictureViewModel Add(PictureViewModel model, bool commit = true)
        {
            this.Validate(model);

            if (string.IsNullOrEmpty(model.Url))
            {
                model.Url = this.configuration["AzureBlobUrl"] + Guid.NewGuid().ToString() + ".png";
                this.fileService.Upload(model.Base64, model.Url);
            }

            var entity = this.mapper.Map<Picture>(model);
            this.Validate(entity);

            this.repository.Add(entity);
            this.Commit(commit);

            this.mapper.Map<Picture, PictureViewModel>(entity, model);

            return model;
        }

        public void Remove(int id, Guid requesterId, bool commit = true)
        {
            var entity = this.repository.GetById(id);
            if (entity == null)
            {
                throw new SnowmanLabsChallengeException(Messages.NotFound);
            }

            if (entity.OwnerId != requesterId)
            {
                throw new SnowmanLabsChallengeException("You cannot delete other's pictures.");
            }

            base.Remove(id, commit);
        }

        public override void Remove(int id, bool commit = true)
        {
            var entity = this.repository.GetById(id);
            if (entity == null)
            {
                throw new SnowmanLabsChallengeException(Messages.NotFound);
            }

            if (entity.Url.Contains(this.configuration["AzureBlobUrl"]))
            {
                this.fileService.Remove(entity.Url);
            }

            this.repository.Remove(id);
            this.Commit(commit);
        }

        public override void Validate(PictureViewModel model)
        {
            base.Validate(model);

            if (string.IsNullOrEmpty(model.Url) && string.IsNullOrEmpty(model.Base64))
            {
                throw new SnowmanLabsChallengeException("The url and base64 are empty.");
            }
        }

        public override Expression<Func<Picture, bool>> Filter(PictureFilter filter)
        {
            var expression = base.Filter(filter);

            if (filter != null)
            {
                if (filter.TouristSpotId.HasValue)
                {
                    expression = expression.And(f => f.TouristSpotId == filter.TouristSpotId.Value);
                }
            }

            return expression;
        }

        public override Func<Picture, object> OrderBy(PictureFilter filter)
        {
            Func<Picture, object> orderBy;

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
