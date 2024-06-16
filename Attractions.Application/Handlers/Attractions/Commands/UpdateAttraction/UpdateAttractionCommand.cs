using Attractions.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Travel.Application.Dtos;

namespace Attractions.Application.Handlers.Attractions.Commands.UpdateAttraction
{
    public class UpdateAttractionCommand : IRequest<GetAttractionDto>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? NumberOfVisitors { get; set; }
        public AddressDto Address { get; set; }
        public GeoLocationDto GeoLocation { get; set; }
        public List<WorkScheduleDto>? WorkSchedules { get; set; } = null;
    }
}


