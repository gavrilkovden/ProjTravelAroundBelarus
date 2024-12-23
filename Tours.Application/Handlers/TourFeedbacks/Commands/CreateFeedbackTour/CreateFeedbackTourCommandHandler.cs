﻿using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using MediatR;
using Tours.Application.Caches.TourCaches;
using Tours.Application.Caches.TourFeedbackCaches;
using Tours.Application.Dtos;
using Travels.Domain;

namespace Tours.Application.Handlers.TourFeedbacks.Commands.CreateFeedbackTour
{
    public class CreateFeedbackTourCommandHandler : IRequestHandler<CreateFeedbackTourCommand, GetFeedbackTourDto>
    {
        private readonly IBaseWriteRepository<TourFeedback> _tourFeedbacks;
        private readonly IBaseWriteRepository<Tour> _tours;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICleanTourFeedbacksCacheService _cleanTourFeedbacksCacheService;
        private readonly ICleanToursCacheService _cleanToursCacheService;

        public CreateFeedbackTourCommandHandler(IBaseWriteRepository<Tour> tours, IBaseWriteRepository<TourFeedback> tourFeedbacks, ICurrentUserService currentUserService, IMapper mapper,
            ICleanTourFeedbacksCacheService cleanTourFeedbacksCacheService, ICleanToursCacheService cleanToursCacheService)
        {
            _tourFeedbacks = tourFeedbacks;
            _tours = tours;
            _mapper = mapper;
            _cleanTourFeedbacksCacheService = cleanTourFeedbacksCacheService;
            _cleanToursCacheService = cleanToursCacheService;
            _currentUserService = currentUserService;
        }

        public async Task<GetFeedbackTourDto> Handle(CreateFeedbackTourCommand request, CancellationToken cancellationToken)
        {
            var tour = await _tours.AsAsyncRead(a => a.TourFeedback).SingleOrDefaultAsync(e => e.Id == request.TourId, cancellationToken);
            if (tour is null)
            {
                throw new NotFoundException(request);
            }

            var existingFeedback = tour.TourFeedback?.FirstOrDefault(d => d.UserId == _currentUserService.CurrentUserId);

            TourFeedback tourFeedback = new TourFeedback()
            {
                UserId = (Guid)_currentUserService.CurrentUserId,
                Value = request.Value,
                TourId = request.TourId,
                Comment = request.Comment
            };

            if (existingFeedback != null && existingFeedback.Value.HasValue && tourFeedback.Value.HasValue)
            {
                throw new ForbiddenException("You have already appreciated this tour. You can only add a comment");
            }

                tourFeedback = await _tourFeedbacks.AddAsync(tourFeedback, cancellationToken);

            if (tourFeedback.Value != null)
                _cleanToursCacheService.ClearAllCaches();

            _cleanTourFeedbacksCacheService.ClearListCaches();
            
            return _mapper.Map<GetFeedbackTourDto>(tourFeedback);
        }
    }
}
