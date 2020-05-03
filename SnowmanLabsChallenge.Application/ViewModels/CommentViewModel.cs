using System;

namespace SnowmanLabsChallenge.Application.ViewModels
{
    public class CommentViewModel : BaseViewModel
    {
        public Guid OwnerId { get; set; }

        public int TouristSpotId { get; set; }

        public string Content { get; set; }
    }
}
