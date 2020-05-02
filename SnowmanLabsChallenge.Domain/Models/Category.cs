using System;
using System.Collections.Generic;

namespace SnowmanLabsChallenge.Domain.Models
{
    /// <summary>
    ///     Entidade Category.
    /// </summary>
    public class Category : BaseEntity
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Category"/> class.
        ///     Contrutor vazio utilizado pelo EntityFramework.
        /// </summary>
        public Category()
            : base()
        {
        }

        public Category(
            int id,
            Guid uuid,
            DateTime createdOn,
            bool active,
            string name)
            : base(id, uuid, createdOn, active)
        {
            #region Validations

            if (string.IsNullOrEmpty(name))
            {
                throw new SnowmanLabsChallengeException("The category name is required.");
            }

            name = name.Trim();
            if (name.Length == 0)
            {
                throw new SnowmanLabsChallengeException("The category name can not be empty.");
            }

            if (name.Length > 255)
            {
                throw new SnowmanLabsChallengeException("The category name can not have more than 255 characters.");
            }

            #endregion Validations

            this.Name = name;
        }

        public string Name { get; private set; }

        public ICollection<TouristSpot> TouristSpots { get; set; }
    }
}
