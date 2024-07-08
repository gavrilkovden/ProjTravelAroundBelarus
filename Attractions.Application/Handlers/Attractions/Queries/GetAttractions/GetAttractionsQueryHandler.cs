using Attractions.Application.Caches.AttractionCaches;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Core.Auth.Application.Abstractions.Service;
using Core.Users.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Travel.Application.Dtos;
using Travels.Domain;

namespace Attractions.Application.Handlers.Attractions.Queries.GetAttractions
{
    public class GetAttractionsQueryHandler : BaseCashedForUserQuery<GetAttractionsQuery, BaseListDto<GetAttractionDto>>
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
        public override async Task<BaseListDto<GetAttractionDto>> SentQueryAsync(GetAttractionsQuery request, CancellationToken cancellationToken)
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

            query = query.Include(a => a.Address).Include(a => a.GeoLocation).Include(a => a.AttractionFeedback).Include(a => a.WorkSchedules);


            var entitiesResult = await _attraction.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            var entitiesCount = await _attraction.AsAsyncRead().CountAsync(query, cancellationToken);

            var items = _mapper.Map<GetAttractionDto[]>(entitiesResult);

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
            }

            return new BaseListDto<GetAttractionDto>
            {
                Items = items,
                TotalCount = entitiesCount
            };
        }
    }
}
