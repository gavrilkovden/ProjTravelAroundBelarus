using Core.Application.Abstractions.Mappings;
using Travels.Domain;

namespace Tours.Application.Dtos
{
    public class GetFeedbackTourDto : IMapFrom<TourFeedback>
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int? Value { get; set; } = null;
        public int TourId { get; set; }
        public string? Comment { get; set; } = null;
    }
}
