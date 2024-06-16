using AutoFixture;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.DTOs;
using Core.Application.Exceptions;
using Core.Tests;
using Core.Tests.Attributes;
using Core.Tests.Fixtures;
using Core.Users.Domain;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Caches;
using Users.Application.Dtos;
using Users.Application.Handlers.Queries;
using Users.Application.Handlers.Queries.GetUser;
using Users.Application.Handlers.Queries.GetUsers;
using Xunit.Abstractions;

namespace Users.UnitTests.Tests.Queries.GetUsers
{
    public class GetUsersQueryHandlerTests : RequestHandlerTestBase<GetUsersQuery, BaseListDto<GetUserDto>>
    {
        private readonly Mock<IBaseReadRepository<ApplicationUser>> _usersMock = new();
        private readonly Mock<ApplicationUsersListMemoryCache> _mockUserMemoryCache = new();
        private readonly IMapper _mapper;

        public GetUsersQueryHandlerTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(GetUserQuery).Assembly).Mapper;
        }

        protected override IRequestHandler<GetUsersQuery, BaseListDto<GetUserDto>> CommandHandler =>
            new GetUsersQueryHandler(_usersMock.Object, _mapper, _mockUserMemoryCache.Object);

        [Fact]
        public async Task Should_BeValid_When_GetUsers()
        {
            // arrange
            var query = new GetUsersQuery();
            var users = TestFixture.Build<ApplicationUser>().CreateMany(5).ToList();

            _usersMock
                .Setup(p => p.AsQueryable())
                .Returns(users.AsQueryable());

            _usersMock
                .Setup(p => p.AsAsyncRead().CountAsync(It.IsAny<IQueryable<ApplicationUser>>(), default))
                .ReturnsAsync(users.Count);

            // act and assert
            await AssertNotThrow(query);
        }

        [Fact]
        public async Task Should_BeValid_When_GetUsersWithOffsetAndLimit()
        {
            // arrange
            var query = new GetUsersQuery
            {
                Offset = 1,
                Limit = 3
            };
            var users = TestFixture.Build<ApplicationUser>().CreateMany(5).ToList();

            _usersMock
                .Setup(p => p.AsQueryable())
                .Returns(users.AsQueryable());

            _usersMock
                .Setup(p => p.AsAsyncRead().CountAsync(It.IsAny<IQueryable<ApplicationUser>>(), default))
                .ReturnsAsync(users.Count);

            // act and assert
            await AssertNotThrow(query);
        }


    }

}
