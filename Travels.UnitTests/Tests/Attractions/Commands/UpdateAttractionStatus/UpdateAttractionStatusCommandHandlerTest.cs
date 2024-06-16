using Attractions.Application.Caches.AttractionCaches;
using Attractions.Application.Handlers.Attractions.Commands.UpdateAttractionStatus;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests.Attributes;
using Core.Tests;
using MediatR;
using Moq;
using Travel.Application.Dtos;
using Travels.Domain;
using Xunit.Abstractions;
using Core.Users.Domain.Enums;
using System.Linq.Expressions;

namespace Travel.UnitTests.Tests.Attractions.Commands.UpdateAttractionStatus
{
    public class UpdateAttractionStatusHandlerTest : RequestHandlerTestBase<UpdateAttractionStatusCommand, GetAttractionDto>
    {
        private readonly Mock<IBaseWriteRepository<Attraction>> _attractionsMock = new Mock<IBaseWriteRepository<Attraction>>();
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new Mock<ICurrentUserService>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<ICleanAttractionsCacheService> _cleanAttractionsCacheServiceMock = new Mock<ICleanAttractionsCacheService>();

        public UpdateAttractionStatusHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IRequestHandler<UpdateAttractionStatusCommand, GetAttractionDto> CommandHandler =>
            new UpdateAttractionStatusCommandHandler(
                _attractionsMock.Object,
                _currentUserServiceMock.Object,
                _mapperMock.Object,
                _cleanAttractionsCacheServiceMock.Object
            );

        [Theory, FixtureInlineAutoData]
        public async Task Should_Update_Attraction_Status_When_Admin_Updates(
            UpdateAttractionStatusCommand command,
            Guid userId,
            Attraction attractionEntity,
            GetAttractionDto attractionDto
        )
        {
            // Arrange
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentUserServiceMock.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(true);

            _attractionsMock.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), default))
                .ReturnsAsync(attractionEntity);

            _mapperMock.Setup(m => m.Map<Attraction>(command)).Returns(attractionEntity);
            _mapperMock.Setup(m => m.Map<GetAttractionDto>(attractionEntity)).Returns(attractionDto);

            _attractionsMock.Setup(p => p.UpdateAsync(attractionEntity, default)).ReturnsAsync(attractionEntity);

            // act & assert
            await AssertNotThrow(command);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_Throw_Forbidden_When_Owner_Updates(
            UpdateAttractionStatusCommand command,
            Guid userId,
            Attraction attractionEntity,
            GetAttractionDto attractionDto
        )
        {
            // Arrange
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentUserServiceMock.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(false);
            _currentUserServiceMock.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Client)).Returns(true);

            attractionEntity.UserId = userId;

            _attractionsMock.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), default))
                .ReturnsAsync(attractionEntity);

            _mapperMock.Setup(m => m.Map<Attraction>(command)).Returns(attractionEntity);
            _mapperMock.Setup(m => m.Map<GetAttractionDto>(attractionEntity)).Returns(attractionDto);

            _attractionsMock.Setup(p => p.UpdateAsync(attractionEntity, default)).ReturnsAsync(attractionEntity);

            // act & assert
            await AssertThrowForbiddenFound(command);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_Throw_NotFound_When_Attraction_NotFound(
            UpdateAttractionStatusCommand command,
            Guid userId
        )
        {
            // Arrange
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentUserServiceMock.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(true);

            _attractionsMock.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), default))
                .ReturnsAsync((Attraction)null);

            // Act & Assert
            await AssertThrowNotFound(command);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_Throw_Forbidden_When_User_NotAdmin_Or_Owner(
            UpdateAttractionStatusCommand command,
            Guid userId,
            Attraction attractionEntity
        )
        {
            // Arrange
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentUserServiceMock.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(false);
            _currentUserServiceMock.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Client)).Returns(false);

            attractionEntity.UserId = Guid.NewGuid();

            _attractionsMock.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), default))
                .ReturnsAsync(attractionEntity);

            // Act & Assert
            await AssertThrowForbiddenFound(command);
        }
    }
}
