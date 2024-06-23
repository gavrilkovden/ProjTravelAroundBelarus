using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests.Attributes;
using Core.Tests;
using MediatR;
using Moq;
using Travels.Domain;
using Xunit.Abstractions;
using Attractions.Application.Handlers.AttractionFeedbacks.Commands.DeleteFeedbackAttraction;
using Travel.Application.Caches.AttractionFeedback;
using Core.Users.Domain.Enums;
using AutoFixture;
using System.Linq.Expressions;

namespace Travel.UnitTests.Tests.AttractionFeedbacks.Command.DeleteFeedbackAttraction
{
    public class DeleteFeedbackAttractionCommandHandleresTest : RequestHandlerTestBase<DeleteFeedbackAttractionCommand>
    {
        private readonly Mock<IBaseWriteRepository<AttractionFeedback>> _attractionFeedbacksMock = new();
        private readonly Mock<ICurrentUserService> _currentServiceMock = new();
        private readonly Mock<ICleanAttractionFeedbacksCacheService> _cleanAttractionFeedbacksCacheServiceMock = new();

        public DeleteFeedbackAttractionCommandHandleresTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IRequestHandler<DeleteFeedbackAttractionCommand> CommandHandler =>
            new DeleteFeedbackAttractionCommandHandler(_attractionFeedbacksMock.Object, _currentServiceMock.Object, _cleanAttractionFeedbacksCacheServiceMock.Object);

        [Theory, FixtureInlineAutoData]
        public async Task Should_Delete_AttractionFeedback_When_Admin_Deletes(DeleteFeedbackAttractionCommand command, Guid userId)
        {
            // arrange
            _currentServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentServiceMock.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(true);

            var attractionFeedback = TestFixture.Build<AttractionFeedback>().Create();
            _attractionFeedbacksMock.Setup(
                p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<AttractionFeedback, bool>>>(), default)
            ).ReturnsAsync(attractionFeedback);

            // act and assert
            await AssertNotThrow(command);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_ThrowForbidden_When_Not_Admin_Deletes(DeleteFeedbackAttractionCommand command, Guid userId)
        {
            // arrange
            _currentServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentServiceMock.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(false);

            var attractionFeedback = TestFixture.Build<AttractionFeedback>().Create();
            _attractionFeedbacksMock.Setup(
                p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<AttractionFeedback, bool>>>(), default)
            ).ReturnsAsync(attractionFeedback);

            // act and assert
            await AssertThrowForbiddenFound(command);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_ThrowNotFound_When_Attraction_Not_Found(DeleteFeedbackAttractionCommand command)
        {
            // arrange
            _attractionFeedbacksMock.Setup(
                p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<AttractionFeedback, bool>>>(), default)
            ).ReturnsAsync(null as AttractionFeedback);

            // act and assert
            await AssertThrowNotFound(command);
        }
    }
}
