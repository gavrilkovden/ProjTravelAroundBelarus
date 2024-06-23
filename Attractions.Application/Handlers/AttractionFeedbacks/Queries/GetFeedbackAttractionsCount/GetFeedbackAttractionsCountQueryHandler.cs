using Attractions.Application.Caches.AttractionFeedback;
using Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttractionsCount;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Auth.Application.Abstractions.Service;
using Travels.Domain;

namespace Travel.Application.Handlers.Attractions.Queries.GetFeedbackAttractionsCount
{
    public class GetFeedbackAttractionsCountQueryHandler : BaseCashedForUserQuery<GetFeedbackAttractionsCountQuery, int>
    {
        private readonly IBaseReadRepository<AttractionFeedback> _attractionFeedbacks;

        private readonly ICurrentUserService _currentUserService;

        public GetFeedbackAttractionsCountQueryHandler(IBaseReadRepository<AttractionFeedback> attractionFeedbacks, AttractionFeedbacksCountMemoryCache cache, ICurrentUserService currentUserService) : base(cache, currentUserService.CurrentUserId!.Value)
        {
            _attractionFeedbacks = attractionFeedbacks;
            _currentUserService = currentUserService;
        }

        public override async Task<int> SentQueryAsync(GetFeedbackAttractionsCountQuery request, CancellationToken cancellationToken)
        {
            return await _attractionFeedbacks.AsAsyncRead().CountAsync( cancellationToken);
        }
    }
}
