namespace Travels.Domain
{
    public class AttractionInRoute
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public decimal DistanceToNextAttraction { get; set; }
        public DateTime VisitDateTime { get; set; }

        public int RouteId { get; set; }
        public int AttractionId { get; set; }
        // Связи
        public Route Route { get; set; }
        public Attraction Attraction { get; set; }
    }
}
