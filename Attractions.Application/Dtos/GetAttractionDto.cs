using Attractions.Application.Dtos;
using Core.Application.Abstractions.Mappings;
using Travels.Domain;

namespace Travel.Application.Dtos
{
    public class GetAttractionDto : IMapFrom<Attraction>
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
        public List<GetImageDto> Images { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
 