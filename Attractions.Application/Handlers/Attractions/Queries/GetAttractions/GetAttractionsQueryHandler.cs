using Attractions.Application.Caches.AttractionCaches;
using Attractions.Application.Dtos;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Core.Auth.Application.Abstractions.Service;
using Core.Users.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Travels.Domain;

namespace Attractions.Application.Handlers.Attractions.Queries.GetAttractions
{
    public class GetAttractionsQueryHandler : BaseCashedForUserQuery<GetAttractionsQuery, BaseListDto<GetAttractionsDto>>
    {
        private readonly IBaseReadRepository<Attraction> _attraction;

        private readonly IMapper _mapper;

        private readonly ICurrentUserService _currentUserService;

        public GetAttractionsQueryHandler(IBaseReadRepository<Attraction> attraction, ICurrentUserService currentUserService, IMapper mapper, AttractionsListMemoryCache cache)
    : base(cache, currentUserService.CurrentUserId ?? Guid.Empty) // Используем Guid.Empty если пользователь неавторизован
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

            else if (_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                query = query.Where(ListAttractionWhere.WhereForAdmin(request));
            }
            else
            {
                // Для неавторизованных пользователей показываем только утвержденные записи
                query = query.Where(ListAttractionWhere.WhereForUnauthenticated(request));
            }

            if (request.Offset.HasValue)
            {
                query = query.Skip(request.Offset.Value);
            }

            if (request.Limit.HasValue)
            {
                query = query.Take(request.Limit.Value);
            }
            query = query.Include(d => d.Address).Include(d => d.GeoLocation).Include(d => d.AttractionFeedback).Include(d => d.WorkSchedules).Include(d => d.Images);


            var entitiesResult = await _attraction.AsAsyncRead().ToArrayAsync(query, cancellationToken);
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

                // Устанавливаем ImagePath для изображения с IsCover = true
                var coverImage = attraction.Images?.FirstOrDefault(img => img.IsCover && img.IsApproved);
                item.ImagePath = coverImage?.ImagePath;

            }

            return new BaseListDto<GetAttractionsDto>
            {
                Items = items,
                TotalCount = entitiesCount
            };
        }
    }
}
