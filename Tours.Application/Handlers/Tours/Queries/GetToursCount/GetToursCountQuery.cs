using Core.Auth.Application.Attributes;
using MediatR;

namespace Tours.Application.Handlers.Tours.Queries.GetToursCount
{
    [RequestAuthorize]
    public class GetToursCountQuery : ListToursFilter, IRequest<int>
    {

    }
}
