using Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttractionsCount;
using Attractions.Application.Handlers.Attractions.Queries.GetAttractionsCount;
using Core.Tests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Travel.UnitTests.Tests.Attractions.Queries.GetAttractionsCount
{
    public class GetAttractionsCountQueryValidatorTest : ValidatorTestBase<GetAttractionsCountQuery>
    {
        public GetAttractionsCountQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<GetAttractionsCountQuery> TestValidator =>
            new GetAttractionsCountQueryValidator();

        [Fact]
        public void Should_BeValid_When_ValidPaginationFilter()
        {
            // arrange
            var query = new GetAttractionsCountQuery
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
            var query = new GetAttractionsCountQuery
            {
                FreeText = freeText
            };

            // act & assert
            AssertNotValid(query);
        }
    }
}
