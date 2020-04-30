namespace SnowmanLabsChallenge.Infra.Data.Repository
{
    using Microsoft.EntityFrameworkCore;
    using SnowmanLabsChallenge.Domain.Enumerators;
    using SnowmanLabsChallenge.Domain.Interfaces;
    using SnowmanLabsChallenge.Domain.Models;
    using SnowmanLabsChallenge.Infra.Data.Context;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    ///     Implementation of generic interface for the most entities.
    ///     Implementação da interface genérica para a maioria das entidades.
    /// </summary>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly DefaultContext Db;
        protected readonly DbSet<TEntity> DbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{TEntity}"/> class.
        ///     A default contructor.
        ///     Contrutor padrão.
        /// </summary>
        /// <param name="context">
        ///     The context to be used.
        ///     O contexto a ser utilizado.
        /// </param>
        public BaseRepository(DefaultContext context)
        {
            this.Db = context;
            this.DbSet = this.Db.Set<TEntity>();
        }

        /// <summary>
        ///     Adds a new entity.
        ///     Adiciona uma entidade nova.
        /// </summary>
        /// <param name="obj">
        ///     The new entity;
        ///     Entidade a ser adicionada.
        /// </param>
        public virtual TEntity Add(TEntity entity)
        {
            this.DbSet.Add(entity);
            return entity;
        }

        /// <summary>
        ///     Gets the entity by ID passed by parameter.
        ///     Obtém a entidade cujo ID é o passado como parâmetro.
        /// </summary>
        /// <param name="id">
        ///     Entity's identificator.
        ///     Identificador da entidade.
        /// </param>
        /// <returns>
        ///     The entity's informations.
        ///     As informações da entidade.
        /// </returns>
        public virtual TEntity GetById(int id)
        {
            return this.DbSet.Find(id);
        }

        /// <summary>
        ///     Gets all registers.
        ///     Obtém todas os registros cadastrados.
        /// </summary>
        public virtual IQueryable<TEntity> GetAll()
        {
            return this.DbSet;
        }

        /// <summary>
        ///     Gets the entities that match the rule proposed in the parameter.
        ///     Obtém as entidades que condizem com a regra proposta no parâmetro.
        /// </summary>
        /// <param name="expression">
        ///     Rule inserted (where) to find the entities.
        ///     Regra inserida (where) para encontrar as entidades.
        /// </param>
        /// <example>
        ///     _repository.Get(r => r.Name.StartsWith("Test"));
        ///     _repository.Get(r => r.Id == id);
        /// </example>
        /// <returns>
        ///     All entities that comply with the informed rule.
        ///     Todas as entidades que condizem com a regra informada.
        /// </returns>
        public virtual IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> expression, bool noTracking = false)
        {
            var query = this.GetAll().Where(expression).AsQueryable();
            return noTracking ? query.AsNoTracking() : query;
        }

        /// <summary>
        ///     Gets the entities that match the rule proposed in the parameter.
        ///     Obtém as entidades que condizem com a regra proposta no parâmetro.
        /// </summary>
        /// <param name="query">
        ///     The query formed, containing or not joins.
        ///     A consulta formada, contendo ou não joins.
        /// </param>
        /// <param name="predicate">
        ///     Rule inserted (where) to find the entities.
        ///     Regra inserida (where) para encontrar as entidades.
        /// </param>
        /// <example>
        ///     _repository.Get(GetAll(), r => r.Name.StartsWith("Test"));
        ///     _repository.Get(GetAll(), r => r.Id == id);
        /// </example>
        /// <returns>
        ///     All entities that comply with the informed rule.
        ///     Todas as entidades que condizem com a regra informada.
        /// </returns>
        public virtual IQueryable<TEntity> GetBy(IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate)
        {
            return query.Where(predicate).AsQueryable();
        }

        /// <summary>
        ///     Gets the entities that match the rule proposed in the parameter.
        ///     Obtém as entidades que condizem com a regra proposta no parâmetro.
        /// </summary>
        /// <param name="predicate">
        ///     Rule inserted (where) to find the entities.
        ///     Regra inserida (where) para encontrar as entidades.
        /// </param>
        /// <param name="includes">
        ///     The includes (joins) to be included.
        ///     Os includes (joins) a serem incluídos.
        /// </param>
        /// <example>
        ///     _repository.Get(r => r.Name.StartsWith("Test"), r => r.Address, r => r.Orders.Select(o => o.OrderItems));
        ///     _repository.Get(r => r.Id == id, r => r.Country); // Join on Contry
        /// </example>
        /// <seealso cref="https://stackoverflow.com/questions/5376421/ef-including-other-entities-generic-repository-pattern"/>
        /// <returns>
        ///     All entities that comply with the informed rule.
        ///     Todas as entidades que condizem com a regra informada.
        /// </returns>
        public virtual IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = this.GetAll();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            if (predicate != null)
            {
                query = this.GetBy(query, predicate);
            }

            return query;
        }

        /// <summary>
        ///     Gets the entities that match the rule proposed in the parameter.
        ///     Obtém as entidades que condizem com a regra proposta no parâmetro.
        /// </summary>
        /// <param name="predicate">
        ///     Rule inserted (where) to find the entities.
        ///     Regra inserida (where) para encontrar as entidades.
        /// </param>
        /// <param name="includes">
        ///     The includes (joins) to be included.
        ///     Os includes (joins) a serem incluídos.
        /// </param>
        /// <example>
        ///     _repository.Get(r => r.Name.StartsWith("Test"), "Address", "Orders.OrderItems");
        ///     _repository.Get(r => r.Id == id, "Country"); // Join on Contry
        /// </example>
        /// <returns>
        ///     All entities that comply with the informed rule.
        ///     Todas as entidades que condizem com a regra informada.
        /// </returns>
        public virtual IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            var query = this.GetAll();

            if (includes != null && includes.Any())
            {
                foreach (var include in includes)
                {
                    if (!string.IsNullOrEmpty(include))
                    {
                        query = query.Include(include);
                    }
                }
            }

            if (predicate != null)
            {
                query = this.GetBy(query, predicate);
            }

            return query;
        }

        /// <summary>
        ///     Gets all paginated registers.
        ///     Obtém todas os registros cadastrados por paginação.
        /// </summary>
        public virtual IQueryable<TEntity> GetByPaged(
            IQueryable<TEntity> query,
            Func<TEntity, object> orderBy,
            OrderByDirectionEnum orderByDirection,
            IPager pager)
        {
            if (orderByDirection == OrderByDirectionEnum.Ascending)
            {
                query = query.OrderBy(orderBy).AsQueryable();
            }
            else if (orderByDirection == OrderByDirectionEnum.Descending)
            {
                query = query.OrderByDescending(orderBy).AsQueryable();
            }

            if (pager.HasPagination)
            {
                query = query.Skip((pager.PageNumber - 1) * pager.PageSize)
                             .Take(pager.PageSize)
                             .AsQueryable();
            }

            return query;
        }

        /// <summary>
        ///     Gets the entities that match the rule proposed in the parameter and do the order by.
        ///     Obtém as entidades que condizem com a regra proposta no parâmetro e realiza a ordenação.
        /// </summary>
        /// <param name="predicate">
        ///     Rule inserted (where) to find the entities.
        ///     Regra inserida (where) para encontrar as entidades.
        /// </param>
        /// <param name="orderByExp">
        ///     The order expression.
        ///     A expressão de ordenaçâo.
        /// </param>
        /// <param name="ascending">
        ///     Indicator whether the sort must be ascending or descending.
        ///     Indicador se a ordenação deve ser ascendente ou descendente.
        /// </param>
        /// <param name="includes">
        ///     The includes (joins) to be included.
        ///     Os includes (joins) a serem incluídos.
        /// </param>
        /// <returns>
        ///     All entities that comply with the informed rule.
        ///     Todas as entidades que condizem com a regra informada.
        /// </returns>
        public virtual IQueryable<TEntity> GetByOrdered<TOrderKey>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TOrderKey>> orderByExp,
            bool ascending,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = this.GetBy(predicate, includes);

            query = ascending ? query.OrderBy(orderByExp) : query.OrderByDescending(orderByExp);

            return query;
        }

        /// <summary>
        ///     Updates the record with the information passed in the parameter.
        ///     Atualiza o registro com as informações passadas no parâmetro.
        /// </summary>
        /// <param name="obj">
        ///     Entity containing the updated information.
        ///     Entidade contendo as informações atualizadas.
        /// </param>
        public virtual void Update(TEntity obj)
        {
            this.DbSet.Update(obj);
        }

        /// <summary>
        ///     Removes the record whose ID is passed in the parameter.
        ///     Remove o registro cujo o ID é o passado no parâmetro.
        /// </summary>
        /// <param name="id">
        ///     Identifier of the record in the entity table.
        ///     Identificador do registro na tabela da entidade.
        /// </param>
        public virtual void Remove(int id)
        {
            this.DbSet.Remove(this.DbSet.Find(id));
        }

        /// <summary>
        ///     Removes the records whose IDs is passed in the parameter.
        ///     Remove os registros cujo IDs são passados no parâmetro.
        /// </summary>
        public virtual void Remove(IEnumerable<int> ids)
        {
            foreach(var id in ids)
            {
                this.Remove(id);
            }
        }

        /// <summary>
        ///     Removes the record
        ///     Remove o registro
        /// </summary>
        public virtual void Remove(TEntity obj)
        {
            if (obj != null)
            {
                this.Remove(obj.Id);
            }
        }

        /// <summary>
        ///     Removes the list of records
        ///     Remove a lista de registros
        /// </summary>
        public virtual void Remove(IEnumerable<TEntity> objs)
        {
            var ids = objs.Select(obj => obj.Id).Distinct().ToList();
            this.Remove(ids);
        }

        /// <summary>
        ///     Counts the number of expression items.
        ///     Contador da expressão
        /// </summary>
        public int Count(Expression<Func<TEntity, bool>> expression)
        {
            return this.DbSet.Count(expression);
        }

        /// <summary>
        ///     Saves the changes made.
        ///     Salva as modificações realizadas.
        /// </summary>
        public int SaveChanges()
        {
            return this.Db.SaveChanges();
        }

        /// <summary>
        ///     Releases the allocated resources.
        ///     Libera os recursos alocados.
        /// </summary>
        public void Dispose()
        {
            this.Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
