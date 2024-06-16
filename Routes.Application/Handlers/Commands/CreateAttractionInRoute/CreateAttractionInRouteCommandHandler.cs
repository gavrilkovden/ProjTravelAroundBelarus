using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
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
        private readonly IBaseReadRepository<Attraction> _attractions;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly ICleanRoutesCacheService _cleanRotesCacheService;

        public CreateAttractionInRouteCommandHandler(IBaseWriteRepository<AttractionInRoute> attractionInRoutes, IBaseReadRepository<Attraction> attraction, ICurrentUserService currentUserService, IMapper mapper, ICleanRoutesCacheService cleanRotesCacheService)
        {
            _attractionInRoutes = attractionInRoutes;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _cleanRotesCacheService = cleanRotesCacheService;
            _attractions = attraction;
        }

        public async Task<GetAttractionInRouteDto> Handle(CreateAttractionInRouteCommand request, CancellationToken cancellationToken)
        {
            var attraction = await _attractions.AsQueryable().SingleOrDefaultAsync(a => a.Id == request.AttractionId, cancellationToken);

            if (attraction == null)
            {
                throw new ArgumentException("Attraction not found.");
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

            _cleanRotesCacheService.ClearListCaches();

            return _mapper.Map<GetAttractionInRouteDto>(attractionInRoute);
        }
    }
}
