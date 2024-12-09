using Attractions.Application.Caches.AttractionFeedback;
using Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttractionsCount;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests;
using Core.Users.Domain.Enums;
using MediatR;
using Moq;
using System.Linq.Expressions;
using Travel.Application.Handlers.Attractions.Queries.GetFeedbackAttractionsCount;
using Travels.Domain;
using Xunit.Abstractions;

namespace Travel.UnitTests.Tests.AttractionFeedbacks.Queries.GetFeedbackAttractionsCount
{
    public class GetFeedbackAttractionsCountQueryHandlerTest : RequestHandlerTestBase<GetFeedbackAttractionsCountQuery, int>
    {
        private readonly Mock<IBaseReadRepository<AttractionFeedback>> _attractionFeedbacksMock = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly AttractionFeedbacksCountMemoryCache _attractionFeedbacksCountMemoryCache;

        public GetFeedbackAttractionsCountQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _attractionFeedbacksCountMemoryCache = new AttractionFeedbacksCountMemoryCache();
        }

        protected override IRequestHandler<GetFeedbackAttractionsCountQuery, int> CommandHandler =>
        new GetFeedbackAttractionsCountQueryHandler(_attractionFeedbacksMock.Object, _attractionFeedbacksCountMemoryCache, _currentUserServiceMock.Object);

        [Fact]
        public async Task Should_BeValid_When_GetAttractionFeedbacksCountByAdmin()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            var query = new GetFeedbackAttractionsCountQuery();
            var count = 10;

            _currentUserServiceMock.Setup(
                    p => p.UserInRole(ApplicationUserRolesEnum.Admin))
                .Returns(true);

            _attractionFeedbacksMock.Setup(
                p => p.AsAsyncRead().CountAsync(It.IsAny<Expression<Func<AttractionFeedback, bool>>>(), default)
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

            var query = new GetFeedbackAttractionsCountQuery();
            var count = 5;

            _currentUserServiceMock.Setup(
                    p => p.UserInRole(ApplicationUserRolesEnum.Admin))
                .Returns(false);

            _attractionFeedbacksMock.Setup(
                p => p.AsAsyncRead().CountAsync(It.IsAny<Expression<Func<AttractionFeedback, bool>>>(), default)
            ).ReturnsAsync(count);

            // act and assert
            await AssertNotThrow(query);
        }
    }
}
