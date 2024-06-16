using Core.Users.Domain;

namespace Travels.Domain
{
    public class AttractionFeedback
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int? ValueRating { get; set; } = null;
        public int AttractionId { get; set; }
        public string? Comment { get; set; } = null;

        public Attraction Attraction { get; set; }
        public ApplicationUser User { get; set; }
    }
}
