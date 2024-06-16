using Attractions.Application.Handlers.AttractionFeedbacks.Queries.FeedbackAttractionFilter;
using Core.Auth.Application.Attributes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttractionsCount
{
    [RequestAuthorize]
    public class GetFeedbackAttractionsCountQuery : ListFeedbackAttractionsFilter, IRequest<int>
    {

    }
}
