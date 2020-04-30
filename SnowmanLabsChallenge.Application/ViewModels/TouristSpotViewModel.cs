namespace SnowmanLabsChallenge.Application.ViewModels
{
    public class TouristSpotViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
