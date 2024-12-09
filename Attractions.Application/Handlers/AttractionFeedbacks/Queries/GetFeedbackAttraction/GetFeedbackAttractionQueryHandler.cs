using Attractions.Application.Caches.AttractionFeedback;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Travel.Application.Dtos;
using Travels.Domain;

namespace Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttraction
{
    public class GetFeedbackAttractionQueryHandler : BaseCashedForUserQuery<GetFeedbackAttractionQuery, GetFeedbackAttractionDto>
    {
        private readonly IBaseReadRepository<AttractionFeedback> _attractionFeedbacks;

        private readonly IMapper _mapper;

        private readonly ICurrentUserService _currentUserService;

        public GetFeedbackAttractionQueryHandler(IBaseReadRepository<AttractionFeedback> attractionFeedbacks, ICurrentUserService currentUserService, IMapper mapper, AttractionFeedbackMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
        {
            _attractionFeedbacks = attractionFeedbacks;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public override async Task<GetFeedbackAttractionDto> SentQueryAsync(GetFeedbackAttractionQuery request, CancellationToken cancellationToken)
        {
            var attractionFeedback = await _attractionFeedbacks.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (attractionFeedback is null)
            {
                throw new NotFoundException(request);
            }

            return _mapper.Map<GetFeedbackAttractionDto>(attractionFeedback);
        }
    }
}
