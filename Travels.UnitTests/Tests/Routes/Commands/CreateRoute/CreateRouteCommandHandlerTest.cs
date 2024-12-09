using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests.Attributes;
using Core.Tests.Fixtures;
using Core.Tests;
using MediatR;
using Moq;
using Routes.Application.Caches;
using Routes.Application.Dtos;
using Travels.Domain;
using Xunit.Abstractions;
using Routes.Application.Handlers.Commands.CreateRoute;
using Core.Users.Domain.Enums;
using AutoFixture;

namespace Travel.UnitTests.Tests.Routes.Commands.CreateRoute
{
    public class CreateRouteCommandHandlerTest : RequestHandlerTestBase<CreateRouteCommand, GetRouteDto>
    {
        private readonly Mock<IBaseWriteRepository<Route>> _routesMok = new();
        private readonly Mock<ICurrentUserService> _currentServiceMok = new();
        private readonly ICleanRoutesCacheService _cleanRotesCacheService;
        private readonly IMapper _mapper;

        public CreateRouteCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(CreateRouteCommand).Assembly).Mapper;
            _cleanRotesCacheService = new CleanRoutesCacheService(
                new Mock<RouteMemoryCache>().Object,
                new Mock<RoutesListMemoryCache>().Object,
                new Mock<RoutesCountMemoryCache>().Object);
        }

        protected override IRequestHandler<CreateRouteCommand, GetRouteDto> CommandHandler =>
            new CreateRouteCommandHandler(_routesMok.Object,_currentServiceMok.Object, _mapper,
                _cleanRotesCacheService);

        [Theory, FixtureInlineAutoData]
        public async Task Should_Create_Route_Successfully_ByAdmin(CreateRouteCommand command, Guid userId)
        {
            // arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(true);

            var route = TestFixture.Build<Route>().Create();

            _routesMok.Setup(p => p.AddAsync(It.IsAny<Route>(), It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(route);

            // act and assert
            await AssertNotThrow(command);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_Create_Route_Successfully_ByClient(CreateRouteCommand command, Guid userId)
        {
            // arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(false);

            var route = TestFixture.Build<Route>().Create();

            _routesMok.Setup(p => p.AddAsync(It.IsAny<Route>(), It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(route);

            // act and assert
            await AssertNotThrow(command);
        }
    }
}
