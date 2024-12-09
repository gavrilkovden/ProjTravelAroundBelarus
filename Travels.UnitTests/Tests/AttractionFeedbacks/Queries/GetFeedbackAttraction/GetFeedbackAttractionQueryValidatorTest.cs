using Core.Tests.Attributes;
using Core.Tests;
using FluentValidation;
using Xunit.Abstractions;
using Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttraction;
using Travel.Application.Handlers.Attractions.Queries.GetFeedbackAttraction;

namespace Travel.UnitTests.Tests.AttractionFeedbacks.Queries.GetFeedbackAttraction
{
    public class GetFeedbackAttractionQueryValidatorTest : ValidatorTestBase<GetFeedbackAttractionQuery>
    {
        public GetFeedbackAttractionQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<GetFeedbackAttractionQuery> TestValidator =>
            new GetFeedbackAttractionQueryValidator();

        [Theory]
        [FixtureInlineAutoData(1)]
        [FixtureInlineAutoData(10)]
        [FixtureInlineAutoData(20)]
        public void Should_BeValid_When_Id_Is_Valid(int id)
        {
            // arrange
            var query = new GetFeedbackAttractionQuery
            {
                Id = id
            };

            // act & assert
            AssertValid(query);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [FixtureInlineAutoData(null)]
        public void Should_NotBeValid_When_Id_Is_Invalid(int id)
        {
            // arrange
            var query = new GetFeedbackAttractionQuery
            {
                Id = id
            };

            // act & assert
            AssertNotValid(query);
        }
    }
}
