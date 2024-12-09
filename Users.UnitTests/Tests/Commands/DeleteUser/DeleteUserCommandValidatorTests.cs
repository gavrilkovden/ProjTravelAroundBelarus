using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentAssertions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Handlers.Commands.DeleteUser;
using Users.Application.Handlers.Queries.GetUser;
using Xunit.Abstractions;

namespace Users.UnitTests.Tests.Commands.DeleteUser
{
    public class DeleteUserCommandValidatorTests : ValidatorTestBase<DeleteUserCommand>
    {
        public DeleteUserCommandValidatorTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {

        }
        protected override IValidator<DeleteUserCommand> TestValidator =>
    TestFixture.Create<DeleteUserCommandValidator>();

        [Theory, FixtureAutoData]
        public void Should_BeValid_When_RequestFieldsAreFilledCorrectly(Guid id)
        {
            // arrange
            var command = new DeleteUserCommand
            {
                Id = id.ToString()
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("123")]
        public void Should_NotBeValid_When_IncorrectGuid(string id)
        {
            // arrange
            var command = new DeleteUserCommand
            {
                Id = id
            };

            // act & assert
            AssertNotValid(command);
        }
    }

}
