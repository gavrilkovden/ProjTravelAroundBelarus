using Core.Application.Abstractions.Mappings;
using Travels.Domain;

namespace Tours.Application.Dtos
{
    public class GetTourDto : IMapFrom<Tour>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RouteId { get; set; }
        public decimal Price { get; set; }
        public double? AverageRating { get; set; }
        public bool IsApproved { get; set; }
    }
}
