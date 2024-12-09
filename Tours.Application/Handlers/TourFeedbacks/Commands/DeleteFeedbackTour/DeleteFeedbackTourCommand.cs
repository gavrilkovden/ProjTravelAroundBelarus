using Core.Auth.Application.Attributes;
using MediatR;

namespace Tours.Application.Handlers.TourFeedbacks.Commands.DeleteFeedbackTour
{
    [RequestAuthorize]
    public class DeleteFeedbackTourCommand : IRequest
    {
        public int Id { get; init; }
    }
}

