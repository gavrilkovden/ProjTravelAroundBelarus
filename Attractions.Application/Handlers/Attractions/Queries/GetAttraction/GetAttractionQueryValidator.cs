using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.Handlers.Attractions.Commands.DeleteAttraction;

namespace Attractions.Application.Handlers.Attractions.Queries.GetAttraction
{
    public class GetAttractionQueryValidator : AbstractValidator<GetAttractionQuery>
    {
        public GetAttractionQueryValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0);
        }
    }
}
