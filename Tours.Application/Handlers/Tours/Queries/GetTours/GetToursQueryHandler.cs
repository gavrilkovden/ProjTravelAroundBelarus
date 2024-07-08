using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Core.Auth.Application.Abstractions.Service;
using Core.Users.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Tours.Application.Caches.TourCaches;
using Tours.Application.Dtos;
using Travels.Domain;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Tours.Application.Handlers.Tours.Queries.GetTours
{
    public class GetToursQueryHandler : BaseCashedQuery<GetToursQuery, BaseListDto<GetTourDto>>
    {
        private readonly IBaseReadRepository<Tour> _tours;

        private readonly IMapper _mapper;

        private readonly ICurrentUserService _currentUserService;

        public GetToursQueryHandler(IBaseReadRepository<Tour> tours, ICurrentUserService currentUserService, IMapper mapper, ToursListMemoryCache cache) : base(cache)
        {
            _tours = tours;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public override async Task<BaseListDto<GetTourDto>> SentQueryAsync(GetToursQuery request, CancellationToken cancellationToken)
        {
            var query = _tours.AsQueryable().Where(ListTourWhere.WhereForClient(request));

            if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                query = query.Where(e => e.IsApproved || e.Route.UserId == _currentUserService.CurrentUserId);
            }

            if (request.Offset.HasValue)
            {
                query = query.Skip(request.Offset.Value);
            }

            if (request.Limit.HasValue)
            {
                query = query.Take(request.Limit.Value);
            }
            query = query.Include(d => d.TourFeedback);

            var entitiesResult = await _tours.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            var entitiesCount = await _tours.AsAsyncRead().CountAsync(query, cancellationToken);

            var items = _mapper.Map<GetTourDto[]>(entitiesResult);

            foreach (var item in items)
            {
                var tour = entitiesResult.First(e => e.Id == item.Id);
                var ratings = tour.TourFeedback?.Where(f => f.Value.HasValue).Select(f => f.Value.Value).ToList();

                if (ratings != null && ratings.Count > 0)
                {
                    item.AverageRating = Math.Round(ratings.Average(), 1);
                }
                else
                {
                    item.AverageRating = null; // Не устанавливаем значение, если оценок нет
                }
            }

            return new BaseListDto<GetTourDto>
            {
                Items = items,
                TotalCount = entitiesCount
            };
        }
    }
}
