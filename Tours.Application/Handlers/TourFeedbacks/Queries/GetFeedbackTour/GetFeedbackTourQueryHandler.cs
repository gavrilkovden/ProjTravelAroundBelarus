using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Tours.Application.Caches.TourFeedbackCaches;
using Tours.Application.Dtos;
using Travels.Domain;

namespace Tours.Application.Handlers.TourFeedbacks.Queries.GetFeedbackTour
{
    internal class GetFeedbackTourQueryHandler : BaseCashedForUserQuery<GetFeedbackTourQuery, GetFeedbackTourDto>
    {
        private readonly IBaseReadRepository<TourFeedback> _tourFeedbacks;

        private readonly IMapper _mapper;

        private readonly ICurrentUserService _currentUserService;

        public GetFeedbackTourQueryHandler(IBaseReadRepository<TourFeedback> tourFeedbacks, ICurrentUserService currentUserService, IMapper mapper, TourFeedbackMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
        {
            _tourFeedbacks = tourFeedbacks;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public override async Task<GetFeedbackTourDto> SentQueryAsync(GetFeedbackTourQuery request, CancellationToken cancellationToken)
        {
            var tourFeedback = await _tourFeedbacks.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (tourFeedback is null)
            {
                throw new NotFoundException(request);
            }

            return _mapper.Map<GetFeedbackTourDto>(tourFeedback);
        }
    }
}
