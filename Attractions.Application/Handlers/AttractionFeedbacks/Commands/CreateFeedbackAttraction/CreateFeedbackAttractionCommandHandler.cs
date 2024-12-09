using Attractions.Application.Caches.AttractionCaches;
using Attractions.Application.Handlers.AttractionFeedbacks.Commands.CreateFeedbackAttraction;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using MediatR;
using Travel.Application.Caches.AttractionFeedback;
using Travel.Application.Dtos;
using Travels.Domain;

namespace Travel.Application.Handlers.Attractions.Commands.CreateFeedbackAttraction
{
    public class CreateFeedbackAttractionCommandHandler : IRequestHandler<CreateFeedbackAttractionCommand, GetFeedbackAttractionDto>
    {
        private readonly IBaseWriteRepository<AttractionFeedback> _attractionFeedbacks;
        private readonly IBaseWriteRepository<Attraction> _attractions;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICleanAttractionFeedbacksCacheService _cleanAttractionFeedbacksCacheService;
        private readonly ICleanAttractionsCacheService _cleanAttractionsCacheService;

        public CreateFeedbackAttractionCommandHandler(IBaseWriteRepository<AttractionFeedback> attractionFeedbacks, IBaseWriteRepository<Attraction> attractions, ICurrentUserService currentUserService, IMapper mapper,
            ICleanAttractionsCacheService cleanAttractionsCacheService, ICleanAttractionFeedbacksCacheService cleanAttractionFeedbacksCacheService)
        {
            _attractionFeedbacks = attractionFeedbacks;
            _mapper = mapper;
            _cleanAttractionFeedbacksCacheService = cleanAttractionFeedbacksCacheService;
            _cleanAttractionsCacheService = cleanAttractionsCacheService;
            _currentUserService = currentUserService;
            _attractions = attractions;
        }

        public async Task<GetFeedbackAttractionDto> Handle(CreateFeedbackAttractionCommand request, CancellationToken cancellationToken)
        {
            var attraction = await _attractions.AsAsyncRead(a => a.AttractionFeedback).SingleOrDefaultAsync(e => e.Id == request.AttractionId, cancellationToken);

            if (attraction is null)
            {
                throw new NotFoundException(request);
            }

            var existingFeedback = attraction.AttractionFeedback?.FirstOrDefault(d => d.UserId == _currentUserService.CurrentUserId && d.ValueRating.HasValue);

            AttractionFeedback attractionFeedback = new AttractionFeedback()
            {
                UserId = (Guid)_currentUserService.CurrentUserId,
                ValueRating = request.ValueRating,
                AttractionId = request.AttractionId,
                Comment = request.Comment
            };

            if (existingFeedback != null && existingFeedback.ValueRating.HasValue && attractionFeedback.ValueRating.HasValue)
            {
                throw new ForbiddenException("You have already appreciated this attraction. You can only add a comment");
            }

            attractionFeedback = await _attractionFeedbacks.AddAsync(attractionFeedback, cancellationToken);

            _cleanAttractionFeedbacksCacheService.ClearListCaches();

            if (attractionFeedback.ValueRating != null)
                _cleanAttractionsCacheService.ClearAllCaches();

            return _mapper.Map<GetFeedbackAttractionDto>(attractionFeedback);
        }
    }
}
