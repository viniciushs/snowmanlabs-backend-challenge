using System;

namespace SnowmanLabsChallenge.Domain.Models
{
    /// <summary>
    ///     Entidade Favorite.
    /// </summary>
    public class Favorite : BaseEntity
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Favorite"/> class.
        ///     Contrutor vazio utilizado pelo EntityFramework.
        /// </summary>
        public Favorite()
            : base()
        {
        }

        public Favorite(
            int id,
            Guid uuid,
            DateTime createdOn,
            bool active,
            Guid userId,
            int touristSpotId)
            : base(id, uuid, createdOn, active)
        {
            #region Validations

            if (touristSpotId < 1)
            {
                throw new SnowmanLabsChallengeException("Invalid tourist spot.");
            }

            #endregion Validations

            this.UserId = userId;
            this.TouristSpotId = touristSpotId;
        }

        public Guid UserId { get; private set; }

        public int TouristSpotId { get; private set; }

        public TouristSpot TouristSpot { get; set; }
    }
}
