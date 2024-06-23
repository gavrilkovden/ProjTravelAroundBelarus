using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.DTOs;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests.Fixtures;
using Core.Tests;
using MediatR;
using Moq;
using Travels.Domain;
using Xunit.Abstractions;
using Routes.Application.Handlers.Queries.GetRoutes;
using Routes.Application.Dtos;
using System.Linq.Expressions;
using Core.Users.Domain.Enums;
using AutoFixture;
using Routes.Application.Caches;

namespace Travel.UnitTests.Tests.Routes.Queries.GetRoutes
{
    public class GetRoutesQueryHandlerTest : RequestHandlerTestBase<GetRoutesQuery, BaseListDto<GetRoutesDto>>
    {
        private readonly Mock<IBaseReadRepository<Route>> _routesMock = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly IMapper _mapper;
        private readonly RoutesListMemoryCache _routesListMemoryCache;

        public GetRoutesQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(GetRoutesQuery).Assembly).Mapper;
            _routesListMemoryCache = new RoutesListMemoryCache();
        }

        protected override IRequestHandler<GetRoutesQuery, BaseListDto<GetRoutesDto>> CommandHandler =>
        new GetRoutesQueryHandler(_routesMock.Object, _currentUserServiceMock.Object, _mapper, _routesListMemoryCache);


        [Fact]
        public async Task Should_BeValid_When_GetRoutesByAdmin()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            var query = new GetRoutesQuery();

            var routes = TestFixture.Build<Route>().CreateMany(10).ToArray();
            var count = routes.Length;

            _currentUserServiceMock.Setup(
                    p => p.UserInRole(ApplicationUserRolesEnum.Admin))
                .Returns(true);

            _routesMock.Setup(
                p => p.AsAsyncRead().ToArrayAsync(It.IsAny<Expression<Func<Route, bool>>>(), default)
            ).ReturnsAsync(routes);

            _routesMock.Setup(
                p => p.AsAsyncRead().CountAsync(It.IsAny<Expression<Func<Route, bool>>>(), default)
            ).ReturnsAsync(count);

            // act and assert
            await AssertNotThrow(query);
        }

        [Fact]
        public async Task Should_BeValid_When_GetRoutesByClient()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            var query = new GetRoutesQuery();

            var routes = TestFixture.Build<Route>().CreateMany(10).ToArray();
            var count = routes.Length;

            _currentUserServiceMock.Setup(
                    p => p.UserInRole(ApplicationUserRolesEnum.Admin))
                .Returns(false);

            _routesMock.Setup(
                p => p.AsAsyncRead().ToArrayAsync(It.IsAny<Expression<Func<Route, bool>>>(), default)
            ).ReturnsAsync(routes);

            _routesMock.Setup(
                p => p.AsAsyncRead().CountAsync(It.IsAny<Expression<Func<Route, bool>>>(), default)
            ).ReturnsAsync(count);

            // act and assert
            await AssertNotThrow(query);
        }
    }
}
