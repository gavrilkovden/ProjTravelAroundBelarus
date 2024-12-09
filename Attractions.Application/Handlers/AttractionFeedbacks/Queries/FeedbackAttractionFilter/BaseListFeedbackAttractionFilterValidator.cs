using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.Application.Handlers.AttractionFeedbacks.Queries.FeedbackAttractionFilter
{
    internal class BaseListFeedbackAttractionFilterValidator : AbstractValidator<ListFeedbackAttractionsFilter>
    {
        public BaseListFeedbackAttractionFilterValidator()
        {
            RuleFor(e => e.FreeText).MaximumLength(50).When(e => e.FreeText is not null);
        }
    }
}
