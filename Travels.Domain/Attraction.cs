using Core.Users.Domain;

namespace Travels.Domain
{
    public class Attraction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; } = default;
        public int? NumberOfVisitors { get; set; } = default;
        public Guid UserId { get; set; }
        public int AddressId { get; set; }
        public int? GeoLocationId { get; set; }
        public bool IsApproved { get; set; }

        public GeoLocation? GeoLocation { get; set; }
        public Address Address { get; set; }
        public ICollection<AttractionInRoute> AttractionsInRoutes { get; set; }
        public ICollection<AttractionFeedback>? AttractionFeedback { get; set; }
        public ICollection<WorkSchedule>? WorkSchedules { get; set; }
        public ApplicationUser User { get; set; }
    }
}
