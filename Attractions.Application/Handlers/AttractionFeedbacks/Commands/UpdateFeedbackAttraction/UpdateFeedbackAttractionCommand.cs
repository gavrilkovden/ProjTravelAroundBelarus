using MediatR;
using System.Text.Json.Serialization;
using Travel.Application.Dtos;

namespace Attractions.Application.Handlers.AttractionFeedbacks.Commands.UpdateFeedbackAttraction
{
    public class UpdateFeedbackAttractionCommand : IRequest<GetFeedbackAttractionDto>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int? ValueRating { get; set; } = null;
        public int AttractionId { get; set; }
        public string? Comment { get; set; } = null;
    }
}
