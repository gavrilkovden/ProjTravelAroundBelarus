using Attractions.Application.Handlers.AttractionFeedbacks.Commands.CreateFeedbackAttraction;
using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tours.Application.Handlers.Tours.Queries.GetTours;
using Travel.Application.Handlers.Attractions.Commands.CreateFeedbackAttraction;
using Xunit.Abstractions;

namespace Travel.UnitTests.Tests.AttractionFeedbacks.Command.CreateFeedbackAttraction
{
    public class CreateFeedbackAttractionCommandValidatorTest : ValidatorTestBase<CreateFeedbackAttractionCommand>
    {
        public CreateFeedbackAttractionCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<CreateFeedbackAttractionCommand> TestValidator =>
            TestFixture.Create<CreateFeedbackAttractionCommandValidator>();

        [Theory, FixtureAutoData]
        public void Should_BeValid_When_RequestFieldsAreFilledCorrectly(CreateFeedbackAttractionCommand command)
        {
            // arrange
            command.ValueRating = 4;
            command.AttractionId = 1;
            command.Comment = "Valid comment";

            // act & assert
            AssertValid(command);
        }

        [Theory, FixtureInlineAutoData(-1)]
        [FixtureInlineAutoData(0)]
        public void Should_NotBeValid_When_AttractionIdIsInvalid(int attractionId, CreateFeedbackAttractionCommand command)
        {
            // arrange
            command.AttractionId = attractionId;
            command.ValueRating = 4;
            command.Comment = "Valid comment";

            // act & assert
            AssertNotValid(command);
        }

        [Theory, FixtureInlineAutoData(0)]
        [FixtureInlineAutoData(6)]
        public void Should_NotBeValid_When_ValueRatingIsOutOfRange(int valueRating, CreateFeedbackAttractionCommand command)
        {
            // arrange
            command.ValueRating = valueRating;
            command.AttractionId = 1;
            command.Comment = "Valid comment";

            // act & assert
            AssertNotValid(command);
        }

        [Theory, FixtureInlineAutoData("")]
        public void Should_NotBeValid_When_CommentIsInvalid(string comment, CreateFeedbackAttractionCommand command)
        {
            // arrange
            command.ValueRating = 4;
            command.AttractionId = 1;
            command.Comment = comment;

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("123456789101234567891012345678910123456789101234567891012345678910123456789101234567891012345678910123456789101234567891012345678910123456" +
            "78910123456789101234567891012345678910123456789101234567891012345678910123456789101234567891012345678910123456789101234567891012345678910123456789" +
            "10123456789101234567891012345678910123456789101234567891012345678910123456789101234567891012345678910123456789101234567891012345678910123456789101" +
            "2345678910123456789101234567891012345678910123456789101234567891012345678910123456789101234567891012345678910123456789101234567891012345678910123456" +
            "78910123456789101234567891012345678910123456789101234567891012345678910123456789101234567891012345678910123456789101234567891012345678910123456789101" +
            "2345678910123456789101234567891012345678910123456789101234567891012345678910123456789101234567891012345678910123456789101234567891012345678910123456" +
            "78910123456789101234567891012345678910123456789101234567891012345678910123456789101234567891012345678910123456789101234567891012345678910123456789101" +
            "2345678910123456789101234567891012345678910123456789101234567891012345678910123456789101234567891012345678910123456789101234567891012345678910123456")]
        public void Should_NotBeValid_When_CommentExceedsMaximumLength(string longComment)
        {
            // arrange
            var command = new CreateFeedbackAttractionCommand
            {
                ValueRating = 4,
                AttractionId = 1,
                Comment = longComment
            };

            // act & assert
            AssertNotValid(command);
        }
    }
}