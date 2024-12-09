//using AutoMapper;
//using Core.Application.Abstractions.Persistence.Repository.Writing;
//using Core.Auth.Application.Abstractions.Service;
//using Core.Tests.Attributes;
//using Core.Tests.Fixtures;
//using Core.Tests;
//using MediatR;
//using Moq;
//using Travels.Domain;
//using Xunit.Abstractions;
//using Routes.Application.Handlers.Commands.CreateAttractionInRoute;
//using Routes.Application.Dtos;
//using System.Linq.Expressions;
//using Routes.Application.Caches;
//using AutoFixture;
//using Core.Application.Abstractions.Persistence.Repository.Read;

//namespace Travel.UnitTests.Tests.Routes.Commands.CreateAttractionInRoute
//{
//    public class CreateAttractionInRouteCommandHandlerTest : RequestHandlerTestBase<CreateAttractionInRouteCommand, GetAttractionInRouteDto>
//    {
//        private readonly Mock<IBaseWriteRepository<AttractionInRoute>> _attractionInRouteMok = new();
//        private readonly Mock<IBaseReadRepository<Attraction>> _attractionsMok = new();
//        private readonly Mock<ICurrentUserService> _currentServiceMok = new();
//        private readonly ICleanRoutesCacheService _cleanRotesCacheService;
//        private readonly IMapper _mapper;

//        public CreateAttractionInRouteCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
//        {
//            _mapper = new AutoMapperFixture(typeof(CreateAttractionInRouteCommand).Assembly).Mapper;
//            _cleanRotesCacheService = new CleanRoutesCacheService(
//                new Mock<RouteMemoryCache>().Object,
//                new Mock<RoutesListMemoryCache>().Object,
//                new Mock<RoutesCountMemoryCache>().Object);
//        }

//        protected override IRequestHandler<CreateAttractionInRouteCommand, GetAttractionInRouteDto> CommandHandler =>
//            new CreateAttractionInRouteCommandHandler(_attractionInRouteMok.Object, _attractionsMok.Object, _currentServiceMok.Object, _mapper,
//                _cleanRotesCacheService);

//        [Theory, FixtureInlineAutoData]
//        public async Task Should_Create_AttractionInRoute_Successfully(CreateAttractionInRouteCommand command, Guid userId)
//        {
//            // arrange
//            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);

//            var attractionInRoute = TestFixture.Build<AttractionInRoute>().Create();

//            var attraction = TestFixture.Build<Attraction>().Create();
//            attraction.IsApproved = true;

//            _attractionsMok.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), It.IsAny<CancellationToken>()))
//                            .ReturnsAsync(attraction);

//            _attractionInRouteMok.Setup(p => p.AddAsync(It.IsAny<AttractionInRoute>(), It.IsAny<CancellationToken>()))
//                                    .ReturnsAsync(attractionInRoute);

//            // act and assert
//            await AssertNotThrow(command);
//        }

//        [Theory, FixtureInlineAutoData]
//        public async Task Should_Throw_NotFoundException_When_Attraction_Not_Found(CreateAttractionInRouteCommand command, Guid userId)
//        {
//            // Arrange
//            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);

//            _attractionsMok.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), It.IsAny<CancellationToken>()))
//                            .ReturnsAsync(null as Attraction);

//            // Act & Assert
//            await AssertThrowNotFound(command);
//        }

//        [Theory, FixtureInlineAutoData]
//        public async Task Should_Throw_ForbiddenException_When_Attraction_IsNotApproved(CreateAttractionInRouteCommand command, Guid userId)
//        {
//            // Arrange
//            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);

//            var attractionInRoute = TestFixture.Build<AttractionInRoute>().Create();

//            var attraction = TestFixture.Build<Attraction>().Create();

//            attraction.IsApproved = false;

//            _attractionsMok.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), It.IsAny<CancellationToken>()))
//                            .ReturnsAsync(attraction);

//            _attractionInRouteMok.Setup(p => p.AddAsync(It.IsAny<AttractionInRoute>(), It.IsAny<CancellationToken>()))
//                        .ReturnsAsync(attractionInRoute);

//            // Act & Assert
//            await AssertThrowForbiddenFound(command);
//        }
//    }
//}
