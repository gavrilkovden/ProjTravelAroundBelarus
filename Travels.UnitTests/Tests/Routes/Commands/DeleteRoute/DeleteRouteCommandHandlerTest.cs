using Attractions.Application.Handlers.AttractionFeedbacks.Commands.DeleteFeedbackAttraction;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests.Attributes;
using Core.Tests;
using MediatR;
using Moq;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.Caches.AttractionFeedback;
using Travels.Domain;
using Xunit.Abstractions;
using Routes.Application.Handlers.Commands.DeleteRoute;
using System.Linq.Expressions;
using Routes.Application.Caches;
using Core.Users.Domain.Enums;
using AutoFixture;
using Core.Tests.Helpers;

namespace Travel.UnitTests.Tests.Routes.Commands.DeleteRoute
{

    public class DeleteRouteCommandHandlerTest : RequestHandlerTestBase<DeleteRouteCommand>
    {
        private readonly Mock<IBaseWriteRepository<Route>> _routesMock = new();
        private readonly Mock<ICurrentUserService> _currentServiceMock = new();
        private readonly Mock<ICleanRoutesCacheService> _cleanRoutesCacheServiceMock = new();

        public DeleteRouteCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IRequestHandler<DeleteRouteCommand> CommandHandler =>
            new DeleteRouteCommandHandler(_routesMock.Object, _currentServiceMock.Object, _cleanRoutesCacheServiceMock.Object);

        [Theory, FixtureInlineAutoData]
        public async Task Should_Delete_Route_When_Admin_Deletes(DeleteRouteCommand command, Guid userId)
        {
            // arrange
            _currentServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentServiceMock.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(true);

            var route = TestFixture.Build<Route>().Create();
            _routesMock.Setup(
                p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Route, bool>>>(), default)
            ).ReturnsAsync(route);

            // act and assert
            await AssertNotThrow(command);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_ThrowForbidden_When_Not_AdminOrOwner_Deletes(DeleteRouteCommand command, Guid userId)
        {
            // arrange
            _currentServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentServiceMock.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(false);

            var route = TestFixture.Build<Route>().Create();
            route.UserId = GuidHelper.GetNotEqualGiud(userId);

            _routesMock.Setup(
                p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Route, bool>>>(), default)
            ).ReturnsAsync(route);


            // act and assert
            await AssertThrowForbiddenFound(command);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_ThrowNotFound_When_Route_Not_Found(DeleteRouteCommand command)
        {
            // arrange
            _routesMock.Setup(
                p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Route, bool>>>(), default)
            ).ReturnsAsync(null as Route);

            // act and assert
            await AssertThrowNotFound(command);
        }
    }
}
