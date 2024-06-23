using Core.Tests.Attributes;
using Core.Tests;
using FluentValidation;
using Xunit.Abstractions;
using Routes.Application.Handlers.Commands.DeleteRoute;

namespace Travel.UnitTests.Tests.Routes.Commands.DeleteRoute
{
    public class DeleteRouteCommandValidatorTest : ValidatorTestBase<DeleteRouteCommand>
    {
        public DeleteRouteCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<DeleteRouteCommand> TestValidator =>
            new DeleteRouteCommandValidator();

        [Theory]
        [FixtureInlineAutoData(1)]
        [FixtureInlineAutoData(10)]
        [FixtureInlineAutoData(20)]
        public void Should_BeValid_When_Id_Is_Valid(int id)
        {
            // arrange
            var command = new DeleteRouteCommand
            {
                Id = id
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(0)]
        [FixtureInlineAutoData(-1)]
        [FixtureInlineAutoData(null)]
        public void Should_NotBeValid_When_Id_Is_Invalid(int id)
        {
            // arrange
            var command = new DeleteRouteCommand
            {
                Id = id
            };

            // act & assert
            AssertNotValid(command);
        }
    }
}
