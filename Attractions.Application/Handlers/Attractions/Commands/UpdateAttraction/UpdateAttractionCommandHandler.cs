using Attractions.Application.Caches.AttractionCaches;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Travel.Application.Dtos;
using Travels.Domain;

namespace Attractions.Application.Handlers.Attractions.Commands.UpdateAttraction
{
    public class UpdateAttractionHandler : IRequestHandler<UpdateAttractionCommand, GetAttractionDto>
    {
        private readonly IBaseWriteRepository<Attraction> _attractions;

        private readonly ICurrentUserService _currentUserService;

        private readonly IMapper _mapper;

        private readonly ICleanAttractionsCacheService _cleanAttractionsCacheService;

        public UpdateAttractionHandler(IBaseWriteRepository<Attraction> attractions, ICurrentUserService currentUserService, IMapper mapper, ICleanAttractionsCacheService cleanAttractionsCacheService)
        {
            _attractions = attractions;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _cleanAttractionsCacheService = cleanAttractionsCacheService;
        }

        public async Task<GetAttractionDto> Handle(UpdateAttractionCommand request, CancellationToken cancellationToken)
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
            _mapper.Map(request, attraction);
            attraction = await _attractions.UpdateAsync(attraction, cancellationToken);
            _cleanAttractionsCacheService.ClearAllCaches();
            return _mapper.Map<GetAttractionDto>(attraction);
        }
    }
}
