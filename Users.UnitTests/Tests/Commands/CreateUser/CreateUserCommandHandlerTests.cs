using AutoFixture;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Tests;
using Core.Tests.Attributes;
using Core.Users.Domain;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Linq.Expressions;
using Users.Application.Caches;
using Users.Application.Dtos;
using Users.Application.Handlers.Commands.CreateUser;
using Xunit.Abstractions;

namespace Users.UnitTests.Tests.Commands.CreateUser
{
    public class CreateUserCommandHandlerTests : RequestHandlerTestBase<CreateUserCommand, GetUserDto>
    {
        private readonly Mock<IBaseWriteRepository<ApplicationUser>> _usersMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<ApplicationUsersListMemoryCache> _listCacheMock = new();
        private readonly Mock<ApplicationUsersCountMemoryCache> _countCacheMock = new();

        protected override IRequestHandler<CreateUserCommand, GetUserDto> CommandHandler =>
            new CreateUserCommandHandler(
                _usersMock.Object,
                _mapperMock.Object,
                _listCacheMock.Object,
                NullLogger<CreateUserCommandHandler>.Instance,
                _countCacheMock.Object
            );

        public CreateUserCommandHandlerTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_CreateUser_When_CommandIsValid(CreateUserCommand command, ApplicationUser user, GetUserDto userDto)
        {
            // arrange
            _usersMock
                .Setup(r => r.AsAsyncRead().AnyAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _usersMock
                .Setup(r => r.AddAsync(It.IsAny<ApplicationUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _mapperMock
                .Setup(m => m.Map<GetUserDto>(It.IsAny<ApplicationUser>()))
                .Returns(userDto);

            // act
            var result = await CommandHandler.Handle(command, default);

            // assert
            result.Should().BeEquivalentTo(userDto);

            _usersMock.Verify(r => r.AddAsync(It.IsAny<ApplicationUser>(), It.IsAny<CancellationToken>()), Times.Once);

        }

        [Fact]
        public async Task Should_ThrowBadOperationException_When_UserAlreadyExists()
        {
            // arrange
            var command = TestFixture.Create<CreateUserCommand>();

            _usersMock
                .Setup(r => r.AsAsyncRead().AnyAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // act
            var action = () => CommandHandler.Handle(command, default);

            // assert
            await action.Should().ThrowAsync<BadOperationException>()
                .WithMessage($"User with login {command.Login} already exists.");

            _usersMock.Verify(r => r.AddAsync(It.IsAny<ApplicationUser>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
