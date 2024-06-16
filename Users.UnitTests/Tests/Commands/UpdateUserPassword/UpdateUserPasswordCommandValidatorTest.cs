using Core.Tests.Attributes;
using Core.Tests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Handlers.Commands.UpdateUserPassword;
using Xunit.Abstractions;

namespace Users.UnitTests.Tests.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordCommandValidatorTests : ValidatorTestBase<UpdateUserPasswordCommand>
    {
        public UpdateUserPasswordCommandValidatorTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<UpdateUserPasswordCommand> TestValidator =>
            new UpdateUserPasswordCommandValidator();

        [Theory]
        [FixtureInlineAutoData("ValidPassword123!")]
        public void Should_BeValid_When_PasswordIsValid(string password)
        {
            // Arrange
            var command = new UpdateUserPasswordCommand
            {
                Password = password
            };

            // Act & Assert
            AssertValid(command);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_NotBeValid_When_PasswordIsEmptyOrNull(string password)
        {
            // Arrange
            var command = new UpdateUserPasswordCommand
            {
                Password = password
            };

            // Act & Assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("short1")] // less than 8 characters
        [FixtureInlineAutoData("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")] // more than 100 characters
        public void Should_NotBeValid_When_PasswordDoesNotMeetLengthRequirements(string password)
        {
            // Arrange
            var command = new UpdateUserPasswordCommand
            {
                Password = password
            };

            // Act & Assert
            AssertNotValid(command);
        }
    }

}
