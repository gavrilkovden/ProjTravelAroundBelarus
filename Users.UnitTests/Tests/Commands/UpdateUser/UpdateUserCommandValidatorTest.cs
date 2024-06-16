using Core.Tests.Attributes;
using Core.Tests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Handlers.Commands.CreateUser;
using Xunit.Abstractions;
using Users.Application.Handlers.Commands.UpdateUser;
using AutoFixture;
using AutoFixture.Xunit2;

namespace Users.UnitTests.Tests.Commands.UpdateUser
{

    public class UpdateUserCommandValidatorTest : ValidatorTestBase<UpdateUserCommand>
    {
        public UpdateUserCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<UpdateUserCommand> TestValidator =>
            TestFixture.Create<UpdateUserCommandValidator>();

        [Theory, FixtureAutoData]
        public void Should_BeValid_When_RequestFieldsAreFilledCorrectly(Guid userId, string login)
        {
            // arrange
            var command = new UpdateUserCommand
            {
                Id = userId.ToString(),
                Login = login
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData(null)]
        public void Should_NotBeValid_When_IdIsEmptyOrNull(string id)
        {
            // arrange
            var command = new UpdateUserCommand
            {
                Id = id,
                Login = "ValidLogin"
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [InlineAutoData("invalid-guid")]
        public void Should_NotBeValid_When_IdIsNotValidGuid(string id)
        {
            // arrange
            var command = new UpdateUserCommand
            {
                Id = id,
                Login = "ValidLogin"
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData(null)]
        public void Should_NotBeValid_When_LoginIsEmptyOrNull(string login)
        {
            // arrange
            var command = new UpdateUserCommand
            {
                Id = Guid.NewGuid().ToString(),
                Login = login
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("ab")]
        public void Should_NotBeValid_When_LoginIsTooShort(string login)
        {
            // arrange
            var command = new UpdateUserCommand
            {
                Id = Guid.NewGuid().ToString(),
                Login = login
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("thisloginiswaytoolongthisloginiswaytoolongthisloginiswaytoolong")]
        public void Should_NotBeValid_When_LoginIsTooLong(string login)
        {
            // arrange
            var command = new UpdateUserCommand
            {
                Id = Guid.NewGuid().ToString(),
                Login = login
            };

            // act & assert
            AssertNotValid(command);
        }
    }
}
