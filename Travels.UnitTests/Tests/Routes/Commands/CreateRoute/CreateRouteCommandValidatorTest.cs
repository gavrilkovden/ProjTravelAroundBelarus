using Core.Tests.Attributes;
using Core.Tests;
using FluentValidation;
using Xunit.Abstractions;
using AutoFixture;
using Routes.Application.Handlers.Commands.CreateRoute;

namespace Travel.UnitTests.Tests.Routes.Commands.CreateRoute
{
    public class CreateRouteCommandValidatorTest : ValidatorTestBase<CreateRouteCommand>
    {
        public CreateRouteCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<CreateRouteCommand> TestValidator =>
            TestFixture.Create<CreateRouteCommandValidator>();

        [Theory, FixtureAutoData]
        public void Should_BeValid_When_RequestFieldsAreFilledCorrectly(CreateRouteCommand command)
        {
            // arrange
            command.Name = "test";
            command.Description = "test";

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData(" ")]
        [FixtureInlineAutoData("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
        public void Should_NotBeValid_When_Name_IsInvalid(string name, CreateRouteCommand command)
        {
            command.Name = name;
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
        public void Should_NotBeValid_When_Description_IsInvalid(string name, CreateRouteCommand command)
        {
            command.Name = name;
            command.Description = "test";

            // act & assert
            AssertNotValid(command);
        }
    }
}
