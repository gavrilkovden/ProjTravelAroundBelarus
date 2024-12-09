using Attractions.Application.Caches.AttractionCaches;
using Attractions.Application.Handlers.Attractions.Queries.GetAttraction;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests;
using MediatR;
using Moq;
using System.Linq.Expressions;
using Travel.Application.Dtos;
using Travels.Domain;
using Xunit.Abstractions;
using Core.Users.Domain.Enums;
using AutoFixture;
using Core.Tests.Fixtures;
using Core.Tests.Helpers;

namespace Travel.UnitTests.Tests.Attractions.Queries.GetAttraction
{
    public class GetAttractionQueryHandlerTest : RequestHandlerTestBase<GetAttractionQuery, GetAttractionDto>
    {
        private readonly Mock<IBaseReadRepository<Attraction>> _attractionsMock = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly IMapper _mapper;
        private readonly AttractionMemoryCache _attractionMemoryCacheMock;

        public GetAttractionQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(GetAttractionQuery).Assembly).Mapper;
            _attractionMemoryCacheMock = new AttractionMemoryCache();
        }

        protected override IRequestHandler<GetAttractionQuery, GetAttractionDto> CommandHandler =>
            new GetAttractionQueryHandler(
                _attractionsMock.Object,
                _currentUserServiceMock.Object,
                _mapper,
                _attractionMemoryCacheMock
            );

      
    }
}
