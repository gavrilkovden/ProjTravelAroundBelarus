using Core.Tests.Attributes;
using Core.Tests;
using FluentValidation;
using Xunit.Abstractions;
using AutoFixture;
using Routes.Application.Handlers.Commands.UpdateRoute;

namespace Travel.UnitTests.Tests.Routes.Commands.UpdateRoute
{
    public class UpdateRouteCommandValidatorTest : ValidatorTestBase<UpdateRouteCommand>
    {
        public UpdateRouteCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<UpdateRouteCommand> TestValidator =>
            TestFixture.Create<UpdateRouteCommandValidator>();

        [Theory, FixtureAutoData]
        public void Should_BeValid_When_RequestFieldsAreFilledCorrectly(UpdateRouteCommand command)
        {
            // arrange
            command.Id = 1;
            command.Name = "test";
            command.Description = "test";

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData(" ")]
        [FixtureInlineAutoData("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
        public void Should_NotBeValid_When_Name_IsInvalid(string name, UpdateRouteCommand command)
        {
            command.Name = name;
            command.Description = "test";

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(0)]
        [FixtureInlineAutoData(-1)]
        public void Should_NotBeValid_When_Id_IsInvalid(int id, UpdateRouteCommand command)
        {
            command.Id = id;
            command.Name = "test";
            command.Description = "test";

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData(" ")]
        [FixtureInlineAutoData("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890" +
            "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890" +
            "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890" +
            "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890" +
            "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890" +
            "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
        public void Should_NotBeValid_When_Description_IsInvalid(string name, UpdateRouteCommand command)
        {
            command.Name = name;
            command.Description = "test";

            // act & assert
            AssertNotValid(command);
        }
    }
}
