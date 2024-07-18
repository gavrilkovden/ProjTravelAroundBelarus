using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.Exceptions;
using Core.Users.Domain.Enums;
using Travel.Application.Dtos;
using Attractions.Application.Caches.AttractionCaches;
using Travels.Domain;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Attractions.Application.Dtos;

namespace Attractions.Application.Handlers.Attractions.Queries.GetAttraction
{
    public class GetAttractionQueryHandler : BaseCashedForUserQuery<GetAttractionQuery, GetAttractionDto>
    {
        private readonly IBaseReadRepository<Attraction> _attractions;

        private readonly IMapper _mapper;

        private readonly ICurrentUserService _currentUserService;

        public GetAttractionQueryHandler(IBaseReadRepository<Attraction> attraction, ICurrentUserService currentUserService, IMapper mapper, AttractionMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
        {
            _attractions = attraction;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public override async Task<GetAttractionDto> SentQueryAsync(GetAttractionQuery request, CancellationToken cancellationToken)
        {

            var attraction = await _attractions.AsAsyncRead(a => a.Address, a => a.GeoLocation, a => a.AttractionFeedback, a => a.WorkSchedules, a => a.Images).SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (attraction is null)
            {
                throw new NotFoundException(request);
            }

            if (attraction.IsApproved == false && attraction.UserId != _currentUserService.CurrentUserId &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException("This entity is unavailable because it is under moderation");
            }

            var attractionResult = _mapper.Map<GetAttractionDto>(attraction);
            var ratings = attraction.AttractionFeedback?.Where(f => f.ValueRating.HasValue).Select(f => f.ValueRating.Value).ToList();

            if (ratings != null && ratings.Count > 0)
            {
                attractionResult.AverageRating = Math.Round(ratings.Average(), 1);
            }
            else
            {
                attractionResult.AverageRating = null; // Не устанавливаем значение, если оценок нет
            }
            var currentUserId = _currentUserService.CurrentUserId!.Value;

            // Фильтруем изображения в зависимости от роли пользователя
            bool isAdmin = _currentUserService.UserInRole(ApplicationUserRolesEnum.Admin);

            if (attraction.Images != null && attraction.Images.Count != 0)
            {
                attractionResult.Images = attraction.Images.Where(d => isAdmin || d.IsApproved).Select(_mapper.Map<GetImageDto>).ToList();
            }

            return attractionResult;
        }
    }
}
