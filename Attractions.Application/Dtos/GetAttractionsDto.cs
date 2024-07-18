using Core.Application.Abstractions.Mappings;
using Travel.Application.Dtos;
using Travels.Domain;

namespace Attractions.Application.Dtos
{
    public class GetAttractionsDto : IMapFrom<Attraction>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? NumberOfVisitors { get; set; }
        public double? AverageRating { get; set; }
        public AddressDto Address { get; set; }
        public GeoLocationDto GeoLocation { get; set; }
        public List<WorkScheduleDto> WorkSchedules { get; set; }
        public bool IsApproved { get; set; }
        public string? ImagePath { get; set; }
    }
}
