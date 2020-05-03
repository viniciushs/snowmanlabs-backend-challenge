using System;

namespace SnowmanLabsChallenge.Domain.Models
{
    /// <summary>
    ///     Entidade Vote.
    /// </summary>
    public class Vote : BaseEntity
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Vote"/> class.
        ///     Contrutor vazio utilizado pelo EntityFramework.
        /// </summary>
        public Vote()
            : base()
        {
        }

        public Vote(
            int id,
            Guid uuid,
            DateTime createdOn,
            bool active,
            Guid userId,
            int touristSpotId,
            bool up,
            bool down)
            : base(id, uuid, createdOn, active)
        {
            #region Validations

            if (up && down)
            {
                throw new SnowmanLabsChallengeException("Can not vote up and down at the same time.");
            }

            if (!up && !down)
            {
                throw new SnowmanLabsChallengeException("Need to vote up or down.");
            }

            if (touristSpotId < 1)
            {
                throw new SnowmanLabsChallengeException("Invalid tourist spot identifier.");
            }

            #endregion Validations

            this.UserId = userId;
            this.TouristSpotId = touristSpotId;
            this.Up = up;
            this.Down = down;
        }

        public Guid UserId { get; private set; }

        public int TouristSpotId { get; private set; }

        public TouristSpot TouristSpot { get; set; }

        public bool Up { get; private set; }

        public bool Down { get; private set; }
    }
}
