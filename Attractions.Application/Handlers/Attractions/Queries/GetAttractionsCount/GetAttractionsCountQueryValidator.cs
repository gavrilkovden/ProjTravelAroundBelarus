using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.Application.Handlers.Attractions.Queries.GetAttractionsCount
{

    public class GetAttractionsCountQueryValidator : AbstractValidator<GetAttractionsCountQuery>
    {
        public GetAttractionsCountQueryValidator()
        {
            RuleFor(e => e).IsValidListAttractionFilter();
        }
    }
}
