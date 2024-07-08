using Core.Application.Abstractions.Mappings;
using Travels.Domain;

namespace Attractions.Application.Dtos
{
    public class WorkScheduleDto : IMapFrom<WorkSchedule>
    {
        public string? DayOfWeek { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
    }
}
