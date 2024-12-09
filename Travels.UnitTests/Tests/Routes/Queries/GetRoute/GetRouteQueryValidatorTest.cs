using Core.Tests.Attributes;
using Core.Tests;
using FluentValidation;
using Xunit.Abstractions;
using Routes.Application.Handlers.Queries.GetRoute;

namespace Travel.UnitTests.Tests.Routes.Queries.GetRoute
{
    public class GetRouteQueryValidatorTest : ValidatorTestBase<GetRouteQuery>
    {
        public GetRouteQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<GetRouteQuery> TestValidator =>
            new GetRouteQueryValidator();

        [Theory]
        [FixtureInlineAutoData(1)]
        [FixtureInlineAutoData(10)]
        [FixtureInlineAutoData(20)]
        public void Should_BeValid_When_Id_Is_Valid(int id)
        {
            // arrange
            var query = new GetRouteQuery
            {
                Id = id
            };

            // act & assert
            AssertValid(query);
        }

        [Theory]
        [FixtureInlineAutoData(0)]
        [FixtureInlineAutoData(-1)]
        [FixtureInlineAutoData(null)]
        public void Should_NotBeValid_When_Id_Is_Invalid(int id)
        {
            // arrange
            var query = new GetRouteQuery
            {
                Id = id
            };

            // act & assert
            AssertNotValid(query);
        }
    }
}
