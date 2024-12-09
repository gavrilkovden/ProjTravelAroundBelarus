//using Attractions.Application.Dtos;
//using Attractions.Application.Handlers.Attractions.Commands.UpdateAttraction;
//using Core.Tests.Attributes;
//using Core.Tests;
//using FluentValidation;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Travel.Application.Dtos;
//using Xunit.Abstractions;
//using AutoFixture;
//using Travels.Domain;

//namespace Travel.UnitTests.Tests.Attractions.Commands.UpdateAttraction
//{
//    public class UpdateAttractionValidatorTests : ValidatorTestBase<UpdateAttractionCommand>
//    {
//        public UpdateAttractionValidatorTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
//        {
//        }

//        protected override IValidator<UpdateAttractionCommand> TestValidator =>
//            TestFixture.Create<UpdateAttractionValidator>();

//        [Theory, FixtureInlineAutoData]
//        public void Should_BeValid_When_RequestFieldsAreFilledCorrectly(UpdateAttractionCommand command)
//        {
//            // arrange
//            command.Id = 1;
//            command.Name = "Valid Name";
//            command.Description = "Valid Description";
//            command.Price = 10;
//            command.NumberOfVisitors = 100;
//            command.GeoLocation = new GeoLocationDto { Latitude = 50, Longitude = 10 };
//            command.Address = new AddressDto { Region = "Some Region", Street = "Some Street", City = "Some City" };
//            command.WorkSchedules = new List<WorkScheduleDto>
//        {
//            new WorkScheduleDto { DayOfWeek = "Monday", OpenTime = TimeSpan.FromHours(8), CloseTime = TimeSpan.FromHours(18) }
//        };

//            // act & assert
//            AssertValid(command);
//        }

//        [Theory]
//        [FixtureInlineAutoData("")]
//        [FixtureInlineAutoData(null)]
//        public void Should_NotBeValid_When_NameIsInvalid(string name, UpdateAttractionCommand command)
//        {
//            // arrange
//            command.Id = 1;
//            command.Name = name;
//            command.Description = "Valid Description";
//            command.Price = 10;
//            command.NumberOfVisitors = 100;
//            command.GeoLocation = new GeoLocationDto { Latitude = 50, Longitude = 10 };
//            command.Address = new AddressDto { Region = "Some Region", Street = "Some Street", City = "Some City" };
//            command.WorkSchedules = new List<WorkScheduleDto>
//        {
//            new WorkScheduleDto { DayOfWeek = "Monday", OpenTime = TimeSpan.FromHours(8), CloseTime = TimeSpan.FromHours(18) }
//        };

//            // act & assert
//            AssertNotValid(command);
//        }

//        [Theory]
//        [FixtureInlineAutoData("")]
//        [FixtureInlineAutoData(null)]
//        public void Should_NotBeValid_When_RegionIsInvalid(string region, UpdateAttractionCommand command)
//        {
//            // arrange
//            command.Id = 1;
//            command.Name = "Valid Name";
//            command.Description = "Valid Description";
//            command.Price = 10;
//            command.NumberOfVisitors = 100;
//            command.GeoLocation = new GeoLocationDto { Latitude = 50, Longitude = 10 };
//            command.Address = new AddressDto { Region = region, Street = "Some Street", City = "Some City" };
//            command.WorkSchedules = new List<WorkScheduleDto>
//        {
//            new WorkScheduleDto { DayOfWeek = "Monday", OpenTime = TimeSpan.FromHours(8), CloseTime = TimeSpan.FromHours(18) }
//        };

//            // act & assert
//            AssertNotValid(command);
//        }

