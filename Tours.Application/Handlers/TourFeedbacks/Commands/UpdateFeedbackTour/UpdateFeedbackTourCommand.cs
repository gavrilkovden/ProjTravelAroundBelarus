using MediatR;
using System.Text.Json.Serialization;
using Tours.Application.Dtos;

namespace Tours.Application.Handlers.TourFeedbacks.Commands.UpdateFeedbackTour
{
    public class UpdateFeedbackTourCommand : IRequest<GetFeedbackTourDto>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int? Value { get; set; } = null;
        public int TourId { get; set; }
        public string? Comment { get; set; } = null;
    }
}
