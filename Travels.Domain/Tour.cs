namespace Travels.Domain
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RouteId { get; set; }
        public decimal Price { get; set; }
        public bool IsApproved { get; set; }

        public Route Route { get; set; }
        public ICollection<TourFeedback> TourFeedback { get; set; }
    }
}
