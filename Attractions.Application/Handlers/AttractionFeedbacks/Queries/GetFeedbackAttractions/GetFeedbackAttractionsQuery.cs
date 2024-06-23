using Attractions.Application.Handlers.AttractionFeedbacks.Queries.FeedbackAttractionFilter;
using Core.Application.DTOs;
using MediatR;
using Travel.Application.Dtos;

namespace Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttractions
{
    public class GetFeedbackAttractionsQuery : ListFeedbackAttractionsFilter, IBasePaginationFilter, IRequest<BaseListDto<GetFeedbackAttractionDto>>
    {
        public int? Limit { get; init; }

        public int? Offset { get; init; }
    }
}
