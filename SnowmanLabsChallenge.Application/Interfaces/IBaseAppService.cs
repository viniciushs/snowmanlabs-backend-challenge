namespace SnowmanLabsChallenge.Application.Interfaces
{
    using SnowmanLabsChallenge.Application.Filters;
    using SnowmanLabsChallenge.Application.ViewModels;
    using SnowmanLabsChallenge.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IBaseAppService<TViewModel, TFilter, TEntity> : IDisposable
        where TViewModel : BaseViewModel
        where TFilter : BaseFilter
        where TEntity : BaseEntity
    {
        /// <summary>
        ///     Obtém todos os registros.
        /// </summary>
        /// <returns>
        ///     Todos os registros do banco de dados.
        /// </returns>
        IEnumerable<TViewModel> GetAll();

        /// <summary>
        ///     Obtém o registro cujo ID é o passado como parâmetro.
        /// </summary>
        TViewModel GetById(int id);

        /// <summary>
        ///     Obtém o registro cujo GuidID é o passado como parâmetro.
        /// </summary>
        TViewModel GetByGuid(Guid guid);

        /// <summary>
        ///     Obtém os registros utilizando o filtro utilizado no parâmetro.
        /// </summary>
        ResponseViewModel GetBy(TFilter filter, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Obtém os registros utilizando a expressão utilizada no parâmetro.
        /// </summary>
        IEnumerable<TViewModel> GetBy(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        ///     Obtém os registros utilizando a expressão utilizada no parâmetro.
        /// </summary>
        IEnumerable<TViewModel> GetBy(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Salva o registro passado como parâmetro.
        /// </summary>
        TViewModel Add(TViewModel model, bool commit = true);

        /// <summary>
        ///     Atualiza o registro passado como parâmetro.
        /// </summary>
        void Update(TViewModel model, bool commit = true);

        /// <summary>
        ///     Remove o registro que possui o identificador passado no parâmetro.
        /// </summary>
        void Remove(int id, bool commit = true);

        /// <summary>
        ///     Remove a lista de registros possuem o identificador passado no parâmetro.
        /// </summary>
        void Remove(IEnumerable<int> ids, bool commit = true);

        void Validate(TViewModel model);

        void Validate(TEntity model);

        void Commit(bool commit);
    }
}
