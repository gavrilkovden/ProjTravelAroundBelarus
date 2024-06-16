using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Microsoft.EntityFrameworkCore;
using Routes.Application.Caches;
using Routes.Application.Dtos;
using Travels.Domain;

namespace Routes.Application.Handlers.Queries.GetRoute
{
    internal class GetRouteQueryHandler : BaseCashedForUserQuery<GetRouteQuery, GetRouteDto>
    {
        private readonly IBaseReadRepository<Route> _routes;

        private readonly IMapper _mapper;

        private readonly ICurrentUserService _currentUserService;

        public GetRouteQueryHandler(IBaseReadRepository<Route> routes, ICurrentUserService currentUserService, IMapper mapper, RouteMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
        {
            _routes = routes;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public override async Task<GetRouteDto> SentQueryAsync(GetRouteQuery request, CancellationToken cancellationToken)
        {
            var route = await _routes.AsAsyncRead().Include(a => a.AttractionsInRoutes).SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (route is null)
            {
                throw new NotFoundException(request);
            }

            return _mapper.Map<GetRouteDto>(route);
        }
    }
}
