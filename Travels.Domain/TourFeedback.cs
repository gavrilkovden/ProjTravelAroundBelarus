using Core.Users.Domain;

namespace Travels.Domain
{
    public class TourFeedback
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int? Value { get; set; } = null;
        public int TourId { get; set; }
        public string? Comment { get; set; } = null;

        public Tour Tour { get; set; }
        public ApplicationUser User { get; set; }
    }
}
