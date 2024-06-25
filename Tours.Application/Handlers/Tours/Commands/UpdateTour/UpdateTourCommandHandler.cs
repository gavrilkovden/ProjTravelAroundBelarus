using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Tours.Application.Caches.TourCaches;
using Tours.Application.Dtos;
using Travels.Domain;

namespace Tours.Application.Handlers.Tours.Commands.UpdateTour
{
    public class UpdateTourCommandHandler : IRequestHandler<UpdateTourCommand, GetTourDto>
    {
        private readonly IBaseWriteRepository<Tour> _tours;
        private readonly IBaseReadRepository<Route> _routes;

        private readonly ICurrentUserService _currentUserService;

        private readonly IMapper _mapper;

        private readonly ICleanToursCacheService _cleanToursCacheService;

        public UpdateTourCommandHandler(IBaseWriteRepository<Tour> tours, IBaseReadRepository<Route> routes, ICurrentUserService currentUserService, IMapper mapper, ICleanToursCacheService cleanToursCacheService)
        {
            _tours = tours;
            _routes = routes;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _cleanToursCacheService = cleanToursCacheService;
        }

        public async Task<GetTourDto> Handle(UpdateTourCommand request, CancellationToken cancellationToken)
        {
            var tour = await _tours.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (tour is null)
            {
                throw new NotFoundException(request);
            }
            var route = await _routes.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == tour.RouteId, cancellationToken);
            if (route is null)
            {
                throw new NotFoundException(request);
            }

            if (route.UserId != _currentUserService.CurrentUserId &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException("You do not have the rights to delete the tour");
            }

            _mapper.Map(request, tour);
            tour = await _tours.UpdateAsync(tour, cancellationToken);
            _cleanToursCacheService.ClearAllCaches();
            return _mapper.Map<GetTourDto>(tour);
        }
    }
}
