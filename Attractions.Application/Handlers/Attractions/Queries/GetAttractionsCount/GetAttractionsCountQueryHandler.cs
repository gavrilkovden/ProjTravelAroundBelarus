using Attractions.Application.Caches.AttractionCaches;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Auth.Application.Abstractions.Service;
using Core.Users.Domain.Enums;
using Travels.Domain;

namespace Attractions.Application.Handlers.Attractions.Queries.GetAttractionsCount
{
    public class GetAttractionsCountQueryHandler : BaseCashedForUserQuery<GetAttractionsCountQuery, int>
    {
        private readonly IBaseReadRepository<Attraction> _attractions;

        private readonly ICurrentUserService _currentUserService;

        public GetAttractionsCountQueryHandler(IBaseReadRepository<Attraction> attractions, AttractionsCountMemoryCache cache, ICurrentUserService currentUserService) : base(cache, currentUserService.CurrentUserId!.Value)
        {
            _attractions = attractions;
            _currentUserService = currentUserService;
        }

        public override async Task<int> SentQueryAsync(GetAttractionsCountQuery request, CancellationToken cancellationToken)
        {
            return await _attractions.AsAsyncRead().CountAsync(_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin) ? ListAttractionWhere.WhereForAdmin(request) : ListAttractionWhere.WhereForClient(request, _currentUserService.CurrentUserId!.Value), cancellationToken);
        }
    }
}
