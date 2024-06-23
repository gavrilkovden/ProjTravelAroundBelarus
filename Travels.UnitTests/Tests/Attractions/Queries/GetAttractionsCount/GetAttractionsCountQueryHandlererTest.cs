using Attractions.Application.Caches.AttractionCaches;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests;
using MediatR;
using Moq;
using Travels.Domain;
using Xunit.Abstractions;
using Attractions.Application.Handlers.Attractions.Queries.GetAttractionsCount;
using Core.Users.Domain.Enums;
using System.Linq.Expressions;

namespace Travel.UnitTests.Tests.Attractions.Queries.GetAttractionsCount
{
    public class GetAttractionsCountQueryHandlererTest : RequestHandlerTestBase<GetAttractionsCountQuery, int>
    {
        private readonly Mock<IBaseReadRepository<Attraction>> _attractionsMock = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly AttractionsCountMemoryCache _attractionsCountMemoryCacheMock;

        public GetAttractionsCountQueryHandlererTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _attractionsCountMemoryCacheMock = new AttractionsCountMemoryCache();
        }

        protected override IRequestHandler<GetAttractionsCountQuery, int> CommandHandler =>
        new GetAttractionsCountQueryHandler(_attractionsMock.Object, _attractionsCountMemoryCacheMock,_currentUserServiceMock.Object);

        [Fact]
        public async Task Should_BeValid_When_GetAttractionsCountByAdmin()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            var query = new GetAttractionsCountQuery();
            var count = 10;

            _currentUserServiceMock.Setup(
                    p => p.UserInRole(ApplicationUserRolesEnum.Admin))
                .Returns(true);

            _attractionsMock.Setup(
                p => p.AsAsyncRead().CountAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), default)
            ).ReturnsAsync(count);

            // act and assert
            await AssertNotThrow(query);
        }

        [Fact]
        public async Task Should_BeValid_When_GetAttractionsCountByClient()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            var query = new GetAttractionsCountQuery();
            var count = 5;

            _currentUserServiceMock.Setup(
                    p => p.UserInRole(ApplicationUserRolesEnum.Admin))
                .Returns(false);

            _attractionsMock.Setup(
                p => p.AsAsyncRead().CountAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), default)
            ).ReturnsAsync(count);

            // act and assert
            await AssertNotThrow(query);
        }
    }
}
