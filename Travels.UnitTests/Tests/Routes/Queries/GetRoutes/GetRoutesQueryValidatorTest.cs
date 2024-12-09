using Core.Tests;
using FluentValidation;
using Routes.Application.Handlers.Queries.GetRoutes;
using Xunit.Abstractions;

namespace Travel.UnitTests.Tests.Routes.Queries.GetRoutes
{
    public class GetRoutesQueryValidatorTest : ValidatorTestBase<GetRoutesQuery>
    {
        public GetRoutesQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<GetRoutesQuery> TestValidator =>
            new GetRoutesQueryValidator();

        [Fact]
        public void Should_BeValid_When_ValidListRoutesFilter()
        {
            // arrange
            var query = new GetRoutesQuery
            {
                Limit = 10,
                Offset = 1
            };

            // act & assert
            AssertValid(query);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(21)]
        public void Should_NotBeValid_When_InValidListRoutesFilter(int? limit)
        {
            // arrange
            var query = new GetRoutesQuery
            {
                Limit = limit,
                Offset = 1
            };

            // act & assert
            AssertNotValid(query);
        }

        [Theory]
        [InlineData("123456789012345678901234567890123456789012345678901234567890")]

        public void Should_NotBeValid_When_InvalidPaginationFilter(string? freeText)
        {
            // arrange
            var query = new GetRoutesQuery
            {
                FreeText = freeText
            };

            // act & assert
            AssertNotValid(query);
        }
    }
}
