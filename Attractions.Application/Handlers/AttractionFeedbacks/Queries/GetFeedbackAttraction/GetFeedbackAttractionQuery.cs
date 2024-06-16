using Core.Auth.Application.Attributes;
using MediatR;
using Travel.Application.Dtos;

namespace Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttraction
{
    [RequestAuthorize]
    public class GetFeedbackAttractionQuery : IRequest<GetFeedbackAttractionDto>
    {
        public int Id { get; init; }
    }
}
