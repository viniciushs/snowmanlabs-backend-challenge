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
    using System.Linq;
    using SnowmanLabsChallenge.Infra.CrossCutting.Core.Messages;

    /// <summary>
    ///     Implementação da <see cref="ICategoryAppService"/>.
    /// </summary>
    public class CategoryAppService : BaseAppService<CategoryViewModel, CategoryFilter, Category>, ICategoryAppService
    {
        private readonly ITouristSpotRepository touristSpotRepository;

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
            ICategoryRepository repository,
            ITouristSpotRepository touristSpotRepository)
            : base(uow, mapper, repository)
        {
            this.touristSpotRepository = touristSpotRepository;
        }

        public override Expression<Func<Category, bool>> Filter(CategoryFilter filter)
        {
            var expression = base.Filter(filter);

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    expression = expression.And(f => f.Name.Contains(filter.Name));
                }
            }

            return expression;
        }

        public override void Remove(int id, bool commit = true)
        {
            var entity = this.repository.GetBy(c => c.Id == id, c => c.TouristSpots).FirstOrDefault();
            if (entity == null)
            {
                throw new SnowmanLabsChallengeException(Messages.NotFound);
            }

            if (entity.TouristSpots.Any())
            {
                throw new SnowmanLabsChallengeException("You can not delete this category because there are tourist spots using it.");
            }

            this.repository.Remove(id);
            this.Commit(commit);
        }

        public override void Validate(Category model)
        {
            base.Validate(model);

            var category = this.repository.GetBy(c => c.Id != model.Id && c.Name == model.Name).FirstOrDefault();
            if (category != null)
            {
                throw new SnowmanLabsChallengeException("Already exists a category with that name.");
            }
        }

        public override Func<Category, object> OrderBy(CategoryFilter filter)
        {
            Func<Category, object> orderBy = (filter.SortBy.ToLower()) switch
            {
                "name" => (x => x.Name),
                _ => base.OrderBy(filter),
            };
            return orderBy;
        }
    }
}
