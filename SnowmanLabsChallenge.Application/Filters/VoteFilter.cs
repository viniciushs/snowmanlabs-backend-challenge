namespace SnowmanLabsChallenge.Application.Filters
{
    public class VoteFilter : BaseFilter
    {
        public int? TouristSpotId { get; set; }

        public bool? Up { get; set; }

        public bool? Down { get; set; }
    }
}
