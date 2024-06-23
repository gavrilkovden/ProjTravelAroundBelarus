using Attractions.Application.Caches.AttractionCaches;
using Attractions.Application.Handlers.Attractions.Queries.GetAttraction;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using System.Linq.Expressions;
using Travel.Application.Dtos;
using Travels.Domain;
using Xunit.Abstractions;
using Core.Users.Domain.Enums;
using AutoFixture;

namespace Travel.UnitTests.Tests.Attractions.Queries.GetAttraction
{
    public class GetAttractionQueryHandlerTest : RequestHandlerTestBase<GetAttractionQuery, GetAttractionDto>
    {
        private readonly Mock<IBaseReadRepository<Attraction>> _attractionsMock = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly AttractionMemoryCache _attractionMemoryCacheMock;

        public GetAttractionQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _attractionMemoryCacheMock = new AttractionMemoryCache();
        }

        protected override IRequestHandler<GetAttractionQuery, GetAttractionDto> CommandHandler =>
            new GetAttractionQueryHandler(
                _attractionsMock.Object,
                _currentUserServiceMock.Object,
                _mapperMock.Object,
                _attractionMemoryCacheMock
            );

        [Fact]
        public async Task Should_Return_Attraction_When_Found_By_Id()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);
            var attractionId = 1;
            var query = new GetAttractionQuery { Id = attractionId };

            var attractions = TestFixture.Build<Attraction>()
    .With(a => a.IsApproved, true)
    .CreateMany(10)
    .ToArray();
            var attraction = new Attraction
            {
                Id = attractionId,
                Name = "Test Attraction",
                Description = "Test description",
                IsApproved = true,
                UserId = userId,
                Address = new Address
                {
                    City = "testCity",
                    Region = "testRegion"
                },
                WorkSchedules = null,
                GeoLocation = null,
                AttractionFeedback = null
            };

            var attractionDto = new GetAttractionDto
            {
                Id = attraction.Id,
                Name = attraction.Name,
                Description = attraction.Description,
                IsApproved = attraction.IsApproved,

            };

            _attractionsMock.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), default))
                                .ReturnsAsync(attraction);

            _mapperMock.Setup(m => m.Map<GetAttractionDto>(attraction)).Returns(attractionDto);

            // act and assert
            await AssertNotThrow(query);
        }

        [Fact]
        public async Task Should_Throw_NotFound_Exception_When_Attraction_Not_Found()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);
            var attractionId = 1;
            var query = new GetAttractionQuery { Id = attractionId };

            _attractionsMock.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), default))
                .ReturnsAsync(null as Attraction);

            // act and assert
            await AssertThrowNotFound(query);
        }

        [Fact]
        public async Task Should_Throw_Forbidden_Exception_When_Attraction_Under_Moderation_And_Not_Admin()
        {
            // arrange
            var userId = Guid.NewGuid();
            var attractionId = 1;
            var query = new GetAttractionQuery { Id = attractionId };

            var attraction = new Attraction
            {
                Id = attractionId,
                Name = "Test Attraction",
                Description = "Test description",
                IsApproved = false,
                UserId = Guid.NewGuid(), // Not the current user
                Address = new Address
                {
                    City = "testCity",
                    Region = "testRegion"
                },
                WorkSchedules = null,
                GeoLocation = null,
                AttractionFeedback = null
            };

            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentUserServiceMock.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(false);
            _attractionsMock.Setup(p => p.AsAsyncRead()
                .SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(attraction);


            // act and assert
            await AssertThrowForbiddenFound(query);
        }
    }
}
