namespace SnowmanLabsChallenge.Application.Services
{
    using global::AutoMapper;
    using global::AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using SnowmanLabsChallenge.Application.Filters;
    using SnowmanLabsChallenge.Application.Interfaces;
    using SnowmanLabsChallenge.Application.Pagers;
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Enumerators;
    using SnowmanLabsChallenge.Domain.Interfaces;
    using SnowmanLabsChallenge.Domain.Models;
    using SnowmanLabsChallenge.Infra.CrossCutting.Core.Messages;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using SnowmanLabsChallenge.Infra.CrossCutting.Utils.Builders;

    public abstract class BaseAppService<TViewModel, TFilter, TEntity> : IBaseAppService<TViewModel, TFilter, TEntity>
        where TViewModel : BaseViewModel
        where TFilter : BaseFilter
        where TEntity : BaseEntity
    {
        protected readonly IMapper mapper;
        protected readonly IUnitOfWork uow;
        protected readonly IBaseRepository<TEntity> repository;

        public BaseAppService(
            IUnitOfWork uow,
            IMapper mapper,
            IBaseRepository<TEntity> repository)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.repository = repository;
        }

        /// <summary>
        ///     Obtém todos os registros.
        /// </summary>
        /// <returns>
        ///     Todos os registros do banco de dados.
        /// </returns>
        public virtual IEnumerable<TViewModel> GetAll()
        {
            var results = this.repository.GetAll();
            return results.ProjectTo<TViewModel>(this.mapper.ConfigurationProvider);
        }

        /// <summary>
        ///     Obtém o registro cujo ID é o passado como parâmetro.
        /// </summary>
        public virtual TViewModel GetById(int id)
        {
            var result = this.repository.GetBy(c => c.Id == id).FirstOrDefault();
            return this.mapper.Map<TViewModel>(result);
        }

        /// <summary>
        ///     Obtém o registro cujo GuidID é o passado como parâmetro.
        /// </summary>
        public virtual TViewModel GetByGuid(Guid guid)
        {
            var result = this.repository.GetBy(c => c.Uuid == guid).FirstOrDefault();
            return this.mapper.Map<TViewModel>(result);
        }

        /// <summary>
        ///     Obtém os registros utilizando o filtro utilizado no parâmetro.
        /// </summary>
        public virtual ResponseViewModel GetBy(TFilter filter, params Expression<Func<TEntity, object>>[] includes)
        {
            var expression = this.Filter(filter);
            var orderBy = this.OrderBy(filter);

            var orderByDirection = OrderByDirectionEnum.Ascending;
            if (filter.SortDirection.ToLowerCase() == "desc")
            {
                orderByDirection = OrderByDirectionEnum.Descending;
            }

            #region Pager

            var pager = new Pager()
            {
                PageNumber = filter.PageNumber ?? 0,
                PageSize = filter.PageSize ?? 25,
                HasPagination = filter.HasPagination
            };

            #endregion Pager

            var query = this.repository.GetBy(expression, includes);
            var results = this.repository.GetByPaged(query, orderBy, orderByDirection, pager).ToList();

            var totalItems = this.repository.Count(expression);
            var totalPages = 1;
            if (filter.HasPagination)
            {
                if (filter.PageSize.HasValue)
                {
                    var ceilingResult = Math.Ceiling((decimal)totalItems / (decimal)filter.PageSize.Value);
                    totalPages = int.Parse(ceilingResult.ToString());
                }
            }

            var response = new ResponseViewModel
            {
                Data = this.mapper.Map<IEnumerable<TViewModel>>(results),
                Page = new PageViewModel
                {
                    PageNumber = filter.HasPagination ? (filter.PageNumber ?? 1) : 1,
                    Size = filter.HasPagination ? (filter.PageSize ?? totalItems) : totalItems,
                    TotalElements = totalItems,
                    TotalPages = totalPages,
                    SortBy = filter.SortBy,
                    SortDirection = string.IsNullOrEmpty(filter.SortDirection) ? "asc" : filter.SortDirection
                },
                Success = true
            };

            return response;
        }

