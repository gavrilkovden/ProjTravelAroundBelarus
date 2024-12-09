using Attractions.Application.Handlers.Attractions.Queries.GetAttractions;
using Core.Tests;
using FluentValidation;
using Xunit.Abstractions;

namespace Travel.UnitTests.Tests.Attractions.Queries.GetAttractions
{
    public class GetAttractionsQueryValidatorTest : ValidatorTestBase<GetAttractionsQuery>
    {
        public GetAttractionsQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<GetAttractionsQuery> TestValidator =>
            new GetAttractionsQueryValidator();

        [Fact]
        public void Should_BeValid_When_ValidPaginationFilter()
        {
            // arrange
            var query = new GetAttractionsQuery
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
        public void Should_NotBeValid_When_InvalidPaginationFilter(int? limit)
        {
            // arrange
            var query = new GetAttractionsQuery
            {
                Limit = limit,
                Offset = 1
            };

            // act & assert
            AssertNotValid(query);
        }
    }
}
