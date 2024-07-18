//using Attractions.Application.Caches.AttractionCaches;
//using Attractions.Application.Handlers.Attractions.Queries.GetAttraction;
//using Attractions.Application.Handlers.Attractions.Queries.GetAttractions;
//using AutoFixture;
//using AutoMapper;
//using Core.Application.Abstractions.Persistence.Repository.Read;
//using Core.Application.DTOs;
//using Core.Auth.Application.Abstractions.Service;
//using Core.Tests;
//using Core.Tests.Fixtures;
//using Core.Users.Domain.Enums;
//using MediatR;
//using Moq;
//using System.Linq.Expressions;
//using Travel.Application.Dtos;
//using Travels.Domain;
//using Xunit.Abstractions;

//namespace Travel.UnitTests.Tests.Attractions.Queries.GetAttractions
//{
//    public class GetAttractionsQueryHandlerTest : RequestHandlerTestBase<GetAttractionsQuery, BaseListDto<GetAttractionDto>>
//    {
//        private readonly Mock<IBaseReadRepository<Attraction>> _attractionsMock = new();
//        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
//        private readonly IMapper _mapper;
//        private readonly AttractionsListMemoryCache _attractionsMemoryCacheMock;

//        public GetAttractionsQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
//        {
//            _mapper = new AutoMapperFixture(typeof(GetAttractionsQuery).Assembly).Mapper;

//            _attractionsMemoryCacheMock =  new AttractionsListMemoryCache();
//        }

//        protected override IRequestHandler<GetAttractionsQuery, BaseListDto<GetAttractionDto>> CommandHandler =>
//        new GetAttractionsQueryHandler(_attractionsMock.Object, _currentUserServiceMock.Object, _mapper, _attractionsMemoryCacheMock);


//        [Fact]
//        public async Task Should_BeValid_When_GetAttractionsByAdmin()
//        {
//            // Arrange
//            var userId = Guid.NewGuid();
//            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

//            var query = new GetAttractionsQuery();

//            var attractions = TestFixture.Build<Attraction>()
//                .With(a => a.IsApproved, true)
//                .CreateMany(10)
//                .ToArray();

//            _currentUserServiceMock.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(true);
//            _attractionsMock.Setup(p => p.AsAsyncRead().ToArrayAsync(It.IsAny<IQueryable<Attraction>>(), default))
//                                     .ReturnsAsync(attractions);


//            // act and assert
//            await AssertNotThrow(query);
//        }

//        [Fact]
//        public async Task Should_BeValid_When_GetAttractionByClient()
//        {
//            // arrange
//            var userId = Guid.NewGuid();
//            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

//            var query = new GetAttractionsQuery();

//            var attractions = TestFixture.Build<Attraction>().CreateMany(10).ToArray();
//            var count = attractions.Length;

//            _currentUserServiceMock.Setup(
//                    p => p.UserInRole(ApplicationUserRolesEnum.Admin))
//                .Returns(false);

//            _attractionsMock.Setup(
//                p => p.AsAsyncRead().ToArrayAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), default)
//            ).ReturnsAsync(attractions);

//            _attractionsMock.Setup(
//                p => p.AsAsyncRead().CountAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), default)
//            ).ReturnsAsync(count);

//            // act and assert
//            await AssertNotThrow(query);
//        }
//    }
//}
