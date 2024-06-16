using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.Application.Handlers.Attractions.Commands.CreateAttraction
{
    public class CreateAttractionCommandValidator : AbstractValidator<CreateAttractionCommand>
    {
        public CreateAttractionCommandValidator()
        {
            RuleFor(e => e.Name).NotEmpty().NotNull().WithMessage("Name is required.").MaximumLength(200).WithMessage("Name must be 200 characters or less.");
            RuleFor(e => e.Description).MaximumLength(1000).WithMessage("Description must be 1000 characters or less.");
            RuleFor(e => e.Price).GreaterThanOrEqualTo(0).When(x => x.Price.HasValue);
            RuleFor(e => e.NumberOfVisitors).GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");
            RuleFor(x => x.NumberOfVisitors).GreaterThanOrEqualTo(0).When(x => x.NumberOfVisitors.HasValue).WithMessage("NumberOfVisitors must be greater than or equal to 0.");
            RuleFor(x => x.GeoLocation.Latitude).InclusiveBetween(-90, 90).When(x => x.GeoLocation.Latitude.HasValue).WithMessage("Latitude must be between -90 and 90.");
            RuleFor(x => x.GeoLocation.Longitude).InclusiveBetween(-180, 180).When(x => x.GeoLocation.Longitude.HasValue).WithMessage("Longitude must be between -180 and 180.");
            RuleFor(x => x.Address.Region).NotEmpty().NotNull().WithMessage("Region is required.");
            RuleFor(x => x.WorkSchedules).Must(workSchedules => workSchedules == null || workSchedules.Any()).WithMessage("WorkSchedules must not be empty.");
            RuleForEach(e => e.WorkSchedules)
                        .ChildRules(ws =>
                        {
                            ws.RuleFor(x => x.DayOfWeek)
                                .NotEmpty()
                                .Must(BeAValidDayOfWeek)
                                .WithMessage("Invalid DayOfWeek");

                            ws.RuleFor(x => x.OpenTime)
                                .NotEmpty();

                            ws.RuleFor(x => x.CloseTime)
                                .NotEmpty();
                        });
        }
        private bool BeAValidDayOfWeek(string dayOfWeek)
        {
            return Enum.TryParse<DayOfWeek>(dayOfWeek, ignoreCase: true, out _);
        }
    }
}
