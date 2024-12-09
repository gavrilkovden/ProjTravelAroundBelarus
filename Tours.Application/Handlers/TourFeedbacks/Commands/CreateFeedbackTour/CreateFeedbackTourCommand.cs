using MediatR;
using Tours.Application.Dtos;

namespace Tours.Application.Handlers.TourFeedbacks.Commands.CreateFeedbackTour
{
    public class CreateFeedbackTourCommand : IRequest<GetFeedbackTourDto>
    {
        public Guid UserId { get; set; }
        public int? Value { get; set; } = null;
        public int TourId { get; set; }
        public string? Comment { get; set; } = null;
    }
}
