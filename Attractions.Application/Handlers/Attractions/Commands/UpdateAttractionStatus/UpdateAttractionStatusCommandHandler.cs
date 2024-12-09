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

namespace Attractions.Application.Handlers.Attractions.Commands.UpdateAttractionStatus
{
    public class UpdateAttractionStatusCommandHandler : IRequestHandler<UpdateAttractionStatusCommand, GetAttractionDto>
    {
        private readonly IBaseWriteRepository<Attraction> _attractions;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly ICleanAttractionsCacheService _cleanAttractionsCacheService;

        public UpdateAttractionStatusCommandHandler(IBaseWriteRepository<Attraction> attractions, ICurrentUserService currentUserService, IMapper mapper, ICleanAttractionsCacheService cleanAttractionsCacheService)
        {
            _attractions = attractions;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _cleanAttractionsCacheService = cleanAttractionsCacheService;
        }

        public async Task<GetAttractionDto> Handle(UpdateAttractionStatusCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException("You do not have the rights to change the status");
            }

            var attraction = await _attractions.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (attraction is null)
            {
                throw new NotFoundException(request);
            }

            _mapper.Map(request, attraction);// сделать как и в tour

            attraction = await _attractions.UpdateAsync(attraction, cancellationToken);
            _cleanAttractionsCacheService.ClearAllCaches();
            return _mapper.Map<GetAttractionDto>(attraction);
        }
    }
}
