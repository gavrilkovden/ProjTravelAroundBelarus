using Core.Application.Abstractions.Mappings;
using Travels.Domain;

namespace Routes.Application.Dtos
{
    public class GetRoutesDto : IMapFrom<Route>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
