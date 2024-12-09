using Core.Auth.Application.Attributes;
using MediatR;

namespace Tours.Application.Handlers.TourFeedbacks.Queries.GetFeedbacksTourCount
{
    [RequestAuthorize]
    public class GetFeedbackToursCountQuery : ListFeedbackToursFilter, IRequest<int>
    {

    }
}
