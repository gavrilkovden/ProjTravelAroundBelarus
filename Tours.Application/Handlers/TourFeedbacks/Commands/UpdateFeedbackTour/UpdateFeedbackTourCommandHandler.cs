using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Tours.Application.Caches.TourCaches;
using Tours.Application.Caches.TourFeedbackCaches;
using Tours.Application.Dtos;
using Travels.Domain;

namespace Tours.Application.Handlers.TourFeedbacks.Commands.UpdateFeedbackTour
{
    public class UpdateFeedbackTourCommandHandler : IRequestHandler<UpdateFeedbackTourCommand, GetFeedbackTourDto>
    {
        private readonly IBaseWriteRepository<TourFeedback> _tourFeedbacks;
        private readonly IBaseReadRepository<Tour> _tours;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICleanTourFeedbacksCacheService _cleanTourFeedbacksCacheService;
        private readonly ICleanToursCacheService _cleanToursCacheService;

        public UpdateFeedbackTourCommandHandler(IBaseWriteRepository<TourFeedback> tourFeedbacks, IBaseWriteRepository<Tour> tours, ICurrentUserService currentUserService, IMapper mapper,
            ICleanTourFeedbacksCacheService cleanTourFeedbacksCacheService, ICleanToursCacheService cleanToursCacheService)
        {
            _tourFeedbacks = tourFeedbacks;
            _tours = tours;
            _mapper = mapper;
            _cleanTourFeedbacksCacheService = cleanTourFeedbacksCacheService;
            _cleanToursCacheService = cleanToursCacheService;
            _currentUserService = currentUserService;
        }

        public async Task<GetFeedbackTourDto> Handle(UpdateFeedbackTourCommand request, CancellationToken cancellationToken)
        {
            var tourFeedback = await _tourFeedbacks.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (tourFeedback is null)
            {
                throw new NotFoundException(request);
            }

            if (tourFeedback.UserId != _currentUserService.CurrentUserId)
            {
                throw new ForbiddenException("Only the owner can update feedback");
            }

            var tour = await _tours.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.TourId, cancellationToken);
            if (tour is null)
            {
                throw new NotFoundException(request);
            }

            tourFeedback.Value = request.Value;
            tourFeedback.Comment = request.Comment;
            tourFeedback.TourId = request.TourId;

            tourFeedback = await _tourFeedbacks.UpdateAsync(tourFeedback, cancellationToken);

            if (tourFeedback.Value != null)
                _cleanToursCacheService.ClearAllCaches();

            _cleanTourFeedbacksCacheService.ClearListCaches();

            return _mapper.Map<GetFeedbackTourDto>(tourFeedback);
        }
    }
}
