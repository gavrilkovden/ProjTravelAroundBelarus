using FluentValidation;
using Routes.Application.Handlers.Commands.DeleteRoute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Routes.Application.Handlers.Commands.DeleteAttractionInRoute
{
    public class DeleteAttractionInRouteCommandValidator : AbstractValidator<DeleteAttractionInRouteCommand>
    {
        public DeleteAttractionInRouteCommandValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0);
        }
    }
}
