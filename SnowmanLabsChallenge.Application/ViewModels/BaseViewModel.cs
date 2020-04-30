namespace SnowmanLabsChallenge.Application.ViewModels
{
    using System;

    public abstract class BaseViewModel
    {
        public BaseViewModel()
        {
        }

        public BaseViewModel(int id, Guid uuid, DateTime createdOn)
        {
            this.Id = id;
            this.Uuid = uuid;
            this.CreatedOn = createdOn;
        }

        /// <summary>
        ///     A identidade INT do registro.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     A register's Universal Unique Identifier (UUID).
        /// </summary>
        public Guid Uuid { get; set; }

        /// <summary>
        ///     Indica a data de criação do registro.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        ///     Indica se o registro está ativo.
        /// </summary>
        public bool Active { get; set; }
    }
}
