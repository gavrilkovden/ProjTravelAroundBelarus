using Core.Tests.Attributes;
using Core.Tests;
using FluentValidation;
using Xunit.Abstractions;
using Routes.Application.Handlers.Commands.CreateAttractionInRoute;
using AutoFixture;

namespace Travel.UnitTests.Tests.Routes.Commands.CreateAttractionInRoute
{
    public class CreateAttractionInRouteCommandValidatorTest : ValidatorTestBase<CreateAttractionInRouteCommand>
    {
        public CreateAttractionInRouteCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<CreateAttractionInRouteCommand> TestValidator =>
            TestFixture.Create<CreateAttractionInRouteCommandValidator>();

        [Theory, FixtureAutoData]
        public void Should_BeValid_When_RequestFieldsAreFilledCorrectly(CreateAttractionInRouteCommand command)
        {
            // arrange
            command.Order = 1;
            command.RouteId = 1;
            command.VisitDateTime = DateTime.UtcNow.AddDays(1);
            command.DistanceToNextAttraction = 1;
            command.AttractionId = 1;

            // act & assert
            AssertValid(command);
        }


        [Theory]
        [FixtureInlineAutoData(0)]
        [FixtureInlineAutoData(-1)]
        public void Should_NotBeValid_When_Order_IsInvalid(int order, CreateAttractionInRouteCommand command)
        {
            command.Order = order;
            command.RouteId = 1;
            command.VisitDateTime = DateTime.UtcNow.AddDays(1);
            command.DistanceToNextAttraction = 1;
            command.AttractionId = 1;

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(0)]
        [FixtureInlineAutoData(-1)]
        public void Should_NotBeValid_When_RouteId_IsInvalid(int routeId, CreateAttractionInRouteCommand command)
        {
            command.Order = 1;
            command.RouteId = routeId;
            command.VisitDateTime = DateTime.UtcNow.AddDays(1);
            command.DistanceToNextAttraction = 1;
            command.AttractionId = 1;

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(-1)]
        public void Should_NotBeValid_When_DistanceToNextAttraction_IsInvalid(int distanceToNextAttraction, CreateAttractionInRouteCommand command)
        {
            command.Order = 1;
            command.RouteId = 1;
            command.VisitDateTime = DateTime.UtcNow.AddDays(1);
            command.DistanceToNextAttraction = distanceToNextAttraction;
            command.AttractionId = 1;

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(0)]
        [FixtureInlineAutoData(-1)]
        public void Should_NotBeValid_When_AttractionId_IsInvalid(int attractionId, CreateAttractionInRouteCommand command)
        {
            command.Order = 1;
            command.RouteId = 1;
            command.VisitDateTime = DateTime.UtcNow.AddDays(1);
            command.DistanceToNextAttraction = 1;
            command.AttractionId = attractionId;

            // act & assert
            AssertNotValid(command);
        }

        [Theory, FixtureAutoData]
        public void Should_NotBeValid_When_VisitDateTime_IsInvalid( CreateAttractionInRouteCommand command)
        {
            command.Order = 1;
            command.RouteId = 1;
            command.VisitDateTime = DateTime.UtcNow.AddDays(-1);
            command.DistanceToNextAttraction = 1;
            command.AttractionId = 1;

            // act & assert
            AssertNotValid(command);
        }
    }
}
