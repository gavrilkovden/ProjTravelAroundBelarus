using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Routes.Application.Caches;
using Routes.Application.Dtos;
using Travels.Domain;

namespace Routes.Application.Handlers.Queries.GetRoutes
{
    public class GetRoutesQueryHandler : BaseCashedQuery<GetRoutesQuery, BaseListDto<GetRoutesDto>>
    {
        private readonly IBaseReadRepository<Route> _routes;

        private readonly IMapper _mapper;

        public GetRoutesQueryHandler(IBaseReadRepository<Route> routes, IMapper mapper, RoutesListMemoryCache cache) : base(cache)
        {
            _routes = routes;
            _mapper = mapper;
        }
        public override async Task<BaseListDto<GetRoutesDto>> SentQueryAsync(GetRoutesQuery request, CancellationToken cancellationToken)
        {
            var query = _routes.AsQueryable();

            if (request.Offset.HasValue)
            {
                query = query.Skip(request.Offset.Value);
            }

            if (request.Limit.HasValue)
            {
                query = query.Take(request.Limit.Value);
            }

            var entitiesResult = await _routes.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            var entitiesCount = await _routes.AsAsyncRead().CountAsync(query, cancellationToken);

            var items = _mapper.Map<GetRoutesDto[]>(entitiesResult);
            return new BaseListDto<GetRoutesDto>
            {
                Items = items,
                TotalCount = entitiesCount
            };
        }
    }
}
