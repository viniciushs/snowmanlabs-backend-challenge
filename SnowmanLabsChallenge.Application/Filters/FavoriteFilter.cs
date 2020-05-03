using System;

namespace SnowmanLabsChallenge.Application.Filters
{
    public class FavoriteFilter : BaseFilter
    {
        public int? TouristSpotId { get; set; }

        public Guid? UserId { get; set; }
    }
}
