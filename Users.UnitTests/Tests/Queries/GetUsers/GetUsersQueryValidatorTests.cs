using AutoFixture.Xunit2;
using Core.Tests;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Routing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Handlers.Queries.GetUsers;
using Xunit.Abstractions;
using TheoryAttribute = Xunit.TheoryAttribute;

namespace Users.UnitTests.Tests.Queries.GetUsers
{
    public class GetUsersQueryValidatorTests : ValidatorTestBase<GetUsersQuery>
    {
        public GetUsersQueryValidatorTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<GetUsersQuery> TestValidator =>
            new GetUsersQueryValidator();

        [Fact]
        public void Should_BeValid_When_ValidPaginationFilter()
        {
            // arrange
            var query = new GetUsersQuery
            {
                Limit = 10,
                Offset = 1
            };

            // act & assert
            AssertValid(query);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(51)]
        public void Should_NotBeValid_When_InvalidPaginationFilter(int? limit)
        {
            // arrange
            var query = new GetUsersQuery
            {
                Limit = limit,
                Offset = 0
            };

            // act & assert
            AssertNotValid(query);
        }
    }
}
