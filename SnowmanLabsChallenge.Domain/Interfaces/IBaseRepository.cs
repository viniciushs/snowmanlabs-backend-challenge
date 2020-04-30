namespace SnowmanLabsChallenge.Domain.Interfaces
{
    using SnowmanLabsChallenge.Domain.Enumerators;
    using SnowmanLabsChallenge.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    ///     Generic interface contract for most entities.
    ///     Interface de contrato genérica para a maioria das entidades.
    /// </summary>
    public interface IBaseRepository<TEntity> : IDisposable
        where TEntity : BaseEntity
    {
        /// <summary>
        ///     Adds a new entity.
        ///     Adiciona uma entidade nova.
        /// </summary>
        /// <param name="obj">
        ///     The new entity;
        ///     Entidade a ser adicionada.
        /// </param>
        TEntity Add(TEntity model);

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
        TEntity GetById(int id);

        /// <summary>
        ///     Gets all registers.
        ///     Obtém todas os registros cadastrados.
        /// </summary>
        IQueryable<TEntity> GetAll();

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
        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> expression, bool noTracking = false);

        /// <summary>
        ///     Gets the entities that match the rule proposed in the parameter.
        ///     Obtém as entidades que condizem com a regra proposta no parâmetro.
        /// </summary>
        /// <param name="query">
        ///     The query formed, containing or not joins.
        ///     A consulta formada, contendo ou não joins.
        /// </param>
        /// <param name="predicate">
        ///     The conditions (where) to find the entities.
        ///     As condições (where) para encontrar as entidades.
        /// </param>
        /// <example>
        ///     _repository.Get(GetAll(), r => r.Name.StartsWith("Test"));
        ///     _repository.Get(GetAll(), r => r.Id == id);
        /// </example>
        /// <returns>
        ///     All entities that comply with the informed rule.
        ///     Todas as entidades que condizem com a regra informada.
        /// </returns>
        IQueryable<TEntity> GetBy(IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Gets the entities that match the rule proposed in the parameter.
        ///     Obtém as entidades que condizem com a regra proposta no parâmetro.
        /// </summary>
        /// <param name="predicate">
        ///     Rule inserted to find the entities.
        ///     Regra inserida para encontrar as entidades.
        /// </param>
        /// <param name="includes">
        ///     The includes (joins) to be included.
        ///     Os includes (joins) a serem incluídos.
        /// </param>
        /// <example>
        ///     _repository.Get(r => r.Name.StartsWith("Test"), r => r.Address, r => r.Orders.Select(o => o.OrderItems));
        ///     _repository.Get(r => r.Id == id, r => r.Country); // Join on Contry
        /// </example>
        /// <seealso cref="https://stackoverflow.com/questions/5376421/ef-including-other-entities-generic-repository-pattern" />
        /// <returns>
        ///     All entities that comply with the informed rule.
        ///     Todas as entidades que condizem com a regra informada.
        /// </returns>
        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

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
        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate, params string[] includes);

        /// <summary>
        ///     Gets all paginated registers.
        ///     Obtém todas os registros cadastrados por paginação.
        /// </summary>
        IQueryable<TEntity> GetByPaged(
            IQueryable<TEntity> query,
            Func<TEntity, object> orderBy,
            OrderByDirectionEnum orderByDirection,
            IPager pager);

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
        IQueryable<TEntity> GetByOrdered<TOrderKey>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TOrderKey>> orderByExp,
            bool ascending,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Updates the record with the information passed in the parameter.
        ///     Atualiza o registro com as informações passadas no parâmetro.
        /// </summary>
        /// <param name="obj">
        ///     Entity containing the updated information.
        ///     Entidade contendo as informações atualizadas.
        /// </param>
        void Update(TEntity obj);

        /// <summary>
        ///     Removes the record whose ID is passed in the parameter.
        ///     Remove o registro cujo o ID é o passado no parâmetro.
        /// </summary>
        /// <param name="id">
        ///     Identifier of the record in the entity table.
        ///     Identificador do registro na tabela da entidade.
        /// </param>
        void Remove(int id);

        /// <summary>
        ///     Removes the record
        ///     Remove o registro
        /// </summary>
        void Remove(TEntity obj);

        /// <summary>
        ///     Removes the records whose IDs is passed in the parameter.
        ///     Remove os registros cujo IDs são passados no parâmetro.
        /// </summary>
        void Remove(IEnumerable<int> ids);

        /// <summary>
        ///     Removes the list of records
        ///     Remove a lista de registros
        /// </summary>
        void Remove(IEnumerable<TEntity> objs);

        /// <summary>
        ///     Counts the number of expression items.
        ///     Contador da expressão
        /// </summary>
        int Count(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        ///     Saves the changes made.
        ///     Salva as modificações realizadas.
        /// </summary>
        int SaveChanges();
    }
}
