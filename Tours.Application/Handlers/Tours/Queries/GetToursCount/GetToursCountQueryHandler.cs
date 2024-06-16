using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Auth.Application.Abstractions.Service;
using Tours.Application.Caches.TourCaches;
using Travels.Domain;

namespace Tours.Application.Handlers.Tours.Queries.GetToursCount
{
    internal class GetToursCountQueryHandler : BaseCashedForUserQuery<GetToursCountQuery, int>
    {
        private readonly IBaseReadRepository<Tour> _tour;

        public GetToursCountQueryHandler(IBaseReadRepository<Tour> tours, ToursCountMemoryCache cache, ICurrentUserService currentUserService) : base(cache, currentUserService.CurrentUserId!.Value)
        {
            _tour = tours;
        }

        public override async Task<int> SentQueryAsync(GetToursCountQuery request, CancellationToken cancellationToken)
        {
            return await _tour.AsAsyncRead().CountAsync(cancellationToken);
        }
    }
}
