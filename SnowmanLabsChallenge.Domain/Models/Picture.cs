using System;

namespace SnowmanLabsChallenge.Domain.Models
{
    /// <summary>
    ///     Entidade Picture.
    /// </summary>
    public class Picture : BaseEntity
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Picture"/> class.
        ///     Contrutor vazio utilizado pelo EntityFramework.
        /// </summary>
        public Picture()
            : base()
        {
        }

        public Picture(
            int id,
            Guid uuid,
            DateTime createdOn,
            bool active,
            int touristSpotId,
            string url)
            : base(id, uuid, createdOn, active)
        {
            #region Validations

            if (touristSpotId < 1)
            {
                throw new SnowmanLabsChallengeException("Invalid tourist spot identifier.");
            }

            if (string.IsNullOrEmpty(url))
            {
                throw new SnowmanLabsChallengeException("The picture URL is required.");
            }

            url = url.Trim();
            if (url.Length == 0)
            {
                throw new SnowmanLabsChallengeException("The picture URL can not be empty.");
            }

            if (url.Length > 2047)
            {
                throw new SnowmanLabsChallengeException("The picture URL can not have more than 2047 characters.");
            }

            #endregion Validations

            this.Url = url;
        }

        public string Url { get; set; }

        public int TouristSpotId { get; set; }

        public TouristSpot TouristSpot { get; set; }
    }
}
