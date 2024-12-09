using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests.Attributes;
using Core.Tests.Fixtures;
using Core.Tests;
using MediatR;
using Moq;
using Travels.Domain;
using Xunit.Abstractions;
using AutoFixture;
using System.Linq.Expressions;
using Tours.Application.Dtos;
using Tours.Application.Handlers.TourFeedbacks.Commands.CreateFeedbackTour;
using Tours.Application.Caches.TourFeedbackCaches;
using Tours.Application.Caches.TourCaches;

namespace Travel.UnitTests.Tests.TourFeedbacks.Commands.CreateFeedbackTour
{
    public class CreateFeedbackTourCommandHandlerTest : RequestHandlerTestBase<CreateFeedbackTourCommand, GetFeedbackTourDto>
    {
        private readonly Mock<IBaseWriteRepository<TourFeedback>> _tourFeedbacksMok = new();
        private readonly Mock<IBaseWriteRepository<Tour>> _toursMok = new();
        private readonly Mock<ICurrentUserService> _currentServiceMok = new();
        private readonly ICleanTourFeedbacksCacheService _cleanTourFeedbacksCacheService;
        private readonly ICleanToursCacheService _cleanToursCacheService;
        private readonly IMapper _mapper;

        public CreateFeedbackTourCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(CreateFeedbackTourCommand).Assembly).Mapper;
            _cleanToursCacheService = new CleanToursCacheService(
                new Mock<TourMemoryCache>().Object,
                new Mock<ToursListMemoryCache>().Object,
                new Mock<ToursCountMemoryCache>().Object);
            _cleanTourFeedbacksCacheService = new CleanTourFeedbacksCacheService(
                new Mock<TourFeedbackMemoryCache>().Object,
                new Mock<TourFeedbacksListMemoryCache>().Object,
                new Mock<TourFeedbacksCountMemoryCache>().Object);
        }

        protected override IRequestHandler<CreateFeedbackTourCommand, GetFeedbackTourDto> CommandHandler =>
            new CreateFeedbackTourCommandHandler(_toursMok.Object, _tourFeedbacksMok.Object, _currentServiceMok.Object, _mapper,
                _cleanTourFeedbacksCacheService, _cleanToursCacheService);

        [Theory, FixtureInlineAutoData]
        public async Task Should_Create_FeedbackTour_Successfully(CreateFeedbackTourCommand command, Guid userId)
        {
            // arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);

            var tour = TestFixture.Build<Tour>().Create();
            var tourFeedback = TestFixture.Build<TourFeedback>().Create();

            _toursMok.Setup(p => p.AsAsyncRead(p => p.TourFeedback).SingleOrDefaultAsync(It.IsAny<Expression<Func<Tour, bool>>>(), It.IsAny<CancellationToken>()))
                            .ReturnsAsync(tour);

            _tourFeedbacksMok.Setup(p => p.AddAsync(It.IsAny<TourFeedback>(), It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(tourFeedback);

            // act and assert
            await AssertNotThrow(command);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_Throw_NotFoundException_When_Tour_Not_Found(CreateFeedbackTourCommand command, Guid userId)
        {
            // Arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);

            _toursMok.Setup(p => p.AsAsyncRead(p => p.TourFeedback).SingleOrDefaultAsync(It.IsAny<Expression<Func<Tour, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(null as Tour);

            // Act & Assert
            await AssertThrowNotFound(command);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_Throw_ForbiddenException_When_Owner_have_already_appreciated_this_tour(CreateFeedbackTourCommand command, Guid userId)
        {
            // Arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);

            var tour = TestFixture.Build<Tour>().Create();
            var tourFeedback = TestFixture.Build<TourFeedback>().Create();

            _toursMok.Setup(p => p.AsAsyncRead(p => p.TourFeedback).SingleOrDefaultAsync(It.IsAny<Expression<Func<Tour, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(tour);
            tour.TourFeedback = new List <TourFeedback> { tourFeedback };
            tourFeedback.UserId = userId;

            // Act & Assert
            await AssertThrowForbiddenFound(command);
        }
    }
}
