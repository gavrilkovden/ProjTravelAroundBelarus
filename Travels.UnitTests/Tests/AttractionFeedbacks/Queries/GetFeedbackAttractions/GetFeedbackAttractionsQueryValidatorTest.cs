using Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttractions;
using Core.Tests;
using FluentValidation;
using Routes.Application.Handlers.Queries.GetRoutes;
using Xunit.Abstractions;

namespace Travel.UnitTests.Tests.AttractionFeedbacks.Queries.GetFeedbackAttractions
{
    public class GetFeedbackAttractionsQueryValidatorTest : ValidatorTestBase<GetFeedbackAttractionsQuery>
    {
        public GetFeedbackAttractionsQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<GetFeedbackAttractionsQuery> TestValidator =>
            new GetFeedbackAttractionsQueryValidator();

        [Fact]
        public void Should_BeValid_When_ValidListFeedbackAttractionsFilter()
        {
            // arrange
            var query = new GetFeedbackAttractionsQuery
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
        public void Should_NotBeValid_When_InValidListFeedbackAttractionsFilter(int? limit)
        {
            // arrange
            var query = new GetFeedbackAttractionsQuery
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
            var query = new GetFeedbackAttractionsQuery
            {
                FreeText = freeText
            };

            // act & assert
            AssertNotValid(query);
        }
    }



  
}
