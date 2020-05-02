using System;

namespace SnowmanLabsChallenge.Domain.Models
{
    /// <summary>
    ///     Entidade Comment.
    /// </summary>
    public class Comment : BaseEntity
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Comment"/> class.
        ///     Contrutor vazio utilizado pelo EntityFramework.
        /// </summary>
        public Comment()
            : base()
        {
        }

        public Comment(
            int id,
            Guid uuid,
            DateTime createdOn,
            bool active,
            int touristSpotId,
            string content)
            : base(id, uuid, createdOn, active)
        {
            #region Validations

            if (string.IsNullOrEmpty(content))
            {
                throw new SnowmanLabsChallengeException("The comment content is required.");
            }

            content = content.Trim();
            if (content.Length == 0)
            {
                throw new SnowmanLabsChallengeException("The comment content can not be empty.");
            }

            if (content.Length > 2047)
            {
                throw new SnowmanLabsChallengeException("The comment content can not have more than 2047 characters.");
            }

            if (touristSpotId < 1)
            {
                throw new SnowmanLabsChallengeException("Invalid tourist spot.");
            }

            #endregion Validations

            this.TouristSpotId = touristSpotId;
            this.Content = content;
        }

        public int TouristSpotId { get; set; }

        public TouristSpot TouristSpot { get; set; }

        public string Content { get; set; }
    }
}
