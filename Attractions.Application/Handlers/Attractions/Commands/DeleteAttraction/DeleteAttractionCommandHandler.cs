using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Attractions.Application.Caches.AttractionCaches;
using Travels.Domain;

namespace Attractions.Application.Handlers.Attractions.Commands.DeleteAttraction
{
    public class DeleteAttractionCommandHandler : IRequestHandler<DeleteAttractionCommand>
    {
        private readonly IBaseWriteRepository<Attraction> _attractions;

        private readonly ICurrentUserService _currentUserService;

        private readonly ICleanAttractionsCacheService _cleanAttractionsCacheService;

        public DeleteAttractionCommandHandler(IBaseWriteRepository<Attraction> attractions, ICurrentUserService currentUserService, ICleanAttractionsCacheService cleanAttractionsCacheService)
        {
            _attractions = attractions;
            _currentUserService = currentUserService;
            _cleanAttractionsCacheService = cleanAttractionsCacheService;
        }
        public async Task Handle(DeleteAttractionCommand request, CancellationToken cancellationToken)
        {
            var attraction = await _attractions.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (attraction is null)
            {
                throw new NotFoundException(request);
            }

            if (attraction.UserId != _currentUserService.CurrentUserId &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException();
            }

            await _attractions.RemoveAsync(attraction, cancellationToken);
            _cleanAttractionsCacheService.ClearAllCaches();
        }
    }
}
