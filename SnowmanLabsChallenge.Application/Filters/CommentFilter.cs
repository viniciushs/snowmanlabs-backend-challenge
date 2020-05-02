namespace SnowmanLabsChallenge.Application.Filters
{
    public class CommentFilter : BaseFilter
    {
        public int? TouristSpotId { get; set; }

        public string Content { get; set; }
    }
}
