using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Core.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Tours.Application.Caches.TourFeedbackCaches;
using Tours.Application.Dtos;
using Travels.Domain;

namespace Tours.Application.Handlers.TourFeedbacks.Queries.GetFeedbacksTour
{
    public class GetFeedbackToursQueryHandler : BaseCashedQuery<GetFeedbackToursQuery, BaseListDto<GetFeedbackTourDto>>
    {
        private readonly IBaseReadRepository<TourFeedback> _tourFeedbacks;

        private readonly IMapper _mapper;

        public GetFeedbackToursQueryHandler(IBaseReadRepository<TourFeedback> tourFeedbacks, IMapper mapper, TourFeedbacksListMemoryCache cache) : base(cache)
        {
            _tourFeedbacks = tourFeedbacks;
            _mapper = mapper;
        }
        public override async Task<BaseListDto<GetFeedbackTourDto>> SentQueryAsync(GetFeedbackToursQuery request, CancellationToken cancellationToken)
        {
            var query = _tourFeedbacks.AsQueryable();

            if (request.Offset.HasValue)
            {
                query = query.Skip(request.Offset.Value);
            }

            if (request.Limit.HasValue)
            {
                query = query.Take(request.Limit.Value);
            }

            if (request.TourId.HasValue)
            {
                query = query.Where(a => a.TourId == request.TourId.Value);
            }

            var resultExists = await query.AnyAsync();

            if (!resultExists)
            {
                throw new NotFoundException("Nothing was found for the filter");
            }


            var entitiesResult = await _tourFeedbacks.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            var entitiesCount = await _tourFeedbacks.AsAsyncRead().CountAsync(query, cancellationToken);

            var items = _mapper.Map<GetFeedbackTourDto[]>(entitiesResult);
            return new BaseListDto<GetFeedbackTourDto>
            {
                Items = items,
                TotalCount = entitiesCount
            };
        }
    }
}
