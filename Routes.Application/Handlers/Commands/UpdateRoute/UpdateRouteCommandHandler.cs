using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Routes.Application.Caches;
using Routes.Application.Dtos;
using Travels.Domain;

namespace Routes.Application.Handlers.Commands.UpdateRoute
{

    public class UpdateRouteCommandHandler : IRequestHandler<UpdateRouteCommand, GetRouteDto>
    {
        private readonly IBaseWriteRepository<Route> _routes;

        private readonly ICurrentUserService _currentUserService;

        private readonly IMapper _mapper;

        private readonly ICleanRoutesCacheService _cleanRotesCacheService;

        public UpdateRouteCommandHandler(IBaseWriteRepository<Route> routes, ICurrentUserService currentUserService, IMapper mapper, ICleanRoutesCacheService cleanRotesCacheService)
        {
            _routes = routes;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _cleanRotesCacheService = cleanRotesCacheService;
        }

        public async Task<GetRouteDto> Handle(UpdateRouteCommand request, CancellationToken cancellationToken)
        {
            var route = await _routes.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (route is null)
            {
                throw new NotFoundException(request);
            }

            if (route.UserId != _currentUserService.CurrentUserId &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException();
            }
            _mapper.Map(request, route);
            route = await _routes.UpdateAsync(route, cancellationToken);
            _cleanRotesCacheService.ClearAllCaches();
            return _mapper.Map<GetRouteDto>(route);
        }
    }
}
