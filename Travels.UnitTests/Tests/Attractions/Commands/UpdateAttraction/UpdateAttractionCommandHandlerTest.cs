using Attractions.Application.Caches.AttractionCaches;
using Attractions.Application.Handlers.Attractions.Commands.UpdateAttraction;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests.Attributes;
using Core.Tests;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.Dtos;
using Travels.Domain;
using Xunit.Abstractions;
using Core.Users.Domain.Enums;
using System.Linq.Expressions;

namespace Travel.UnitTests.Tests.Attractions.Commands.UpdateAttraction
{
    public class UpdateAttractionHandlerTest : RequestHandlerTestBase<UpdateAttractionCommand, GetAttractionDto>
    {
        private readonly Mock<IBaseWriteRepository<Attraction>> _attractionsMok = new();
        private readonly Mock<ICurrentUserService> _currentServiceMok = new();
        private readonly Mock<IMapper> _mapperMok = new();
        private readonly Mock<ICleanAttractionsCacheService> _cleanAttractionsCacheServiceMok = new();

        public UpdateAttractionHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IRequestHandler<UpdateAttractionCommand, GetAttractionDto> CommandHandler =>
            new UpdateAttractionHandler(
                _attractionsMok.Object,
                _currentServiceMok.Object,
                _mapperMok.Object,
                _cleanAttractionsCacheServiceMok.Object
            );

        [Theory, FixtureInlineAutoData]
        public async Task Should_Update_Attraction_When_Admin_Updates(
            UpdateAttractionCommand command,
            Guid userId,
            Attraction attractionEntity,
            GetAttractionDto attractionDto
        )
        {
            // arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(true);

            _attractionsMok.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), default))
                .ReturnsAsync(attractionEntity);

            _mapperMok.Setup(m => m.Map<Attraction>(command)).Returns(attractionEntity);
            _mapperMok.Setup(m => m.Map<GetAttractionDto>(attractionEntity)).Returns(attractionDto);

            _attractionsMok.Setup(p => p.UpdateAsync(attractionEntity, default)).ReturnsAsync(attractionEntity);

            // act & assert
            await AssertNotThrow(command);

        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_Update_Attraction_When_Owner_Updates(
            UpdateAttractionCommand command,
            Guid userId,
            Attraction attractionEntity,
            GetAttractionDto attractionDto
        )
        {
            // arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(false);
            _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Client)).Returns(true);

            attractionEntity.UserId = userId;

            _attractionsMok.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), default))
                .ReturnsAsync(attractionEntity);

            _mapperMok.Setup(m => m.Map<Attraction>(command)).Returns(attractionEntity);
            _mapperMok.Setup(m => m.Map<GetAttractionDto>(attractionEntity)).Returns(attractionDto);

            _attractionsMok.Setup(p => p.UpdateAsync(attractionEntity, default)).ReturnsAsync(attractionEntity);

            // act & assert
            await AssertNotThrow(command);
        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_Throw_NotFound_When_Attraction_NotFound(
            UpdateAttractionCommand command,
            Guid userId
        )
        {
            // arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(true);

            _attractionsMok.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), default))
                .ReturnsAsync((Attraction)null);

            // act & assert
            await AssertThrowNotFound(command);

        }

        [Theory, FixtureInlineAutoData]
        public async Task Should_Throw_Forbidden_When_User_NotAdmin_Or_Owner(
            UpdateAttractionCommand command,
            Guid userId,
            Attraction attractionEntity
        )
        {
            // arrange
            _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);
            _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(false);
            _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Client)).Returns(false);

            attractionEntity.UserId = Guid.NewGuid();

            _attractionsMok.Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Attraction, bool>>>(), default))
                .ReturnsAsync(attractionEntity);

            // act & assert
            await AssertThrowForbiddenFound(command);

        }
    }
}
