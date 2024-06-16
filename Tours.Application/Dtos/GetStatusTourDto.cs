using Core.Application.Abstractions.Mappings;
using Travels.Domain;

namespace Tours.Application.Dtos
{
    public class GetStatusTourDto : IMapFrom<Tour>
    {
        public int Id { get; set; }
        public bool IsApproved { get; set; }
    }
}
