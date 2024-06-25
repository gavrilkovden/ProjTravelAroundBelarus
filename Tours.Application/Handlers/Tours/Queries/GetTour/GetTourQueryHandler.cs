using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using Tours.Application.Caches.TourCaches;
using Tours.Application.Dtos;
using Travels.Domain;

namespace Tours.Application.Handlers.Tours.Queries.GetTour
{
    internal class GetTourQueryHandler : BaseCashedForUserQuery<GetTourQuery, GetTourDto>
    {
        private readonly IBaseReadRepository<Tour> _tours;

        private readonly IMapper _mapper;

        private readonly ICurrentUserService _currentUserService;

        public GetTourQueryHandler(IBaseReadRepository<Tour> tours, ICurrentUserService currentUserService, IMapper mapper, TourMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
        {
            _tours = tours;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public override async Task<GetTourDto> SentQueryAsync(GetTourQuery request, CancellationToken cancellationToken)
        {
            var tour = await _tours.AsAsyncRead(d => d.TourFeedback, p => p.Route).SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken); 

            if (tour is null)
            {
                throw new NotFoundException(request);
            }

            if (tour.IsApproved == false && tour.Route.UserId != _currentUserService.CurrentUserId &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException("This entity is unavailable because it is under moderation");
            }

            var tourResult = _mapper.Map<GetTourDto>(tour);
            var ratings = tour.TourFeedback?.Where(f => f.Value.HasValue).Select(f => f.Value.Value).ToList();

            if (ratings != null && ratings.Count > 0)
            {
                tourResult.AverageRating = Math.Round(ratings.Average(), 1);
            }
            else
            {
                tourResult.AverageRating = null; // Не устанавливаем значение, если оценок нет
            }

            return tourResult;
        }
    }
}
