using Attractions.Application.Dtos;
using MediatR;
using System.Text.Json.Serialization;
using Travel.Application.Dtos;

namespace Attractions.Application.Handlers.Attractions.Commands.UpdateImageApproveStatus
{
    public class UpdateImageApproveStatusCommand : IRequest<GetImageDto>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public bool IsApproved { get; set; }
    }
}
