using Attractions.Application.Handlers.AttractionFeedbacks.Queries.FeedbackAttractionFilter;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttractions
{
    public class GetFeedbackAttractionsQueryValidator : AbstractValidator<GetFeedbackAttractionsQuery>
    {
        public GetFeedbackAttractionsQueryValidator()
        {
            RuleFor(e => e).IsValidListFeedbackAttractionFilter();
        }
    }
}