//        [Theory]
//        [InlineData(-91)]
//        [InlineData(91)]
//        public void Should_NotBeValid_When_LatitudeIsOutOfRange(double latitude)
//        {
//            // arrange
//            var command = new UpdateAttractionCommand
//            {
//                Id = 1,
//                Name = "Valid Name",
//                Description = "Valid Description",
//                Price = 10,
//                NumberOfVisitors = 100,
//                GeoLocation = new GeoLocationDto { Latitude = latitude, Longitude = 10 },
//                Address = new AddressDto { Region = "Some Region", Street = "Some Street", City = "Some City" },
//                WorkSchedules = new List<WorkScheduleDto>
//            {
//                new WorkScheduleDto { DayOfWeek = "Monday", OpenTime = TimeSpan.FromHours(8), CloseTime = TimeSpan.FromHours(18) }
//            }
//            };

//            // act & assert
//            AssertNotValid(command);
//        }

//        [Theory]
//        [InlineData(-181)]
//        [InlineData(181)]
//        public void Should_NotBeValid_When_LongitudeIsOutOfRange(double longitude)
//        {
//            // arrange
//            var command = new UpdateAttractionCommand
//            {
//                Id = 1,
//                Name = "Valid Name",
//                Description = "Valid Description",
//                Price = 10,
//                NumberOfVisitors = 100,
//                GeoLocation = new GeoLocationDto { Latitude = 50, Longitude = longitude },
//                Address = new AddressDto { Region = "Some Region", Street = "Some Street", City = "Some City" },
//                WorkSchedules = new List<WorkScheduleDto>
//            {
//                new WorkScheduleDto { DayOfWeek = "Monday", OpenTime = TimeSpan.FromHours(8), CloseTime = TimeSpan.FromHours(18) }
//            }
//            };

//            // act & assert
//            AssertNotValid(command);
//        }

//        [Theory, FixtureInlineAutoData]
//        public void Should_NotBeValid_When_NumberOfVisitorsIsNegative( UpdateAttractionCommand command)
//        {
//            // arrange
//            command.Id = 1;
//            command.Name = "Valid Name";
//            command.Description = "Valid Description";
//            command.Price = 10;
//            command.NumberOfVisitors = -1;
//            command.GeoLocation = new GeoLocationDto { Latitude = 50, Longitude = 10 };
//            command.Address = new AddressDto { Region = "Some Region", Street = "Some Street", City = "Some City" };
//            command.WorkSchedules = new List<WorkScheduleDto>
//        {
//            new WorkScheduleDto { DayOfWeek = "Monday", OpenTime = TimeSpan.FromHours(8), CloseTime = TimeSpan.FromHours(18) }
//        };

//            // act & assert
//            AssertNotValid(command);
//        }

//        [Theory, FixtureInlineAutoData]
//        public void Should_NotBeValid_When_WorkScheduleIsEmpty(UpdateAttractionCommand command)
//        {
//            // arrange
//            command.Id = 1;
//            command.Name = "Valid Name";
//            command.Description = "Valid Description";
//            command.Price = 10;
//            command.NumberOfVisitors = 100;
//            command.GeoLocation = new GeoLocationDto { Latitude = 50, Longitude = 10 };
//            command.Address = new AddressDto { Region = "Some Region", Street = "Some Street", City = "Some City" };
//            command.WorkSchedules = new List<WorkScheduleDto>(); // пустой список WorkSchedules

//            // act & assert
//            AssertNotValid(command);
//        }

//        [Theory, FixtureInlineAutoData]
//        public void Should_NotBeValid_When_WorkScheduleDayOfWeekIsInvalid(UpdateAttractionCommand command)
//        {
//            // arrange
//            command.Id = 1;
//            command.Name = "Valid Name";
//            command.Description = "Valid Description";
//            command.Price = 10;
//            command.NumberOfVisitors = 100;
//            command.GeoLocation = new GeoLocationDto { Latitude = 50, Longitude = 10 };
//            command.Address = new AddressDto { Region = "Some Region", Street = "Some Street", City = "Some City" };
//            command.WorkSchedules = new List<WorkScheduleDto>
//        {
//            new WorkScheduleDto { DayOfWeek = "InvalidDay", OpenTime = TimeSpan.FromHours(8), CloseTime = TimeSpan.FromHours(18) }
//        };

//            // act & assert
//            AssertNotValid(command);
//        }
//    }

//}
