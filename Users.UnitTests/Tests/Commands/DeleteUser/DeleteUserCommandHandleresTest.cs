using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Tests.Attributes;
using Core.Tests;
using Core.Users.Domain;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Caches;
using Users.Application.Handlers.Commands.DeleteUser;
using Xunit.Abstractions;
using Core.Users.Domain.Enums;
using System.Linq.Expressions;
using Users.Application.Handlers.Queries.GetUser;
using AutoFixture;
using AutoMapper;
using Core.Tests.Fixtures;
using AutoFixture.Xunit2;

namespace Users.UnitTests.Tests.Commands.DeleteUser
{
    public class DeleteUserCommandHandlerTest : RequestHandlerTestBase<DeleteUserCommand>
    {
        private readonly Mock<IBaseWriteRepository<ApplicationUser>> _usersMock = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly ApplicationUsersListMemoryCache _listCache = new();
        private readonly ApplicationUsersCountMemoryCache _countCache = new();
        private readonly Mock<ILogger<DeleteUserCommandHandler>> _loggerMock = new();
        private readonly ApplicationUserMemoryCache _userCache = new();

        public DeleteUserCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        protected override IRequestHandler<DeleteUserCommand> CommandHandler =>
            new DeleteUserCommandHandler(
                _usersMock.Object,
                _currentUserServiceMock.Object,
                _listCache,
                _countCache,
                _loggerMock.Object,
                _userCache
            );

        [Theory]
        [FixtureInlineAutoData]
        public async Task Should_DeleteUser_When_Admin(Guid userId)
        {
            var command = new DeleteUserCommand { Id = userId.ToString() };

            // Arrange
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentUserServiceMock.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(true);

            var user = new ApplicationUser { ApplicationUserId = userId };
            _usersMock.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(
                It.IsAny<Expression<Func<ApplicationUser, bool>>>(), default)).ReturnsAsync(user);

            // Act and Assert
            await AssertNotThrow(command);
        }

        [Theory]
        [FixtureInlineAutoData]
        public async Task Should_DeleteUser_When_Self(Guid userId)
        {
            var command = new DeleteUserCommand { Id = userId.ToString() };

            // Arrange
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            var user = new ApplicationUser { ApplicationUserId = userId };
            _usersMock.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(
                It.IsAny<Expression<Func<ApplicationUser, bool>>>(), default)).ReturnsAsync(user);

            // Act and Assert
            await AssertNotThrow(command);
        }

        [Theory]
        [FixtureInlineAutoData]
        public async Task Should_ThrowForbidden_When_NotAdmin_NotSelf(Guid userId, Guid otherUserId)
        {
            var command = new DeleteUserCommand { Id = otherUserId.ToString() };

            // Arrange
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            var user = new ApplicationUser { ApplicationUserId = otherUserId };
            _usersMock.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(
                It.IsAny<Expression<Func<ApplicationUser, bool>>>(), default)).ReturnsAsync(user);

            // Act and Assert
            await AssertThrow<ForbiddenException>(command);
        }

        [Theory]
        [FixtureInlineAutoData]
        public async Task Should_ThrowNotFound_When_UserNotFound(Guid userId)
        {
            var command = new DeleteUserCommand { Id = userId.ToString() };

            // Arrange
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            _usersMock.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(
                It.IsAny<Expression<Func<ApplicationUser, bool>>>(), default)).ReturnsAsync((ApplicationUser)null);

            // Act and Assert
            await AssertThrow<NotFoundException>(command);
        }
    }

}
