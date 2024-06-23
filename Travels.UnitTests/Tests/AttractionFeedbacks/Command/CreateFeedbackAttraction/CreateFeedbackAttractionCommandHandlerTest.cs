using Attractions.Application.Caches.AttractionCaches;
using Attractions.Application.Caches.AttractionFeedback;
using Attractions.Application.Handlers.AttractionFeedbacks.Commands.CreateFeedbackAttraction;
using Attractions.Application.Handlers.Attractions.Commands.CreateAttraction;
using AutoFixture;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests;
using Core.Tests.Attributes;
using Core.Tests.Fixtures;
using MediatR;
using Moq;
using System.Linq.Expressions;
using Travel.Application.Caches.AttractionFeedback;
using Travel.Application.Dtos;
using Travel.Application.Handlers.Attractions.Commands.CreateFeedbackAttraction;
using Travels.Domain;
using Xunit.Abstractions;

namespace Travel.UnitTests.Tests.AttractionFeedbacks.Command.CreateFeedbackAttraction
{
    public class CreateFeedbackAttractionCommandHandlerTest : RequestHandlerTestBase<CreateFeedbackAttractionCommand, GetFeedbackAttractionDto>
    {
        private readonly Mock<IBaseWriteRepository<AttractionFeedback>> _attractionFeedbacksMok = new();
        private readonly Mock<IBaseWriteRepository<Attraction>> _attractionsMok = new();
        private readonly Mock<ICurrentUserService> _currentServiceMok = new();
        private readonly ICleanAttractionsCacheService _cleanAttractionsCacheService;
        private readonly ICleanAttractionFeedbacksCacheService _cleanAttractionFeedbacksCacheService;
        private readonly IMapper _mapper;

        public CreateFeedbackAttractionCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(CreateAttractionCommand).Assembly).Mapper;
            _cleanAttractionsCacheService = new CleanAttractionsCacheService(
                new Mock<AttractionMemoryCache>().Object, 
                new Mock<AttractionsListMemoryCache>().Object,
                new Mock<AttractionsCountMemoryCache>().Object);
            _cleanAttractionFeedbacksCacheService = new CleanAttractionFeedbacksCacheService(
                new Mock<AttractionFeedbackMemoryCache>().Object,
                new Mock<AttractionFeedbacksListMemoryCache>().Object, 
                new Mock<AttractionFeedbacksCountMemoryCache>().Object);
        }

        protected override IRequestHandler<CreateFeedbackAttractionCommand, GetFeedbackAttractionDto> CommandHandler =>
            new CreateFeedbackAttractionCommandHandler(_attractionFeedbacksMok.Object, _attractionsMok.Object, _currentServiceMok.Object, _mapper,
                _cleanAttractionsCacheService, _cleanAttractionFeedbacksCacheService);

        [Theory, FixtureInlineAutoData]
        public async Task Should_Create_Feedback_Successfully(CreateFeedbackAttractionCommand command, Guid userId)
        {
            // arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);

            var attraction = TestFixture.Build<Attraction>().Create();

            var attractionFeedback = TestFixture.Build<AttractionFeedback>().Create();

            _attractionsMok.Setup(p => p.AsAsyncRead(p => p.AttractionFeedback).FirstOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), It.IsAny<CancellationToken>()))
                            .ReturnsAsync(attraction);

            _attractionFeedbacksMok.Setup(p => p.AddAsync(It.IsAny<AttractionFeedback>(), It.IsAny<CancellationToken>())).
                                    ReturnsAsync(attractionFeedback);

            // act and assert
            await AssertNotThrow(command);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_Throw_NotFoundException_When_Attraction_Not_Found(CreateFeedbackAttractionCommand command, Guid userId)
        {
            // Arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);

            command = TestFixture.Build<CreateFeedbackAttractionCommand>().Create();

            _attractionsMok.Setup(p => p.AsAsyncRead(p => p.AttractionFeedback).FirstOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), It.IsAny<CancellationToken>()))
                            .ReturnsAsync(null as Attraction);

            // Act & Assert
            await AssertThrowNotFound(command);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_Throw_ForbiddenException_When_User_Has_Already_Appreciated_Attraction(CreateFeedbackAttractionCommand command, Guid userId)
        {
            // Arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);

            command = TestFixture.Build<CreateFeedbackAttractionCommand>().Create();

            var attraction = new Attraction
            {
                Id = 1,
                AttractionFeedback = new List<AttractionFeedback>
            {
                new AttractionFeedback { UserId = userId, ValueRating = 4 }
            },
                UserId = userId
            };

            _attractionsMok.Setup(p => p.AsAsyncRead(p => p.AttractionFeedback).FirstOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), It.IsAny<CancellationToken>()))
                            .ReturnsAsync(attraction);

            // Act & Assert
            await AssertThrowForbiddenFound(command);
        }
    }
}
