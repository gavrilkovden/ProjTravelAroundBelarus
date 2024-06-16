using Core.Application.DTOs;
using MediatR;
using Tours.Application.Dtos;

namespace Tours.Application.Handlers.TourFeedbacks.Queries.GetFeedbacksTour
{
    public class GetFeedbackToursQuery : ListFeedbackToursFilter, IBasePaginationFilter, IRequest<BaseListDto<GetFeedbackTourDto>>
    {
        public int? Limit { get; init; }

        public int? Offset { get; init; }
    }
}
