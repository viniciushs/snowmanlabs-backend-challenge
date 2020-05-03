namespace SnowmanLabsChallenge.Application.ViewModels
{
    using System;

    public class VoteViewModel : BaseViewModel
    {
        public Guid UserId { get; set; }

        public int TouristSpotId { get; set; }

        public bool Up { get; set; }

        public bool Down { get; set; }
    }
}
