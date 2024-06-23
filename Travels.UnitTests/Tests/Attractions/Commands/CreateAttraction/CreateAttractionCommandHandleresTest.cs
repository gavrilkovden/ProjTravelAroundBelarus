using Attractions.Application.Caches.AttractionCaches;
using Attractions.Application.Dtos;
using Attractions.Application.Handlers.Attractions.Commands.CreateAttraction;
using AutoFixture;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests;
using Core.Tests.Attributes;
using Core.Tests.Fixtures;
using Core.Users.Domain.Enums;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Moq;
using Travel.Application.Dtos;
using Travels.Domain;
using Xunit.Abstractions;

namespace Travel.UnitTests.Tests.Attractions.Commands.CreateAttraction
{
    public class CreateAttractionCommandHandleresTest : RequestHandlerTestBase<CreateAttractionCommand, GetAttractionDto>
    {
        private readonly Mock<IBaseWriteRepository<Attraction>> _attractionsMok = new();

        private readonly Mock<ICurrentUserService> _currentServiceMok = new();

        private readonly ICleanAttractionsCacheService _cleanAttractionsCacheService;

        private readonly IMapper _mapper;

        public CreateAttractionCommandHandleresTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(CreateAttractionCommand).Assembly).Mapper;
            _cleanAttractionsCacheService = new CleanAttractionsCacheService(new Mock<AttractionMemoryCache>().Object, new Mock<AttractionsListMemoryCache>().Object, new Mock<AttractionsCountMemoryCache>().Object);
        }

        protected override IRequestHandler<CreateAttractionCommand, GetAttractionDto> CommandHandler =>
            new CreateAttractionCommandHandler(_attractionsMok.Object, _currentServiceMok.Object, _mapper, _cleanAttractionsCacheService);


        [Theory, FixtureInlineAutoData]
        public async Task Should_Create_Attraction_When_Admin_Creates(CreateAttractionCommand command, Guid userId)
        {
            // Arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);


            var fixture = new Fixture();
            fixture.Customize<WorkScheduleDto>(c => c
                .With(x => x.DayOfWeek, () => fixture.Create<DayOfWeek>().ToString())
                .With(x => x.OpenTime, TimeSpan.FromHours(8))
                .With(x => x.CloseTime, TimeSpan.FromHours(18)));

            var workSchedulesDto = fixture.CreateMany<WorkScheduleDto>(3).ToList();
            command.WorkSchedules = workSchedulesDto;

            var attraction = new Attraction
            {
                Id = 1,
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                NumberOfVisitors = command.NumberOfVisitors,
                Address = new Address
                {
                    Street = command.Address.Street,
                    City = command.Address.City,
                    Region = command.Address.Region
                },
                GeoLocation = new GeoLocation
                {
                    Latitude = command.GeoLocation.Latitude,
                    Longitude = command.GeoLocation.Longitude
                },
                WorkSchedules = workSchedulesDto.Select(ws => new WorkSchedule
                {
                    DayOfWeek = Enum.Parse<DayOfWeek>(ws.DayOfWeek, ignoreCase: true),
                    OpenTime = ws.OpenTime,
                    CloseTime = ws.CloseTime
                }).ToList()
            };

            var attractionDto = _mapper.Map<GetAttractionDto>(attraction);

            _attractionsMok.Setup(p => p.AddAsync(It.IsAny<Attraction>(), default)).ReturnsAsync(attraction);

            // act & assert
            await AssertNotThrow(command);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_Create_Attraction_When_Client_Creates(CreateAttractionCommand command, Guid userId)
        {
            // Arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Client)).Returns(true);

            var fixture = new Fixture();
            fixture.Customize<WorkScheduleDto>(c => c
                .With(x => x.DayOfWeek, () => fixture.Create<DayOfWeek>().ToString())
                .With(x => x.OpenTime, TimeSpan.FromHours(8))
                .With(x => x.CloseTime, TimeSpan.FromHours(18)));

            var workSchedulesDto = fixture.CreateMany<WorkScheduleDto>(3).ToList();
            command.WorkSchedules = workSchedulesDto;

            var attraction = new Attraction
            {
                Id = 1,
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                NumberOfVisitors = command.NumberOfVisitors,
                Address = new Address
                {
                    Street = command.Address.Street,
                    City = command.Address.City,
                    Region = command.Address.Region
                },
                GeoLocation = new GeoLocation
                {
                    Latitude = command.GeoLocation.Latitude,
                    Longitude = command.GeoLocation.Longitude
                },
                WorkSchedules = workSchedulesDto.Select(ws => new WorkSchedule
                {
                    DayOfWeek = Enum.Parse<DayOfWeek>(ws.DayOfWeek, ignoreCase: true),
                    OpenTime = ws.OpenTime,
                    CloseTime = ws.CloseTime
                }).ToList()
            };

            var attractionDto = _mapper.Map<GetAttractionDto>(attraction);

            _attractionsMok.Setup(p => p.AddAsync(It.IsAny<Attraction>(), default)).ReturnsAsync(attraction);

            // act & assert
            await AssertNotThrow(command);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_Create_Attraction_With_Partial_Data(CreateAttractionCommand command, Guid userId)
        {
            // Arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(true);

            var fixture = new Fixture();
            fixture.Customize<WorkScheduleDto>(c => c
                .With(x => x.DayOfWeek, () => fixture.Create<DayOfWeek>().ToString())
                .With(x => x.OpenTime, TimeSpan.FromHours(8))
                .With(x => x.CloseTime, TimeSpan.FromHours(18)));

            var workSchedulesDto = fixture.CreateMany<WorkScheduleDto>(3).ToList();
            command.WorkSchedules = workSchedulesDto;

            var attraction = new Attraction
            {
                Id = 1,
                Name = command.Name,
                Description = command.Description,
                Price = null,
                NumberOfVisitors = null,
                Address = new Address
                {
                    Street = null,
                    City = null,
                    Region = command.Address.Region
                },
                GeoLocation = null,
                WorkSchedules = null
            };

            var attractionDto = _mapper.Map<GetAttractionDto>(attraction);

            _attractionsMok.Setup(p => p.AddAsync(It.IsAny<Attraction>(), default)).ReturnsAsync(attraction);

            // act & assert
            await AssertNotThrow(command);
        }
    }
}