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

namespace Tours.Application.Handlers.Tours.Commands.CreateTour
{
    public class CreateTourCommandHandler : IRequestHandler<CreateTourCommand, GetTourDto>
    {
        private readonly IBaseWriteRepository<Tour> _tours;
        private readonly IBaseReadRepository<Route> _routes;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly ICleanToursCacheService _cleanToursCacheService;

        public CreateTourCommandHandler(IBaseWriteRepository<Tour> tours, IBaseReadRepository<Route> routes, ICurrentUserService currentUserService, IMapper mapper, ICleanToursCacheService cleanToursCacheService)
        {
            _tours = tours;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _cleanToursCacheService = cleanToursCacheService;
            _routes = routes;
        }

        public async Task<GetTourDto> Handle(CreateTourCommand request, CancellationToken cancellationToken)
        {
            var route = await _routes.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.RouteId, cancellationToken);
            if (route is null)
            {
                throw new NotFoundException(request);
            }

            if (route.UserId != _currentUserService.CurrentUserId &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException("You do not have the rights to use the route");
            }

            var tour = new Tour
            {
                Name = request.Name,
                Description = request.Description,
                RouteId = request.RouteId,
                Price = (decimal)request.Price
            };

            tour = await _tours.AddAsync(tour, cancellationToken);

            _cleanToursCacheService.ClearListCaches();

            return _mapper.Map<GetTourDto>(tour);
        }
    }
}
