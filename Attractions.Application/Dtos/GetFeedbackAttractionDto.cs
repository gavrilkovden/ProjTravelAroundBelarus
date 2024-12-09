using Core.Application.Abstractions.Mappings;
using Travels.Domain;

namespace Travel.Application.Dtos
{
    public class GetFeedbackAttractionDto : IMapFrom<AttractionFeedback>
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int? ValueRating { get; set; }
        public int AttractionId { get; set; }
        public string? Comment { get; set; } = null;
    }
}
