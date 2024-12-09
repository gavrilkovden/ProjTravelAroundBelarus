using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using global::Users.Application.Handlers.Commands.CreateUser;
using Xunit.Abstractions;

namespace Users.UnitTests.Tests.Commands.CreateUser
{
    public class CreateUserCommandValidatorTests : ValidatorTestBase<CreateUserCommand>
    {
        public CreateUserCommandValidatorTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<CreateUserCommand> TestValidator =>
            TestFixture.Create<CreateUserCommandValidator>();

        [Theory, FixtureAutoData]
        public void Should_BeValid_When_RequestFieldsAreFilledCorrectly(string login, string password)
        {
            // arrange
            var command = new CreateUserCommand
            {
                Login = login,
                Password = password
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData(null)]
        public void Should_NotBeValid_When_LoginIsEmptyOrNull(string login)
        {
            // arrange
            var command = new CreateUserCommand
            {
                Login = login,
                Password = "ValidPassword123!"
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("shrt")]
        public void Should_NotBeValid_When_LoginIsTooShort(string login)
        {
            // arrange
            var command = new CreateUserCommand
            {
                Login = login,
                Password = "123!"
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData(null)]
        public void Should_NotBeValid_When_PasswordIsEmptyOrNull(string password)
        {
            // arrange
            var command = new CreateUserCommand
            {
                Login = "ValidLogin",
                Password = password
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("short1")]
        public void Should_NotBeValid_When_PasswordIsTooShort(string password)
        {
            // arrange
            var command = new CreateUserCommand
            {
                Login = "ValidLogin",
                Password = password
            };

            // act & assert
            AssertNotValid(command);
        }
    }
}
