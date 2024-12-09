using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Tours.Application.Caches.TourCaches;
using Tours.Application.Caches.TourFeedbackCaches;
using Travels.Domain;

namespace Tours.Application.Handlers.TourFeedbacks.Commands.DeleteFeedbackTour
{
    internal class DeleteFeedbackTourCommandHandler : IRequestHandler<DeleteFeedbackTourCommand>
    {
        private readonly IBaseWriteRepository<TourFeedback> _tourFeedbacks;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICleanTourFeedbacksCacheService _cleanTourFeedbacksCacheService;
        private readonly ICleanToursCacheService _cleanToursCacheService;

        public DeleteFeedbackTourCommandHandler(IBaseWriteRepository<TourFeedback> tourFeedbacks, ICurrentUserService currentUserService,
            ICleanTourFeedbacksCacheService cleanTourFeedbacksCacheService, ICleanToursCacheService cleanToursCacheService)
        {
            _tourFeedbacks = tourFeedbacks;
            _currentUserService = currentUserService;
            _cleanTourFeedbacksCacheService = cleanTourFeedbacksCacheService;
            _cleanToursCacheService = cleanToursCacheService;
        }
        public async Task Handle(DeleteFeedbackTourCommand request, CancellationToken cancellationToken)
        {
            var tourFeedback = await _tourFeedbacks.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (tourFeedback is null)
            {
                throw new NotFoundException(request);
            }

            if (_currentUserService.CurrentUserId != tourFeedback.UserId && !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException("Only the administrator or owner can delete feedback");
            }

            await _tourFeedbacks.RemoveAsync(tourFeedback, cancellationToken);
            _cleanTourFeedbacksCacheService.ClearAllCaches();

            if (tourFeedback.Value != null)
                _cleanToursCacheService.ClearAllCaches();
        }
    }
}
