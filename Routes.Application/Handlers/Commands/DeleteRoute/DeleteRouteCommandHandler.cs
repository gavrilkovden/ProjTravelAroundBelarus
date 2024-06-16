using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Routes.Application.Caches;
using Travels.Domain;

namespace Routes.Application.Handlers.Commands.DeleteRoute
{
    internal class DeleteRouteCommandHandler : IRequestHandler<DeleteRouteCommand>
    {
        private readonly IBaseWriteRepository<Route> _routes;

        private readonly ICurrentUserService _currentUserService;

        private readonly ICleanRoutesCacheService _cleanRoutesCacheService;

        public DeleteRouteCommandHandler(IBaseWriteRepository<Route> routes, ICurrentUserService currentUserService, ICleanRoutesCacheService cleanRoutesCacheService)
        {
            _routes = routes;
            _currentUserService = currentUserService;
            _cleanRoutesCacheService = cleanRoutesCacheService;
        }
        public async Task Handle(DeleteRouteCommand request, CancellationToken cancellationToken)
        {
            var route = await _routes.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (route is null)
            {
                throw new NotFoundException(request);
            }

            if (route.UserId != _currentUserService.CurrentUserId &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException("You do not have the rights to delete the route");
            }

            await _routes.RemoveAsync(route, cancellationToken);
            _cleanRoutesCacheService.ClearAllCaches();
        }
    }
}
