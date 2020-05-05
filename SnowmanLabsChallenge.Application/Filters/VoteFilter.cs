using System;

namespace SnowmanLabsChallenge.Application.Filters
{
    public class VoteFilter : BaseFilter
    {
        public Guid? UserId { get; set; }

        public int? TouristSpotId { get; set; }

        public bool? Up { get; set; }

        public bool? Down { get; set; }
    }
}
