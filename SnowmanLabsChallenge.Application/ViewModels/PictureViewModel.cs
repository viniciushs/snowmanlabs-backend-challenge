using System;

namespace SnowmanLabsChallenge.Application.ViewModels
{
    public class PictureViewModel : BaseViewModel
    {
        public Guid OwnerId { get; set; }

        public int TouristSpotId { get; set; }

        public string Url { get; set; }

        public string Base64 { get; set; }
    }
}
