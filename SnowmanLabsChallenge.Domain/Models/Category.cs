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

        public string Name { get; set; }

        public ICollection<TouristSpot> TouristSpots { get; set; }
    }
}
