using Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttractionsCount;
using Core.Tests;
using FluentValidation;
using Xunit.Abstractions;

namespace Travel.UnitTests.Tests.AttractionFeedbacks.Queries.GetFeedbackAttractionsCount
{
    public class GetFeedbackAttractionsCountQueryValidatorTest : ValidatorTestBase<GetFeedbackAttractionsCountQuery>
    {
        public GetFeedbackAttractionsCountQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<GetFeedbackAttractionsCountQuery> TestValidator =>
            new GetFeedbackAttractionsCountQueryValidator();

        [Fact]
        public void Should_BeValid_When_ValidPaginationFilter()
        {
            // arrange
            var query = new GetFeedbackAttractionsCountQuery
            {
                FreeText = "test"
            };

            // act & assert
            AssertValid(query);
        }

        [Theory]
        [InlineData("123456789012345678901234567890123456789012345678901234567890")]

        public void Should_NotBeValid_When_InvalidPaginationFilter(string? freeText)
        {
            // arrange
            var query = new GetFeedbackAttractionsCountQuery
            {
                FreeText = freeText
            };

            // act & assert
            AssertNotValid(query);
        }
    }
}
