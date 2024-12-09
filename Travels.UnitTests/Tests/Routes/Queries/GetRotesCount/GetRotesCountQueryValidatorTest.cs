using Core.Tests;
using FluentValidation;
using Routes.Application.Handlers.Queries.GetRotesCount;
using Xunit.Abstractions;

namespace Travel.UnitTests.Tests.Routes.Queries.GetRotesCount
{
    public class GetRotesCountQueryValidatorTest : ValidatorTestBase<GetRotesCountQuery>
    {
        public GetRotesCountQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<GetRotesCountQuery> TestValidator =>
            new GetRotesCountQueryValidator();

        [Fact]
        public void Should_BeValid_When_ValidPaginationFilter()
        {
            // arrange
            var query = new GetRotesCountQuery
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
            var query = new GetRotesCountQuery
            {
                FreeText = freeText
            };

            // act & assert
            AssertNotValid(query);
        }
    }
}
