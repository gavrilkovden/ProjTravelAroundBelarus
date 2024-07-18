using Attractions.Application.Caches.AttractionCaches;
using Attractions.Application.Dtos;
using Attractions.Application.Handlers.Attractions.Commands.UpdateAttractionStatus;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Travel.Application.Dtos;
using Travels.Domain;

namespace Attractions.Application.Handlers.Attractions.Commands.UpdateImageApproveStatus
{
    public class UpdateImageApproveStatusCommandHandler : IRequestHandler<UpdateImageApproveStatusCommand, GetImageDto>
    {
        private readonly IBaseWriteRepository<Travels.Domain.Image> _images;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly ICleanAttractionsCacheService _cleanAttractionsCacheService;

        public UpdateImageApproveStatusCommandHandler(IBaseWriteRepository<Travels.Domain.Image> images, ICurrentUserService currentUserService, IMapper mapper, ICleanAttractionsCacheService cleanAttractionsCacheService)
        {
            _images = images;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _cleanAttractionsCacheService = cleanAttractionsCacheService;
        }

        public async Task<GetImageDto> Handle(UpdateImageApproveStatusCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException("You do not have the rights to change the status");
            }

            var image = await _images.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (image is null)
            {
                throw new NotFoundException(request);
            }

            image.IsApproved = request.IsApproved;

            image = await _images.UpdateAsync(image, cancellationToken);
            _cleanAttractionsCacheService.ClearAllCaches();
            return _mapper.Map<GetImageDto>(image);
        }
    }
}
