using Core.Auth.Application.Attributes;
using MediatR;
using Routes.Application.Handlers.Queries;

namespace Routes.Application.Handlers.Queries.GetRotesCount
{
    [RequestAuthorize]
    public class GetRotesCountQuery : ListRoutesFilter, IRequest<int>
    {

    }
}
