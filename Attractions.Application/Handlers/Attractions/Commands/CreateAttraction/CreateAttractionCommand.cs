using Attractions.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Travel.Application.Dtos;

namespace Attractions.Application.Handlers.Attractions.Commands.CreateAttraction
{
    public class CreateAttractionCommand : IRequest<GetAttractionDto>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? NumberOfVisitors { get; set; }
        public AddressDto Address { get; set; }
        public GeoLocationDto GeoLocation { get; set; }
        public List<WorkScheduleDto>? WorkSchedules { get; set; }
      //  public IFormFile? Image { get; set; }
    }
}
