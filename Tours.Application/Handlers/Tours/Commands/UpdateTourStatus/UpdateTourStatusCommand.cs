using MediatR;
using System.Text.Json.Serialization;
using Tours.Application.Dtos;

namespace Tours.Application.Handlers.Tours.Commands.UpdateTourStatus
{
    public class UpdateTourStatusCommand : IRequest<GetTourDto>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public bool IsApproved { get; set; }
    }
}
