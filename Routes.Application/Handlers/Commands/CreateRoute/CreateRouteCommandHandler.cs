using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Auth.Application.Abstractions.Service;
using MediatR;
using Routes.Application.Caches;
using Routes.Application.Dtos;
using Travels.Domain;

namespace Routes.Application.Handlers.Commands.CreateRoute
{
    public class CreateRouteCommandHandler : IRequestHandler<CreateRouteCommand, GetRouteDto>
    {
        private readonly IBaseWriteRepository<Route> _routes;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly ICleanRoutesCacheService _cleanRotesCacheService;

        public CreateRouteCommandHandler(IBaseWriteRepository<Route> routes, ICurrentUserService currentUserService, IMapper mapper, ICleanRoutesCacheService cleanRotesCacheService)
        {
            _routes = routes;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _cleanRotesCacheService = cleanRotesCacheService;
        }

        public async Task<GetRouteDto> Handle(CreateRouteCommand request, CancellationToken cancellationToken)
        {
            var route = new Route
            {
                Name = request.Name,
                Description = request.Description,
                UserId = (Guid)_currentUserService.CurrentUserId
            };

            route = await _routes.AddAsync(route, cancellationToken);

            _cleanRotesCacheService.ClearListCaches();

            return _mapper.Map<GetRouteDto>(route);
        }
    }
}
