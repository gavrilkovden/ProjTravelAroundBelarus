using Attractions.Application.Handlers.AttractionFeedbacks.Queries.FeedbackAttractionFilter;
using Core.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.Dtos;

namespace Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttractions
{
    public class GetFeedbackAttractionsQuery : ListFeedbackAttractionsFilter, IBasePaginationFilter, IRequest<BaseListDto<GetFeedbackAttractionDto>>
    {
        public int? Limit { get; init; }

        public int? Offset { get; init; }
    }
}
