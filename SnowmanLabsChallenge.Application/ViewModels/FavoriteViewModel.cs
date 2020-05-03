namespace SnowmanLabsChallenge.Application.ViewModels
{
    using System;

    public class FavoriteViewModel : BaseViewModel
    {
        public Guid UserId { get; set; }

        public int TouristSpotId { get; set; }

        public TouristSpotViewModel TouristSpot { get; set; }
    }
}
