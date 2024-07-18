using Attractions.Application.Caches.AttractionCaches;
using Attractions.Application.Dtos;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Core.Auth.Application.Abstractions.Service;
using Core.Users.Domain.Enums;
using Travels.Domain;

namespace Attractions.Application.Handlers.Attractions.Queries.GetAttractions
{
    public class GetAttractionsQueryHandler : BaseCashedForUserQuery<GetAttractionsQuery, BaseListDto<GetAttractionsDto>>
    {
        private readonly IBaseReadRepository<Attraction> _attraction;

        private readonly IMapper _mapper;

        private readonly ICurrentUserService _currentUserService;

        public GetAttractionsQueryHandler(IBaseReadRepository<Attraction> attraction, ICurrentUserService currentUserService, IMapper mapper, AttractionsListMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
        {
            _attraction = attraction;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public override async Task<BaseListDto<GetAttractionsDto>> SentQueryAsync(GetAttractionsQuery request, CancellationToken cancellationToken)
        {
            var query = _attraction.AsQueryable();

            if (_currentUserService.UserInRole(ApplicationUserRolesEnum.Client))
            {
                query = query.Where(e => e.IsApproved || e.UserId == _currentUserService.CurrentUserId).Where(ListAttractionWhere.WhereForClient(request, (Guid)_currentUserService.CurrentUserId));
            }

            if (_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                query = query.Where(ListAttractionWhere.WhereForAdmin(request));
            }

            if (request.Offset.HasValue)
            {
                query = query.Skip(request.Offset.Value);
            }

            if (request.Limit.HasValue)
            {
                query = query.Take(request.Limit.Value);
            }

            var entitiesResult = await _attraction.AsAsyncRead(a => a.Address, a => a.GeoLocation, a => a.AttractionFeedback, a => a.WorkSchedules, a => a.Images).ToArrayAsync(cancellationToken);
            var entitiesCount = await _attraction.AsAsyncRead().CountAsync(query, cancellationToken);

            var items = _mapper.Map<GetAttractionsDto[]>(entitiesResult);

            foreach (var item in items)
            {
                var attraction = entitiesResult.First(e => e.Id == item.Id);
                var ratings = attraction.AttractionFeedback?.Where(f => f.ValueRating.HasValue).Select(f => f.ValueRating.Value).ToList();

                if (ratings != null && ratings.Count > 0)
                {
                    item.AverageRating = Math.Round(ratings.Average(), 1);
                }
                else
                {
                    item.AverageRating = null; // Не устанавливаем значение, если оценок нет
                }

                var approvedImage = attraction.Images?.FirstOrDefault(d => d.IsApproved);
                item.ImagePath = approvedImage?.ImagePath;

            }

            return new BaseListDto<GetAttractionsDto>
            {
                Items = items,
                TotalCount = entitiesCount
            };
        }
    }
}
