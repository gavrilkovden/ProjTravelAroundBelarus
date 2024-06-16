using Core.Application.Abstractions.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
