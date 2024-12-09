using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests;
using Core.Users.Domain.Enums;
using MediatR;
using Moq;
using Routes.Application.Caches;
using Routes.Application.Handlers.Queries.GetRotesCount;
using System.Linq.Expressions;
using Travels.Domain;
using Xunit.Abstractions;

namespace Travel.UnitTests.Tests.Routes.Queries.GetRotesCount
{
    public class GetRotesCountQueryHandlerTest : RequestHandlerTestBase<GetRotesCountQuery, int>
    {
        private readonly Mock<IBaseReadRepository<Route>> _routesMock = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly RoutesCountMemoryCache _routesCountMemoryCache;

        public GetRotesCountQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _routesCountMemoryCache = new RoutesCountMemoryCache();
        }

        protected override IRequestHandler<GetRotesCountQuery, int> CommandHandler =>
        new GetRotesCountQueryHandler(_routesMock.Object, _routesCountMemoryCache, _currentUserServiceMock.Object);

        [Fact]
        public async Task Should_BeValid_When_Get_RoutesCountByAdmin()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            var query = new GetRotesCountQuery();
            var count = 10;

            _currentUserServiceMock.Setup(
                    p => p.UserInRole(ApplicationUserRolesEnum.Admin))
                .Returns(true);

            _routesMock.Setup(
                p => p.AsAsyncRead().CountAsync(It.IsAny<Expression<Func<Route, bool>>>(), default)
            ).ReturnsAsync(count);

            // act and assert
            await AssertNotThrow(query);
        }

        [Fact]
        public async Task Should_BeValid_When_Get_RoutesCountCountByClient()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            var query = new GetRotesCountQuery();
            var count = 5;

            _currentUserServiceMock.Setup(
                    p => p.UserInRole(ApplicationUserRolesEnum.Admin))
                .Returns(false);

            _routesMock.Setup(
                p => p.AsAsyncRead().CountAsync(It.IsAny<Expression<Func<Route, bool>>>(), default)
            ).ReturnsAsync(count);

            // act and assert
            await AssertNotThrow(query);
        }
    }
}
