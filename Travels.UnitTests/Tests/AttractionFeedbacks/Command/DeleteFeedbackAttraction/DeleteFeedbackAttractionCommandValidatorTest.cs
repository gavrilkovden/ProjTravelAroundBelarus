using Attractions.Application.Handlers.Attractions.Commands.DeleteAttraction;
using Core.Tests.Attributes;
using Core.Tests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.Handlers.Attractions.Commands.DeleteAttraction;
using Xunit.Abstractions;
using Attractions.Application.Handlers.AttractionFeedbacks.Commands.DeleteFeedbackAttraction;

namespace Travel.UnitTests.Tests.AttractionFeedbacks.Command.DeleteFeedbackAttraction
{
    public class DeleteFeedbackAttractionCommandValidatorTest : ValidatorTestBase<DeleteFeedbackAttractionCommand>
    {
        public DeleteFeedbackAttractionCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<DeleteFeedbackAttractionCommand> TestValidator =>
            new DeleteFeedbackAttractionCommandValidator();

        [Theory]
        [FixtureInlineAutoData(1)]
        [FixtureInlineAutoData(10)]
        [FixtureInlineAutoData(20)]
        public void Should_BeValid_When_Id_Is_Valid(int id)
        {
            // arrange
            var command = new DeleteFeedbackAttractionCommand
            {
                Id = id
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [FixtureInlineAutoData(null)]
        public void Should_NotBeValid_When_Id_Is_Invalid(int id)
        {
            // arrange
            var command = new DeleteFeedbackAttractionCommand
            {
                Id = id
            };

            // act & assert
            AssertNotValid(command);
        }
    }
}
