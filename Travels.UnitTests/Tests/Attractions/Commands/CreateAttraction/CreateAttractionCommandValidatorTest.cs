//using Attractions.Application.Dtos;
//using Attractions.Application.Handlers.Attractions.Commands.CreateAttraction;
//using Core.Tests.Attributes;
//using Core.Tests;
//using FluentValidation;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Travels.Domain;
//using Xunit.Abstractions;
//using AutoFixture;
//using Travel.Application.Dtos;

//namespace Travel.UnitTests.Tests.Attractions.Commands.CreateAttraction
//{
//    public class CreateAttractionCommandValidatorTests : ValidatorTestBase<CreateAttractionCommand>
//    {
//        public CreateAttractionCommandValidatorTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
//        {
//        }

//        protected override IValidator<CreateAttractionCommand> TestValidator =>
//            TestFixture.Create<CreateAttractionCommandValidator>();

//        [Theory, FixtureAutoData]
//        public void Should_BeValid_When_RequestFieldsAreFilledCorrectly(CreateAttractionCommand command)
//        {
//            // arrange
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

//        [Theory, FixtureInlineAutoData("")]
//        [FixtureInlineAutoData(null)]
//        public void Should_NotBeValid_When_NameIsInvalid(string name, CreateAttractionCommand command)
//        {
//            // arrange
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
//        [InlineData("")]
//        [InlineData(null)]
//        public void Should_NotBeValid_When_RegionIsInvalid(string region)
//        {
//            // arrange
//            var command = new CreateAttractionCommand
//            {
//                Name = "Valid Name",
//                Description = "Valid Description",
//                Price = 10,
//                NumberOfVisitors = 100,
//                GeoLocation = new GeoLocationDto { Latitude = 50, Longitude = 10 },
//                Address = new AddressDto { Region = region, Street = "Some Street", City = "Some City" },
//                WorkSchedules = new List<WorkScheduleDto>
//            {
//                new WorkScheduleDto { DayOfWeek = "Monday", OpenTime = TimeSpan.FromHours(8), CloseTime = TimeSpan.FromHours(18) }
//            }
//            };

//            // act & assert
//            AssertNotValid(command);
//        }

//        [Theory]
//        [InlineData(-91)]
//        [InlineData(91)]
//        public void Should_NotBeValid_When_LatitudeIsOutOfRange(double latitude)
//        {
//            // arrange
//            var command = new CreateAttractionCommand
//            {
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
//            var command = new CreateAttractionCommand
//            {
//                Name = "Valid Name",
//                Description = "Valid Description",
//                Price = 10,
//                NumberOfVisitors = 100,
//                GeoLocation = new GeoLocationDto { Latitude = 50, Longitude = longitude },
//                Address = new AddressDto { Region = "Some Region", Street = "Some Street", City = "Some City" },
//                WorkSchedules = new List<WorkScheduleDto>
//        {
//            new WorkScheduleDto { DayOfWeek = "Monday", OpenTime = TimeSpan.FromHours(8), CloseTime = TimeSpan.FromHours(18) }
//        }
//            };

//            // act & assert
//            AssertNotValid(command);
//        }
//    }
//}
