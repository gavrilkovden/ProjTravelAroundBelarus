using Attractions.Application.Caches.ImageCaches;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Travels.Domain;

namespace Attractions.Application.Handlers.Attractions.Commands.DeleteImage
{
    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand>
    {
        private readonly IBaseWriteRepository<Image> _images;

        private readonly ICurrentUserService _currentUserService;

        private readonly ICleanImagesCacheService _cleanImagesCacheService;

        public DeleteImageCommandHandler(IBaseWriteRepository<Image> images, ICurrentUserService currentUserService, ICleanImagesCacheService cleanImagesCacheService)
        {
            _images = images;
            _currentUserService = currentUserService;
            _cleanImagesCacheService = cleanImagesCacheService;
        }
        public async Task Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var image = await _images.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (image is null)
            {
                throw new NotFoundException(request);
            }

            if (image.UserId != _currentUserService.CurrentUserId &&
                       !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException("You do not have the rights to delete the status");
            }

            await _images.RemoveAsync(image, cancellationToken);
            _cleanImagesCacheService.ClearAllCaches();
        }
    }
}
