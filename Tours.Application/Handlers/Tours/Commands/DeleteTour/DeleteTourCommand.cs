using Core.Auth.Application.Attributes;
using MediatR;

namespace Tours.Application.Handlers.Tours.Commands.DeleteTour
{
    [RequestAuthorize]
    public class DeleteTourCommand : IRequest
    {
        public int Id { get; init; }
    }
}
