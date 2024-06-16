using Core.Auth.Application.Attributes;
using MediatR;
using Tours.Application.Dtos;

namespace Tours.Application.Handlers.Tours.Queries.GetTour
{
    [RequestAuthorize]
    public class GetTourQuery : IRequest<GetTourDto>
    {
        public int Id { get; init; }
    }
}
