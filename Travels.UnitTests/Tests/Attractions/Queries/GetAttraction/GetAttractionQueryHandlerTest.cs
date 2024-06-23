using Attractions.Application.Caches.AttractionCaches;
using Attractions.Application.Handlers.Attractions.Queries.GetAttraction;
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
using Core.Users.Domain.Enums;
using AutoFixture;
using Core.Tests.Fixtures;
using Core.Tests.Helpers;

namespace Travel.UnitTests.Tests.Attractions.Queries.GetAttraction
{
    public class GetAttractionQueryHandlerTest : RequestHandlerTestBase<GetAttractionQuery, GetAttractionDto>
    {
        private readonly Mock<IBaseReadRepository<Attraction>> _attractionsMock = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly IMapper _mapper;
        private readonly AttractionMemoryCache _attractionMemoryCacheMock;

        public GetAttractionQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(GetAttractionQuery).Assembly).Mapper;
            _attractionMemoryCacheMock = new AttractionMemoryCache();
        }

        protected override IRequestHandler<GetAttractionQuery, GetAttractionDto> CommandHandler =>
            new GetAttractionQueryHandler(
                _attractionsMock.Object,
                _currentUserServiceMock.Object,
                _mapper,
                _attractionMemoryCacheMock
            );

        [Fact]
        public async Task Should_Return_Attraction_When_Found_By_Id()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            var query = TestFixture.Build<GetAttractionQuery>().Create();

            var attractions = TestFixture.Build<Attraction>().With(a => a.IsApproved, true).CreateMany(10).ToArray();
            var attraction = TestFixture.Build<Attraction>().Create();
            attraction.IsApproved = true;

            _attractionsMock.Setup(p => p.AsAsyncRead(p => p.Address, p => p.GeoLocation, p => p.AttractionFeedback)
            .SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), default))
                                .ReturnsAsync(attraction);

            // act and assert
            await AssertNotThrow(query);
        }

        [Fact]
        public async Task Should_Throw_NotFound_Exception_When_Attraction_Not_Found()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            var query = TestFixture.Build<GetAttractionQuery>().Create();

            var attraction = TestFixture.Build<Attraction>().Create();

            _attractionsMock.Setup(p => p.AsAsyncRead(p => p.Address, p => p.GeoLocation, p => p.AttractionFeedback)
            .SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), default))
                .ReturnsAsync(null as Attraction);

            // act and assert
            await AssertThrowNotFound(query);
        }

        [Fact]
        public async Task Should_Throw_Forbidden_Exception_When_Attraction_Under_Moderation_And_Not_Admin()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            _currentUserServiceMock.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(false);

            var query = TestFixture.Build<GetAttractionQuery>().Create();

            var attraction = TestFixture.Build<Attraction>().Create();
            attraction.IsApproved = false;
            attraction.UserId = GuidHelper.GetNotEqualGiud(userId);

            _attractionsMock.Setup(p => p.AsAsyncRead(p => p.Address, p => p.GeoLocation, p => p.AttractionFeedback)
                .SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(attraction);


            // act and assert
            await AssertThrowForbiddenFound(query);
        }
    }
}
