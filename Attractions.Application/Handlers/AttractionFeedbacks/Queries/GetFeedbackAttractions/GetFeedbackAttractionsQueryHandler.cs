using Attractions.Application.Caches.AttractionFeedback;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Core.Auth.Application.Abstractions.Service;
using Travel.Application.Dtos;
using Travels.Domain;

namespace Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttractions
{
    public class GetFeedbackAttractionsQueryHandler : BaseCashedQuery<GetFeedbackAttractionsQuery, BaseListDto<GetFeedbackAttractionDto>>
    {
        private readonly IBaseReadRepository<AttractionFeedback> _attractionFeedbacks;

        private readonly IMapper _mapper;

        private readonly ICurrentUserService _currentUserService;

        public GetFeedbackAttractionsQueryHandler(IBaseReadRepository<AttractionFeedback> attractionFeedbacks, ICurrentUserService currentUserService, IMapper mapper, AttractionFeedbacksListMemoryCache cache) : base(cache)
        {
            _attractionFeedbacks = attractionFeedbacks;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public override async Task<BaseListDto<GetFeedbackAttractionDto>> SentQueryAsync(GetFeedbackAttractionsQuery request, CancellationToken cancellationToken)
        {
            var query = _attractionFeedbacks.AsQueryable();

            if (request.Offset.HasValue)
            {
                query = query.Skip(request.Offset.Value);
            }

            if (request.Limit.HasValue)
            {
                query = query.Take(request.Limit.Value);
            }

            var entitiesResult = await _attractionFeedbacks.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            var entitiesCount = await _attractionFeedbacks.AsAsyncRead().CountAsync(query, cancellationToken);

            var items = _mapper.Map<GetFeedbackAttractionDto[]>(entitiesResult);
            return new BaseListDto<GetFeedbackAttractionDto>
            {
                Items = items,
                TotalCount = entitiesCount
            };
        }
    }
}
