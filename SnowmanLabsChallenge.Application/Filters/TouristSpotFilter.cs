namespace SnowmanLabsChallenge.Application.Filters
{
    using System;

    public class TouristSpotFilter : BaseFilter
    {
        public Guid? OwnerId { get; set; }

        public string Name { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public double? RadiusMeters { get; set; }
    }
}
