using MediatR;
using System.Text.Json.Serialization;
using Tours.Application.Dtos;

namespace Tours.Application.Handlers.Tours.Commands.UpdateTour
{
    public class UpdateTourCommand : IRequest<GetTourDto>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int RouteId { get; set; }
        public decimal? Price { get; set; }
    }
}
