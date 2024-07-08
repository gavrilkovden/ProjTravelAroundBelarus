using Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttractions;
using AutoFixture;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.DTOs;
using Core.Auth.Application.Abstractions.Service;
using Core.Tests.Fixtures;
using Core.Tests;
using MediatR;
using Moq;
using Travel.Application.Dtos;
using Travels.Domain;
using Xunit.Abstractions;
using Attractions.Application.Caches.AttractionFeedback;
using Core.Users.Domain.Enums;
using System.Linq.Expressions;

namespace Travel.UnitTests.Tests.AttractionFeedbacks.Queries.GetFeedbackAttractions
{
    public class GetFeedbackAttractionsQueryHandlerTest : RequestHandlerTestBase<GetFeedbackAttractionsQuery, BaseListDto<GetFeedbackAttractionDto>>
    {
        private readonly Mock<IBaseReadRepository<AttractionFeedback>> _attractionFeedbacksMock = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly IMapper _mapper;
        private readonly AttractionFeedbacksListMemoryCache _attractionFeedbacksListMemoryCacheMock;

        public GetFeedbackAttractionsQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(GetFeedbackAttractionsQuery).Assembly).Mapper;
            _attractionFeedbacksListMemoryCacheMock = new AttractionFeedbacksListMemoryCache();
        }

        protected override IRequestHandler<GetFeedbackAttractionsQuery, BaseListDto<GetFeedbackAttractionDto>> CommandHandler =>
        new GetFeedbackAttractionsQueryHandler(_attractionFeedbacksMock.Object, _currentUserServiceMock.Object, _mapper, _attractionFeedbacksListMemoryCacheMock);


        
    }
}
