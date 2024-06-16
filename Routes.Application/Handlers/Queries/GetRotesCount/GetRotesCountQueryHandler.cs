using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Auth.Application.Abstractions.Service;
using Routes.Application.Caches;
using Travels.Domain;

namespace Routes.Application.Handlers.Queries.GetRotesCount
{
    internal class GetRotesCountQueryHandler : BaseCashedForUserQuery<GetRotesCountQuery, int>
    {
        private readonly IBaseReadRepository<Route> _routes;

        public GetRotesCountQueryHandler(IBaseReadRepository<Route> routes, RoutesCountMemoryCache cache, ICurrentUserService currentUserService) : base(cache, currentUserService.CurrentUserId!.Value)
        {
            _routes = routes;
        }

        public override async Task<int> SentQueryAsync(GetRotesCountQuery request, CancellationToken cancellationToken)
        {
            return await _routes.AsAsyncRead().CountAsync(cancellationToken);
        }
    }
}
