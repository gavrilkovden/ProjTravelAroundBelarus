using Attractions.Application.Caches.AttractionCaches;
using Attractions.Application.Handlers.Attractions.Commands.DeleteAttraction;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests.Attributes;
using Core.Tests.Helpers;
using Core.Tests;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Domain;
using Xunit.Abstractions;
using Core.Users.Domain.Enums;
using System.Linq.Expressions;
using AutoFixture;

namespace Travel.UnitTests.Tests.Attractions.Commands.DeleteAttraction
{
    public class DeleteAttractionCommandHandlerTest : RequestHandlerTestBase<DeleteAttractionCommand>
    {
        private readonly Mock<IBaseWriteRepository<Attraction>> _attractionsMok = new();
        private readonly Mock<ICurrentUserService> _currentServiceMok = new();
        private readonly Mock<ICleanAttractionsCacheService> _cleanAttractionsCacheServiceMok = new();

        public DeleteAttractionCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IRequestHandler<DeleteAttractionCommand> CommandHandler =>
            new DeleteAttractionCommandHandler(_attractionsMok.Object, _currentServiceMok.Object, _cleanAttractionsCacheServiceMok.Object);

        [Theory, FixtureInlineAutoData]
        public async Task Should_Delete_Attraction_When_Admin_Deletes(DeleteAttractionCommand command, Guid userId)
        {
            // arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(true);

            var attraction = TestFixture.Build<Attraction>().Create();
            _attractionsMok.Setup(
                p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), default)
            ).ReturnsAsync(attraction);

            // act and assert
            await AssertNotThrow(command);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_Delete_Attraction_When_Owner_Deletes(DeleteAttractionCommand command, Guid userId)
        {
            // arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Client)).Returns(true);

            var attraction = TestFixture.Build<Attraction>().With(x => x.UserId, userId).Create();
            _attractionsMok.Setup(
                p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), default)
            ).ReturnsAsync(attraction);

            // act and assert
            await AssertNotThrow(command);

        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_ThrowForbidden_When_Not_Owner_And_Not_Admin_Deletes(DeleteAttractionCommand command, Guid userId)
        {
            // arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Client)).Returns(true);

            var attraction = TestFixture.Build<Attraction>().With(x => x.UserId, GuidHelper.GetNotEqualGiud(userId)).Create();
            _attractionsMok.Setup(
                p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), default)
            ).ReturnsAsync(attraction);

            // act and assert
            await AssertThrowForbiddenFound(command);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_ThrowNotFound_When_Attraction_Not_Found(DeleteAttractionCommand command)
        {
            // arrange
            _attractionsMok.Setup(
                p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), default)
            ).ReturnsAsync(null as Attraction);

            // act and assert
            await AssertThrowNotFound(command);
        }
    }

}
