using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.Handlers.Attractions.Commands.DeleteAttraction;

namespace Attractions.Application.Handlers.AttractionFeedbacks.Commands.DeleteFeedbackAttraction
{
    public class DeleteFeedbackAttractionCommandValidator : AbstractValidator<DeleteFeedbackAttractionCommand>
    {
        public DeleteFeedbackAttractionCommandValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0);
        }
    }
}
