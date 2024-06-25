using Attractions.Application.Caches.AttractionCaches;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Travel.Application.Caches.AttractionFeedback;
using Travel.Application.Dtos;
using Travels.Domain;

namespace Attractions.Application.Handlers.AttractionFeedbacks.Commands.UpdateFeedbackAttraction
{
    public class UpdateFeedbackAttractionCommandHandler : IRequestHandler<UpdateFeedbackAttractionCommand, GetFeedbackAttractionDto>
    {
        private readonly IBaseWriteRepository<AttractionFeedback> _attractionFeedbacks;
        private readonly IBaseWriteRepository<Attraction> _attractions;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICleanAttractionFeedbacksCacheService _cleanAttractionFeedbacksCacheService;
        private readonly ICleanAttractionsCacheService _cleanAttractionsCacheService;

        public UpdateFeedbackAttractionCommandHandler(IBaseWriteRepository<AttractionFeedback> attractionFeedbacks, IBaseWriteRepository<Attraction> attractions, ICurrentUserService currentUserService, IMapper mapper,
            ICleanAttractionsCacheService cleanAttractionsCacheService, ICleanAttractionFeedbacksCacheService cleanAttractionFeedbacksCacheService)
        {
            _attractionFeedbacks = attractionFeedbacks;
            _mapper = mapper;
            _cleanAttractionFeedbacksCacheService = cleanAttractionFeedbacksCacheService;
            _cleanAttractionsCacheService = cleanAttractionsCacheService;
            _currentUserService = currentUserService;
            _attractions = attractions;
        }

        public async Task<GetFeedbackAttractionDto> Handle(UpdateFeedbackAttractionCommand request, CancellationToken cancellationToken)
        {
            var attraction = await _attractions.AsAsyncRead(a => a.AttractionFeedback).SingleOrDefaultAsync(e => e.Id == request.AttractionId, cancellationToken);

            if (attraction is null)
            {
                throw new NotFoundException(request);
            }

            var attractionFeedback = await _attractionFeedbacks.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (attractionFeedback is null)
            {
                throw new NotFoundException(request);
            }

            if (attractionFeedback.UserId != _currentUserService.CurrentUserId)
            {
                throw new ForbiddenException("Only the owner can update feedback");
            }

            attractionFeedback.ValueRating = request.ValueRating;
            attractionFeedback.Comment = request.Comment;

            attractionFeedback = await _attractionFeedbacks.UpdateAsync(attractionFeedback, cancellationToken);

            _cleanAttractionFeedbacksCacheService.ClearListCaches();

            if (attractionFeedback.ValueRating != null)
                _cleanAttractionsCacheService.ClearAllCaches();

            return _mapper.Map<GetFeedbackAttractionDto>(attractionFeedback);
        }
    }
}
