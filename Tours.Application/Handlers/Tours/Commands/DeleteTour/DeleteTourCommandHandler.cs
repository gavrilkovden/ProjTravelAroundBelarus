using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Tours.Application.Caches.TourCaches;
using Travels.Domain;

namespace Tours.Application.Handlers.Tours.Commands.DeleteTour
{
    internal class DeleteTourCommandHandler : IRequestHandler<DeleteTourCommand>
    {
        private readonly IBaseWriteRepository<Tour> _tours;
        private readonly IBaseReadRepository<Route> _routes;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICleanToursCacheService _cleanToursCacheService;

        public DeleteTourCommandHandler(IBaseWriteRepository<Route> routes, IBaseWriteRepository<Tour> tours, ICurrentUserService currentUserService, ICleanToursCacheService cleanToursCacheService)
        {
            _routes = routes;
            _tours = tours;
            _currentUserService = currentUserService;
            _cleanToursCacheService = cleanToursCacheService;
        }
        public async Task Handle(DeleteTourCommand request, CancellationToken cancellationToken)
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

            await _tours.RemoveAsync(tour, cancellationToken);
            _cleanToursCacheService.ClearAllCaches();
        }
    }
}
