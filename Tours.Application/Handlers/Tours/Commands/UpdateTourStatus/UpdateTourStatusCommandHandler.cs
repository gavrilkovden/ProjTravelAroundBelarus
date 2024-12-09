using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Tours.Application.Caches.TourCaches;
using Tours.Application.Dtos;
using Travels.Domain;

namespace Tours.Application.Handlers.Tours.Commands.UpdateTourStatus
{
    public class UpdateTourStatusCommandHandler : IRequestHandler<UpdateTourStatusCommand, GetTourDto>
    {
        private readonly IBaseWriteRepository<Tour> _tours;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly ICleanToursCacheService _cleanToursCacheService;

        public UpdateTourStatusCommandHandler(IBaseWriteRepository<Tour> tours, ICurrentUserService currentUserService, IMapper mapper, ICleanToursCacheService cleanToursCacheService)
        {
            _tours = tours;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _cleanToursCacheService = cleanToursCacheService;
        }

        public async Task<GetTourDto> Handle(UpdateTourStatusCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException("You do not have the rights to change the status");
            }

            var tour = await _tours.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (tour is null)
            {
                throw new NotFoundException(request);
            }

            tour.IsApproved = request.IsApproved;

            tour = await _tours.UpdateAsync(tour, cancellationToken);
            _cleanToursCacheService.ClearAllCaches();
            return _mapper.Map<GetTourDto>(tour);
        }
    }
}
