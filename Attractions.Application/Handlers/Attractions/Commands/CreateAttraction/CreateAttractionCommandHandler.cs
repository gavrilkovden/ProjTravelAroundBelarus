using Attractions.Application.Caches.AttractionCaches;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Auth.Application.Abstractions.Service;
using Core.Users.Domain.Enums;
using MediatR;
using Newtonsoft.Json;
using Travel.Application.Dtos;
using Travels.Domain;

namespace Attractions.Application.Handlers.Attractions.Commands.CreateAttraction
{
    public class CreateAttractionCommandHandler : IRequestHandler<CreateAttractionCommand, GetAttractionDto>
    {
        private readonly IBaseWriteRepository<Attraction> _attractions;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly ICleanAttractionsCacheService _cleanAttractionsCacheService;

        public CreateAttractionCommandHandler(IBaseWriteRepository<Attraction> attractions, ICurrentUserService currentUserService, IMapper mapper, ICleanAttractionsCacheService cleanAttractionsCacheService)
        {
            _attractions = attractions;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _cleanAttractionsCacheService = cleanAttractionsCacheService;
        }

        public async Task<GetAttractionDto> Handle(CreateAttractionCommand request, CancellationToken cancellationToken)
        {
            var isAdmin = _currentUserService.UserInRole(ApplicationUserRolesEnum.Admin);

            var workSchedules = new List<WorkSchedule>();

            if (request.WorkSchedules != null && request.WorkSchedules.Any())
            {
                workSchedules = request.WorkSchedules?.Select(ws => new WorkSchedule
                {
                    DayOfWeek = Enum.Parse<DayOfWeek>(ws.DayOfWeek, ignoreCase: true),
                    OpenTime = ws.OpenTime,
                    CloseTime = ws.CloseTime
                }).ToList();
            }

            var address = new Address
            {
                Street = request.Address.Street,
                City = request.Address.City,
                Region = request.Address.Region
            };

            var geoLocation = new GeoLocation
            {
                Latitude = request.GeoLocation.Latitude,
                Longitude = request.GeoLocation.Longitude
            };

              var attraction = new Attraction
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                NumberOfVisitors = request.NumberOfVisitors,
                IsApproved = isAdmin,
                UserId = (Guid)_currentUserService.CurrentUserId,
                Address = address,
                GeoLocation = geoLocation,
                WorkSchedules = workSchedules,
            };

            attraction = await _attractions.AddAsync(attraction, cancellationToken);

             _cleanAttractionsCacheService.ClearListCaches();

            return _mapper.Map<GetAttractionDto>(attraction);
        }
    }
}
