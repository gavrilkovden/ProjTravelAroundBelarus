using AutoFixture;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Tests;
using Core.Tests.Attributes;
using Core.Tests.Fixtures;
using Core.Users.Domain;
using Core.Users.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;
using Users.Application.Caches;
using Users.Application.Dtos;
using Users.Application.Handlers.Commands.DeleteUser;
using Users.Application.Handlers.Commands.UpdateUser;
using Users.Application.Handlers.Queries.GetUser;
using Xunit.Abstractions;

namespace Users.UnitTests.Tests.Commands.UpdateUser
{
    public class UpdateUserCommandHandlerTest : RequestHandlerTestBase<UpdateUserCommand, GetUserDto>
    {
        private readonly Mock<IBaseWriteRepository<ApplicationUser>> _usersMock = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly ApplicationUsersListMemoryCache _listCache;
        private readonly ApplicationUsersCountMemoryCache _countCache;
        private readonly ApplicationUserMemoryCache _userCache;
        private readonly Mock<ILogger<UpdateUserCommandHandler>> _loggerMock = new();
        private readonly IMapper _mapper;

        public UpdateUserCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(UpdateUserCommand).Assembly).Mapper;
            _listCache = new ApplicationUsersListMemoryCache();
            _countCache = new ApplicationUsersCountMemoryCache();
            _userCache = new ApplicationUserMemoryCache();
        }

        protected override IRequestHandler<UpdateUserCommand, GetUserDto> CommandHandler =>
            new UpdateUserCommandHandler(
                _usersMock.Object,
                _mapper,
                _currentUserServiceMock.Object,
                _listCache,
                _countCache,
                _userCache,
                _loggerMock.Object
            );

        [Theory]
        [FixtureInlineAutoData]
        public async Task Should_UpdateUser_When_Admin(Guid userId)
        {
            var command = new UpdateUserCommand { Id = userId.ToString(), Login = "testlogin" };

            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentUserServiceMock.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(true);

            //var user = TestFixture.Build<ApplicationUser>().Create();
            //user.ApplicationUserId = userId;
            var user = new ApplicationUser { ApplicationUserId = userId, Login = "testlogin" };
            _usersMock.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(
                It.IsAny<Expression<Func<ApplicationUser, bool>>>(), default)).ReturnsAsync(user);

            _usersMock.Setup(p => p.UpdateAsync(It.IsAny<ApplicationUser>(), It.IsAny<CancellationToken>()))
  .ReturnsAsync(user);

            // Act and Assert
            await AssertNotThrow(command);
        }

        [Theory]
        [FixtureInlineAutoData]
        public async Task Should_UpdateUser_When_Self(Guid userId)
        {
            var command = new UpdateUserCommand { Id = userId.ToString(), Login = "testlogin" };

            // Arrange
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            var user = new ApplicationUser { ApplicationUserId = userId, Login = "testlogin" };
            _usersMock.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(
                It.IsAny<Expression<Func<ApplicationUser, bool>>>(), default)).ReturnsAsync(user);

            _usersMock.Setup(p => p.UpdateAsync(It.IsAny<ApplicationUser>(), It.IsAny<CancellationToken>()))
  .ReturnsAsync(user);

            // Act and Assert
            await AssertNotThrow(command);
        }

        [Theory]
        [FixtureInlineAutoData]
        public async Task Should_ThrowForbidden_When_NotAdmin_NotSelf(Guid userId, Guid otherUserId)
        {
            var command = new UpdateUserCommand { Id = otherUserId.ToString() };

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
            var command = new UpdateUserCommand { Id = userId.ToString() };

            // Arrange
            _currentUserServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            _usersMock.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(
                It.IsAny<Expression<Func<ApplicationUser, bool>>>(), default)).ReturnsAsync((ApplicationUser)null);

            // Act and Assert
            await AssertThrow<NotFoundException>(command);
        }
    }
}