        /// <summary>
        ///     Obtém os registros utilizando a expressão utilizada no parâmetro.
        /// </summary>
        public virtual IEnumerable<TViewModel> GetBy(Expression<Func<TEntity, bool>> expression)
        {
            var results = this.repository.GetBy(expression).AsNoTracking().ToList();
            return this.mapper.Map<IEnumerable<TViewModel>>(results);
        }

        /// <summary>
        ///     Obtém os registros utilizando a expressão utilizada no parâmetro.
        /// </summary>
        public virtual IEnumerable<TViewModel> GetBy(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            var results = this.repository.GetBy(expression, includes).ToList();
            return this.mapper.Map<IEnumerable<TViewModel>>(results);
        }

        /// <summary>
        ///     Salva o registro passado como parâmetro.
        /// </summary>
        public virtual TViewModel Add(TViewModel model, bool commit = true)
        {
            this.Validate(model);

            var entity = this.mapper.Map<TEntity>(model);
            this.Validate(entity);

            this.repository.Add(entity);
            this.Commit(commit);

            this.mapper.Map<TEntity, TViewModel>(entity, model);

            return model;
        }

        /// <summary>
        ///     Atualiza o registro passado como parâmetro.
        /// </summary>
        public virtual void Update(TViewModel model, bool commit = true)
        {
            this.Validate(model);

            if (model.Id == 0)
            {
                throw new SnowmanLabsChallengeException(Messages.NotFound);
            }

            var entity = this.mapper.Map<TEntity>(model);
            this.Validate(entity);

            this.repository.Update(entity);
            this.Commit(commit);
        }

        /// <summary>
        ///     Remove o registro que possui o identificador passado no parâmetro.
        /// </summary>
        public virtual void Remove(int id, bool commit = true)
        {
            var entity = this.repository.GetById(id);
            if (entity == null)
            {
                throw new SnowmanLabsChallengeException(Messages.NotFound);
            }

            this.repository.Remove(id);
            this.Commit(commit);
        }

        /// <summary>
        ///     Remove a lista de registros possuem o identificador passado no parâmetro.
        /// </summary>
        public virtual void Remove(IEnumerable<int> ids, bool commit = true)
        {
            if (ids != null && ids.Any())
            {
                foreach (var id in ids)
                {
                    this.Remove(id, false);
                }
            }

            this.Commit(commit);
        }

        /// <summary>
        /// Definição dos filtros realizadas na consulta no Database
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual Expression<Func<TEntity, bool>> Filter(TFilter filter)
        {
            var expression = PredicateBuilder.True<TEntity>();

            if (filter != null)
            {
                if (filter.Id.HasValue)
                {
                    expression = expression.And(f => f.Id == filter.Id.Value);
                }

                if (filter.Uuid.HasValue)
                {
                    expression = expression.And(f => f.Uuid == filter.Uuid.Value);
                }

                if (filter.CreatedOn.HasValue)
                {
                    expression = expression.And(f => f.CreatedOn == filter.CreatedOn.Value);
                }

                if (filter.Active.HasValue)
                {
                    expression = expression.And(f => f.Active == filter.Active.Value);
                }
            }

            return expression;
        }

        /// <summary>
        /// Definição dos campos de ordenação realizadas no Database.
        /// </summary>
        public virtual Func<TEntity, object> OrderBy(TFilter filter)
        {
            Func<TEntity, object> orderBy = (filter.SortBy.ToLower()) switch
            {
                "uuid" => (x => x.Uuid),
                "active" => (x => x.Active),
                "createdOn" => (x => x.CreatedOn),
                _ => (x => x.Id),
            };
            return orderBy;
        }

        /// <summary>
        ///     Validações dos valores recebidos pelo ViewModel.
        /// </summary>
        public virtual void Validate(TViewModel model)
        {
            if (model == null)
            {
                throw new SnowmanLabsChallengeException(Messages.EmptyData);
            }
        }

        /// <summary>
        ///     Validações do model gerado a partir do ViewModel recebido.
        /// </summary>
        public virtual void Validate(TEntity model)
        {
            if (model == null)
            {
                throw new SnowmanLabsChallengeException(Messages.EmptyData);
            }
        }

        public virtual void Commit(bool commit)
        {
            if (commit)
            {
                this.uow.Commit();
            }
        }

        /// <summary>
        ///     Desaloca os recursos de Cidade Application Service usando o Garbage Collector.
        /// </summary>
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
