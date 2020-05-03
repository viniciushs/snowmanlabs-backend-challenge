namespace SnowmanLabsChallenge.Application.ViewModels
{
    public class VoteCountViewModel
    {
        public int TouristSpotId { get; set; }

        public TouristSpotViewModel TouristSpot { get; set; }

        public int Up { get; set; }

        public int Down { get; set; }
    }
}
