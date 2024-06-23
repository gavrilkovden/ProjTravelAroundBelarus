using AutoFixture;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests;
using Core.Tests.Fixtures;
using MediatR;
using Moq;
using Routes.Application.Caches;
using Routes.Application.Dtos;
using Routes.Application.Handlers.Queries.GetRoute;
using System.Linq.Expressions;
using Travels.Domain;
using Xunit.Abstractions;

namespace Travel.UnitTests.Tests.Routes.Queries.GetRoute
{
    public class GetRouteQueryHandlerTest : RequestHandlerTestBase<GetRouteQuery, GetRouteDto>
    {
        private readonly Mock<IBaseReadRepository<Route>> _routesMock = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly IMapper _mapper;
        private readonly RouteMemoryCache _routeMemoryCache;

        public GetRouteQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(GetRouteQuery).Assembly).Mapper;
            _routeMemoryCache = new RouteMemoryCache();
        }

        protected override IRequestHandler<GetRouteQuery, GetRouteDto> CommandHandler =>
            new GetRouteQueryHandler(
                _routesMock.Object,
                _currentUserServiceMock.Object,
                _mapper,
                _routeMemoryCache
            );

        [Fact]
        public async Task Should_Return_Route_When_Found_By_Id()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            var query = new GetRouteQuery();

            var route = TestFixture.Build<Route>().Create();

            _routesMock.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Route, bool>>>(), default)
            ).ReturnsAsync(route);

            // act and assert
            await AssertNotThrow(query);
        }

        [Fact]
        public async Task Should_Throw_NotFound_Exception_When_Route_Not_Found()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            var query = new GetRouteQuery();

            _routesMock.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Route, bool>>>(), default))
                .ReturnsAsync(null as Route);

            // act and assert
            await AssertThrowNotFound(query);
        }
    }
}
