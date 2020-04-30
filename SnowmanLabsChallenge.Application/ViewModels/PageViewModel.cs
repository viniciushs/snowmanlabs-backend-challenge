namespace SnowmanLabsChallenge.Application.ViewModels
{
    public class PageViewModel
    {
        public int Size { get; set; }
        public int TotalElements { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
    }
}
