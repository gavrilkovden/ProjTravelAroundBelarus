using Core.Auth.Application.Attributes;
using MediatR;
using Tours.Application.Dtos;

namespace Tours.Application.Handlers.TourFeedbacks.Queries.GetFeedbackTour
{
    [RequestAuthorize]
    public class GetFeedbackTourQuery : IRequest<GetFeedbackTourDto>
    {
        public int Id { get; init; }
    }
}
