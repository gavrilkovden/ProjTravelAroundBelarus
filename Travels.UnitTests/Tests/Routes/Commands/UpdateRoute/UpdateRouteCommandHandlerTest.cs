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
using Xunit.Abstractions;
using Travels.Domain;
using Routes.Application.Handlers.Commands.UpdateRoute;
using Core.Users.Domain.Enums;
using AutoFixture;
using System.Linq.Expressions;
using Core.Tests.Helpers;

namespace Travel.UnitTests.Tests.Routes.Commands.UpdateRoute
{
    public class UpdateRouteCommandHandlerTest : RequestHandlerTestBase<UpdateRouteCommand, GetRouteDto>
    {
        private readonly Mock<IBaseWriteRepository<Route>> _routesMok = new();
        private readonly Mock<ICurrentUserService> _currentServiceMok = new();
        private readonly ICleanRoutesCacheService _cleanRotesCacheService;
        private readonly IMapper _mapper;

        public UpdateRouteCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(UpdateRouteCommand).Assembly).Mapper;
            _cleanRotesCacheService = new CleanRoutesCacheService(
                new Mock<RouteMemoryCache>().Object,
                new Mock<RoutesListMemoryCache>().Object,
                new Mock<RoutesCountMemoryCache>().Object);
        }

        protected override IRequestHandler<UpdateRouteCommand, GetRouteDto> CommandHandler =>
            new UpdateRouteCommandHandler(_routesMok.Object, _currentServiceMok.Object, _mapper,
                _cleanRotesCacheService);

        [Theory, FixtureInlineAutoData]
        public async Task Should_Create_Route_Successfully_ByAdmin(UpdateRouteCommand command, Guid userId)
        {
            // arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(true);

            var route = TestFixture.Build<Route>().Create();
            route.UserId = GuidHelper.GetNotEqualGiud(userId);

            _routesMok.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Route, bool>>>(), default)
            ).ReturnsAsync(route);

            _routesMok.Setup(p => p.UpdateAsync(It.IsAny<Route>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(route);

            // act and assert
            await AssertNotThrow(command);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_Create_Route_Successfully_ByOwner(UpdateRouteCommand command, Guid userId)
        {
            // arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(false);

            var route = TestFixture.Build<Route>().Create();
            route.UserId = userId;

            _routesMok.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Route, bool>>>(), default)
            ).ReturnsAsync(route);

            _routesMok.Setup(p => p.UpdateAsync(It.IsAny<Route>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(route);

            // act and assert
            await AssertNotThrow(command);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_ThrowForbidden_When_GetRoutesWithOtherOwnerByClient(UpdateRouteCommand command, Guid userId)
        {
            // arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(false);

            var route = TestFixture.Build<Route>().Create();
            route.UserId = GuidHelper.GetNotEqualGiud(userId);

            _routesMok.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Route, bool>>>(), default)
            ).ReturnsAsync(route);

            _routesMok.Setup(p => p.UpdateAsync(It.IsAny<Route>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(route);

            // act and assert
            await AssertThrowForbiddenFound(command);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_ThrowNotFound_When_RouteNotFound(UpdateRouteCommand command, Guid userId)
        {
            // arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(true);

            var route = TestFixture.Build<Route>().Create();
            route.UserId = GuidHelper.GetNotEqualGiud(userId);

            _routesMok.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Route, bool>>>(), default)
            ).ReturnsAsync(null as Route);

            _routesMok.Setup(p => p.UpdateAsync(It.IsAny<Route>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(route);

            // act and assert
            await AssertThrowNotFound(command);
        }
    }
}
