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

    /// <summary>
    ///     Implementação da <see cref="IPictureAppService"/>.
    /// </summary>
    public class PictureAppService : BaseAppService<PictureViewModel, PictureFilter, Picture>, IPictureAppService
    {
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
            IPictureRepository repository)
            : base(uow, mapper, repository)
        {
        }

        public override Expression<Func<Picture, bool>> Filter(PictureFilter filter)
        {
            var expression = base.Filter(filter);

            if (filter != null)
            {
                // Adicione outros filtros de busca
                // if (!string.IsNullOrEmpty(filter.Codigo))
                // {
                //     expression = expression.And(f => f.Codigo.ToLowerCase().Contains(filter.Codigo.ToLowerCase()));
                // }
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
