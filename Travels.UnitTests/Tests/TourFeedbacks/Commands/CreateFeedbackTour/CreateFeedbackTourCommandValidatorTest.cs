using Core.Tests.Attributes;
using Core.Tests;
using FluentValidation;
using Xunit.Abstractions;
using Tours.Application.Handlers.TourFeedbacks.Commands.CreateFeedbackTour;
using AutoFixture;

namespace Travel.UnitTests.Tests.TourFeedbacks.Commands.CreateFeedbackTour
{
    public class CreateFeedbackTourCommandValidatorTest : ValidatorTestBase<CreateFeedbackTourCommand>
    {
        public CreateFeedbackTourCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<CreateFeedbackTourCommand> TestValidator =>
            TestFixture.Create<CreateFeedbackTourCommandValidator>();

        [Theory, FixtureAutoData]
        public void Should_BeValid_When_RequestFieldsAreFilledCorrectly(CreateFeedbackTourCommand command)
        {
            // arrange
            command.UserId = Guid.NewGuid();
            command.TourId = 1;
            command.Comment = "test";
            command.Value = 4;
            
            // act & assert
            AssertValid(command);
        }


        [Theory, FixtureAutoData]
        public void Should_NotBeValid_When_UserIdIsEmpty(CreateFeedbackTourCommand command)
        {
            // arrange
            command.UserId = Guid.Empty;  
            command.TourId = 1;
            command.Comment = "test";
            command.Value = 4;

            // act & assert
            AssertNotValid(command);
        }

        [Theory, FixtureAutoData]
        public void Should_NotBeValid_When_TourIdIsZeroOrNegative(CreateFeedbackTourCommand command)
        {
            // arrange
            command.UserId = Guid.NewGuid();
            command.TourId = 0;  
            command.Comment = "test";
            command.Value = 4;

            // act & assert
            AssertNotValid(command);
        }

        [Theory, FixtureAutoData]
        public void Should_NotBeValid_When_CommentIsTooLong(CreateFeedbackTourCommand command)
        {
            // arrange
            command.UserId = Guid.NewGuid();
            command.TourId = 1;
            command.Comment = new string('a', 1001); 
            command.Value = 4;

            // act & assert
            AssertNotValid(command);
        }

        [Theory, FixtureAutoData]
        public void Should_NotBeValid_When_CommentIsEmpty(CreateFeedbackTourCommand command)
        {
            // arrange
            command.UserId = Guid.NewGuid();
            command.TourId = 1;
            command.Comment = "";
            command.Value = 4;

            // act & assert
            AssertNotValid(command);
        }
    }
}
