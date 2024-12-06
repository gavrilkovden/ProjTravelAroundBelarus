using Attractions.Application.Caches.AttractionCaches;
using Attractions.Application.Dtos;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Users.Domain.Enums;
using MediatR;
using Travels.Domain;

namespace Attractions.Application.Handlers.Attractions.Commands.UploadImage
{
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, GetImageDto>
    {
        private readonly IBaseReadRepository<Attraction> _attractions;
        private readonly IBaseWriteRepository<Attraction> _attractionsUpdate;
        private readonly IBaseWriteRepository<Travels.Domain.Image> _images;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly ICleanAttractionsCacheService _cleanAttractionsCacheService;

        public UploadImageCommandHandler(IBaseReadRepository<Attraction> attractions, IBaseWriteRepository<Attraction> attractionsUpdate, IBaseWriteRepository<Travels.Domain.Image> images, ICurrentUserService currentUserService, IMapper mapper, ICleanAttractionsCacheService cleanAttractionsCacheService)
        {
            _images = images;
            _attractions = attractions;
            _attractionsUpdate = attractionsUpdate;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _cleanAttractionsCacheService = cleanAttractionsCacheService;
        }

        public async Task<GetImageDto> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            var isAdmin = _currentUserService.UserInRole(ApplicationUserRolesEnum.Admin);
            var attraction = await _attractions.AsAsyncRead().SingleOrDefaultAsync(e => e.Id == request.AttractionId, cancellationToken);
            if (attraction is null)
            {
                throw new NotFoundException(request);
            }

            string? imagePath = null;

                // Генерация уникального имени файла
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Image.FileName);
                var uploadsFolder = Path.Combine("wwwroot", "images", "attractions");
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                try
                {
                    // Создание директории
                    Directory.CreateDirectory(uploadsFolder);

                    // Сохранение файла
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.Image.CopyToAsync(fileStream);
                    }

                    imagePath = Path.Combine("images", "attractions", uniqueFileName);
                }
                catch (Exception ex)
                {
                    // Обработка исключений при сохранении файла
                    throw new IOException($"Error saving file: {ex.Message}", ex);
                }

            var image = new Travels.Domain.Image
            {
                AttractionId = request.AttractionId,
                ImagePath = imagePath,
                IsApproved = isAdmin,
                IsCover = request.IsCover,
                UserId = (Guid)_currentUserService.CurrentUserId
            };

            image = await _images.AddAsync(image, cancellationToken);

            if (request.IsCover)
            {
                attraction.ImagePath = imagePath;
                await _attractionsUpdate.UpdateAsync(attraction, cancellationToken);
            }
            _cleanAttractionsCacheService.ClearListCaches();

            return _mapper.Map<GetImageDto>(image);
        }
    }
}
