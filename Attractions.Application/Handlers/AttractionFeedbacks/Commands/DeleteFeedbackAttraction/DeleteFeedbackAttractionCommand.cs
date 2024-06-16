using Core.Auth.Application.Attributes;
using MediatR;

namespace Attractions.Application.Handlers.AttractionFeedbacks.Commands.DeleteFeedbackAttraction
{
    [RequestAuthorize]
    public class DeleteFeedbackAttractionCommand : IRequest
    {
        public int Id { get; init; }
    }
}
