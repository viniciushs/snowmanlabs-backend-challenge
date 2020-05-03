using System;
using System.Collections.Generic;

namespace SnowmanLabsChallenge.Application.ViewModels
{
    public class TouristSpotViewModel : BaseViewModel
    {
        public Guid OwnerId { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        private ICollection<PictureViewModel> pictures;

        public ICollection<PictureViewModel> Pictures
        {
            get { return this.pictures ?? (this.pictures = new List<PictureViewModel>()); }
            set { this.pictures = value; }
        }
    }
}
