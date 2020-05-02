namespace SnowmanLabsChallenge.Domain.Models
{
    using NetTopologySuite.Geometries;
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Entidade TouristSpot.
    /// </summary>
    public class TouristSpot : BaseEntity
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TouristSpot"/> class.
        ///     Contrutor vazio utilizado pelo EntityFramework.
        /// </summary>
        public TouristSpot()
            : base()
        {
        }

        public TouristSpot(
            int id,
            Guid uuid,
            DateTime createdOn,
            bool active,
            string name,
            int categoryId,
            double latitude,
            double longitude)
            : base(id, uuid, createdOn, active)
        {
            #region Validations

            if (string.IsNullOrEmpty(name))
            {
                throw new SnowmanLabsChallengeException("The tourist spot name is required.");
            }

            name = name.Trim();
            if (name.Length == 0)
            {
                throw new SnowmanLabsChallengeException("The tourist spot name can not be empty.");
            }

            if (name.Length > 255)
            {
                throw new SnowmanLabsChallengeException("The tourist spot name can not have more than 255 characters.");
            }

            if (categoryId < 1)
            {
                throw new SnowmanLabsChallengeException("Invalid category.");
            }

            #endregion Validations

            var location = new Point(longitude, latitude) { SRID = 4326 };
            this.Location = location;
        }

        public string Name { get; set; }

        public Point Location { get; private set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        private ICollection<Picture> pictures;

        public ICollection<Picture> Pictures
        {
            get { return this.pictures ?? (this.pictures = new List<Picture>()); }
            private set { this.pictures = value; }
        }

        private ICollection<Comment> comments;

        public ICollection<Comment> Comments
        {
            get { return this.comments ?? (this.comments = new List<Comment>()); }
            private set { this.comments = value; }
        }
    }
}
