using Attractions.Application.Caches.AttractionFeedback;
using Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttraction;
using AutoFixture;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests;
using MediatR;
using Moq;
using System.Linq.Expressions;
using Travel.Application.Dtos;
using Travels.Domain;
using Xunit.Abstractions;

namespace Travel.UnitTests.Tests.AttractionFeedbacks.Queries.GetFeedbackAttraction
{
    public class GetFeedbackAttractionQueryHandlerTest : RequestHandlerTestBase<GetFeedbackAttractionQuery, GetFeedbackAttractionDto>
    {
        private readonly Mock<IBaseReadRepository<AttractionFeedback>> _attractionFeedbackMock = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly AttractionFeedbackMemoryCache _attractionFeedbackMemoryCache;

        public GetFeedbackAttractionQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _attractionFeedbackMemoryCache = new AttractionFeedbackMemoryCache();
        }

        protected override IRequestHandler<GetFeedbackAttractionQuery, GetFeedbackAttractionDto> CommandHandler =>
            new GetFeedbackAttractionQueryHandler(
                _attractionFeedbackMock.Object,
                _currentUserServiceMock.Object,
                _mapperMock.Object,
                _attractionFeedbackMemoryCache
            );

        [Fact]
        public async Task Should_Return_Attraction_When_Found_By_Id()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            var query = new GetFeedbackAttractionQuery() { Id = 1 };

            var feedbackAttraction = TestFixture.Build<AttractionFeedback>().Create(); 

            _attractionFeedbackMock.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<AttractionFeedback, bool>>>(), default)
            ).ReturnsAsync(feedbackAttraction);

            // act and assert
            await AssertNotThrow(query);
        }

        [Fact]
        public async Task Should_Throw_NotFound_Exception_When_AttractionFeedback_Not_Found()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            var query = new GetFeedbackAttractionQuery() { Id = 1 };

            _attractionFeedbackMock.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<AttractionFeedback, bool>>>(), default))
                .ReturnsAsync(null as AttractionFeedback);

            // act and assert
            await AssertThrowNotFound(query);
        }
    }
}
