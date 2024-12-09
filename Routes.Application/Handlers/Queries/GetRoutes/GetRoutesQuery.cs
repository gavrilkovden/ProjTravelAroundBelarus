using Core.Application.DTOs;
using MediatR;
using Routes.Application.Dtos;

namespace Routes.Application.Handlers.Queries.GetRoutes
{
    public class GetRoutesQuery : ListRoutesFilter, IBasePaginationFilter, IRequest<BaseListDto<GetRoutesDto>>
    {
        public int? Limit { get; init; }

        public int? Offset { get; init; }
    }
}
