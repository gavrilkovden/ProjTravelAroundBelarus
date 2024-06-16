using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Auth.Application.Abstractions.Service;
using Tours.Application.Caches.TourCaches;
using Tours.Application.Caches.TourFeedbackCaches;
using Travels.Domain;

namespace Tours.Application.Handlers.TourFeedbacks.Queries.GetFeedbacksTourCount
{
    internal class GetFeedbackToursCountQueryHandler : BaseCashedForUserQuery<GetFeedbackToursCountQuery, int>
    {
        private readonly IBaseReadRepository<TourFeedback> _tourFeedbacks;
        private readonly ICurrentUserService _currentUserService;

        public GetFeedbackToursCountQueryHandler(IBaseReadRepository<TourFeedback> tourFeedbacks, TourFeedbacksCountMemoryCache cache, ICurrentUserService currentUserService) : base(cache, currentUserService.CurrentUserId!.Value)
        {
            _tourFeedbacks = tourFeedbacks;
            _currentUserService = currentUserService;
        }

        public override async Task<int> SentQueryAsync(GetFeedbackToursCountQuery request, CancellationToken cancellationToken)
        {
            return await _tourFeedbacks.AsAsyncRead().CountAsync(cancellationToken);
        }
    }
}
