using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Tests;
using Core.Tests.Attributes;
using Core.Users.Domain;
using Core.Users.Domain.Enums;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;
using Users.Application.Handlers.Commands.UpdateUserPassword;
using Xunit.Abstractions;

namespace Users.UnitTests.Tests.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordCommandHandlerTest : RequestHandlerTestBase<UpdateUserPasswordCommand>
    {
        private readonly Mock<IBaseWriteRepository<ApplicationUser>> _usersMock = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly Mock<ILogger<UpdateUserPasswordCommandHandler>> _loggerMock = new();

        public UpdateUserPasswordCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IRequestHandler<UpdateUserPasswordCommand> CommandHandler =>
            new UpdateUserPasswordCommandHandler(
                _usersMock.Object,
                _currentUserServiceMock.Object,
                _loggerMock.Object
            );

        [Theory, FixtureInlineAutoData]
        public async Task Should_UpdateUserPassword_When_Admin(Guid userId, string newPassword)
        {
            // Arrange
            var command = new UpdateUserPasswordCommand { UserId = userId.ToString(), Password = newPassword };
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentUserServiceMock.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(true);

            var user = new ApplicationUser { ApplicationUserId = userId };
            _usersMock.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(
                It.IsAny<Expression<Func<ApplicationUser, bool>>>(), default))
                .ReturnsAsync(user);

            var handler = new UpdateUserPasswordCommandHandler(_usersMock.Object, _currentUserServiceMock.Object, _loggerMock.Object);

            // Act
            Func<Task> act = async () => await handler.Handle(command, default);

            // Assert
            await act.Should().NotThrowAsync<Exception>();
            user.PasswordHash.Should().NotBeNullOrEmpty();
            _usersMock.Verify(u => u.UpdateAsync(user, default), Times.Once);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_ThrowNotFoundException_When_UserNotFound(Guid userId, string newPassword)
        {
            // Arrange
            var command = new UpdateUserPasswordCommand { UserId = userId.ToString(), Password = newPassword };
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentUserServiceMock.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(true);

            _usersMock.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(
                It.IsAny<Expression<Func<ApplicationUser, bool>>>(), default))
                .ReturnsAsync((ApplicationUser)null);

            var handler = new UpdateUserPasswordCommandHandler(_usersMock.Object, _currentUserServiceMock.Object, _loggerMock.Object);

            // Act
            Func<Task> act = async () => await handler.Handle(command, default);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
            _usersMock.Verify(u => u.UpdateAsync(It.IsAny<ApplicationUser>(), default), Times.Never);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_ThrowForbiddenException_When_NotAdmin(Guid userId, string newPassword)
        {
            // Arrange
            var command = new UpdateUserPasswordCommand { UserId = userId.ToString(), Password = newPassword };
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(Guid.NewGuid()); 
            _currentUserServiceMock.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(false);

            var user = new ApplicationUser { ApplicationUserId = userId };
            _usersMock.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(
                It.IsAny<Expression<Func<ApplicationUser, bool>>>(), default))
                .ReturnsAsync(user);

            var handler = new UpdateUserPasswordCommandHandler(_usersMock.Object, _currentUserServiceMock.Object, _loggerMock.Object);

            // Act
            Func<Task> act = async () => await handler.Handle(command, default);

            // Assert
            await act.Should().ThrowAsync<ForbiddenException>();
            _usersMock.Verify(u => u.UpdateAsync(It.IsAny<ApplicationUser>(), default), Times.Never);
        }
    }
}
