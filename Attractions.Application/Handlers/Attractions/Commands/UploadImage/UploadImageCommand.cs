using Attractions.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Attractions.Application.Handlers.Attractions.Commands.UploadImage
{
    public class UploadImageCommand : IRequest<GetImageDto>
    {
        public int AttractionId { get; set; }
        public IFormFile? Image { get; set; }
        public bool IsCover { get; set; }

        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
