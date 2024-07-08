using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Routes.Application.Caches;
using Routes.Application.Dtos;
using Travels.Domain;

namespace Routes.Application.Handlers.Commands.CreateAttractionInRoute
{

    public class CreateAttractionInRouteCommandHandler : IRequestHandler<CreateAttractionInRouteCommand, GetAttractionInRouteDto>
    {
        private readonly IBaseWriteRepository<AttractionInRoute> _attractionInRoutes;
        private readonly IBaseReadRepository<Route> _routes;
        private readonly IBaseReadRepository<Attraction> _attractions;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly ICleanRoutesCacheService _cleanRotesCacheService;

        public CreateAttractionInRouteCommandHandler(IBaseReadRepository<Route> routes, IBaseWriteRepository<AttractionInRoute> attractionInRoutes, IBaseReadRepository<Attraction> attraction, ICurrentUserService currentUserService, IMapper mapper, ICleanRoutesCacheService cleanRotesCacheService)
        {
            _routes = routes;
            _attractionInRoutes = attractionInRoutes;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _cleanRotesCacheService = cleanRotesCacheService;
            _attractions = attraction;
        }

        public async Task<GetAttractionInRouteDto> Handle(CreateAttractionInRouteCommand request, CancellationToken cancellationToken)
        {
            var route = await _routes.AsAsyncRead().SingleOrDefaultAsync(a => a.Id == request.RouteId, cancellationToken);
            
            if (route == null)
            {
                throw new NotFoundException("Route not found.");
            }

            if (route.UserId != _currentUserService.CurrentUserId &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException("You don't have the rights to add this route to the attractions. Only the owner or admin of the route can do this");
            }

            var attraction = await _attractions.AsAsyncRead().SingleOrDefaultAsync(a => a.Id == request.AttractionId, cancellationToken);

            if (attraction == null)
            {
                throw new NotFoundException ("Attraction not found.");
            }

            if (!attraction.IsApproved)
            {
                throw new ForbiddenException("The attraction must be approved to be included in the route.");
            }

            var attractionInRoute = new AttractionInRoute
            {
                Order = request.Order,
                DistanceToNextAttraction = request.DistanceToNextAttraction,
                VisitDateTime = request.VisitDateTime,
                RouteId = request.RouteId,
                AttractionId = request.AttractionId
            };

            attractionInRoute = await _attractionInRoutes.AddAsync(attractionInRoute, cancellationToken);

            _cleanRotesCacheService.ClearAllCaches();
           
            return _mapper.Map<GetAttractionInRouteDto>(attractionInRoute);
        }
    }
}
