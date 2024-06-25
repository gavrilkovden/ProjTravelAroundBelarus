using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Travel.Application.Caches.AttractionFeedback;
using Travels.Domain;

namespace Attractions.Application.Handlers.AttractionFeedbacks.Commands.DeleteFeedbackAttraction
{
    public class DeleteFeedbackAttractionCommandHandler : IRequestHandler<DeleteFeedbackAttractionCommand>
    {
        private readonly IBaseWriteRepository<AttractionFeedback> _attractionFeedbacks;

        private readonly ICurrentUserService _currentUserService;

        private readonly ICleanAttractionFeedbacksCacheService _cleanAttractionFeedbacksCacheService;

        public DeleteFeedbackAttractionCommandHandler(IBaseWriteRepository<AttractionFeedback> attractionFeedbacks, ICurrentUserService currentUserService, ICleanAttractionFeedbacksCacheService cleanAttractionFeedbacksCacheService)
        {
            _attractionFeedbacks = attractionFeedbacks;
            _currentUserService = currentUserService;
            _cleanAttractionFeedbacksCacheService = cleanAttractionFeedbacksCacheService;
        }
        public async Task Handle(DeleteFeedbackAttractionCommand request, CancellationToken cancellationToken)
        {
            var attractionFeedback = await _attractionFeedbacks.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (attractionFeedback is null)
            {
                throw new NotFoundException(request);
            }

            if (_currentUserService.CurrentUserId != attractionFeedback.UserId && !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException("Only the administrator or owner can delete feedback");
            }

            await _attractionFeedbacks.RemoveAsync(attractionFeedback, cancellationToken);
            _cleanAttractionFeedbacksCacheService.ClearAllCaches();
        }
    }
}
