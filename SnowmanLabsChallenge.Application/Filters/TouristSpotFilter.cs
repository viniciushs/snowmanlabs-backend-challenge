namespace SnowmanLabsChallenge.Application.Filters
{
    public class TouristSpotFilter : BaseFilter
    {
        public string Name { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public double? RadiusMeters { get; set; }
    }
}
