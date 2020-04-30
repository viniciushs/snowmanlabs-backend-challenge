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
    ///     Implementação da <see cref="ICategoryAppService"/>.
    /// </summary>
    public class CategoryAppService : BaseAppService<CategoryViewModel, CategoryFilter, Category>, ICategoryAppService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryAppService"/> class.
        ///     Construtor padrão de <see cref="CategoryAppService"/>.
        /// </summary>
        /// <param name="uow">
        ///     Contrato do Unit of Work. Veja <see cref="IUnitOfWork"/>.
        /// </param>
        /// <param name="mapper">
        ///     Contrato do AutoMapper. Veja <see cref="IMapper"/>.
        /// </param>
        /// <param name="repository">
        ///     O repositório da entidade Category. Veja <see cref="ICategoryRepository"/>.
        /// </param>

        public CategoryAppService(
            IUnitOfWork uow,
            IMapper mapper,
            ICategoryRepository repository)
            : base(uow, mapper, repository)
        {
        }

        public override Expression<Func<Category, bool>> Filter(CategoryFilter filter)
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

        public override Func<Category, object> OrderBy(CategoryFilter filter)
        {
            Func<Category, object> orderBy;

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
