using Core.Users.Domain;

namespace Travels.Domain
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }
        public ICollection<AttractionInRoute> AttractionsInRoutes { get; set; } = new List<AttractionInRoute>();
        public ICollection<Tour> Tours { get; set; }
    }
}
