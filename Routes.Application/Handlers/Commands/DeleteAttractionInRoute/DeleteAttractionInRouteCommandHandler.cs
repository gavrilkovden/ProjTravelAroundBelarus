using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Routes.Application.Caches;
using Travels.Domain;

namespace Routes.Application.Handlers.Commands.DeleteAttractionInRoute
{
    public class DeleteAttractionInRouteCommandHandler : IRequestHandler<DeleteAttractionInRouteCommand>
    {
        private readonly IBaseWriteRepository<AttractionInRoute> _attractionInRoutes;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICleanRoutesCacheService _cleanRoutesCacheService;

        public DeleteAttractionInRouteCommandHandler(
            IBaseWriteRepository<AttractionInRoute> attractionInRoutes, 
            ICurrentUserService currentUserService, IMapper mapper, 
            ICleanRoutesCacheService cleanRotesCacheService)
        {
            _attractionInRoutes = attractionInRoutes;
            _currentUserService = currentUserService;
            _cleanRoutesCacheService = cleanRotesCacheService;
        }
        public async Task Handle(DeleteAttractionInRouteCommand request, CancellationToken cancellationToken)
        {
            var attractionInRoute = await _attractionInRoutes.AsAsyncRead(p => p.Route).SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (attractionInRoute is null)
            {
                throw new NotFoundException(request);
            }

            if (attractionInRoute.Route.UserId != _currentUserService.CurrentUserId &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException("You do not have the rights to delete this attraction");
            }

            await _attractionInRoutes.RemoveAsync(attractionInRoute, cancellationToken);
            _cleanRoutesCacheService.ClearAllCaches();
        }
    }
}
