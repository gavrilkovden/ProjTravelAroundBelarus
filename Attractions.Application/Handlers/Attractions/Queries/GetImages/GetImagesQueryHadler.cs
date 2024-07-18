using Attractions.Application.Caches.ImageCaches;
using Attractions.Application.Dtos;
using Attractions.Application.Handlers.Attractions.Queries.ImageFilter;
using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Core.Auth.Application.Abstractions.Service;
using Core.Users.Domain.Enums;
using Travels.Domain;

namespace Attractions.Application.Handlers.Attractions.Queries.GetImages
{
    public class GetImagesQueryHadler : BaseCashedForUserQuery<GetImagesQuery, BaseListDto<GetImageDto>>
    {
        private readonly IBaseReadRepository<Image> _image;

        private readonly IMapper _mapper;

        private readonly ICurrentUserService _currentUserService;

        public GetImagesQueryHadler(IBaseReadRepository<Image> image, ICurrentUserService currentUserService, IMapper mapper, ImagesListMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
        {
            _image = image;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public override async Task<BaseListDto<GetImageDto>> SentQueryAsync(GetImagesQuery request, CancellationToken cancellationToken)
        {
            var query = _image.AsQueryable();

            if (_currentUserService.UserInRole(ApplicationUserRolesEnum.Client))
            {
                query = query.Where(e => e.IsApproved).Where(ListImageWhere.WhereForClient(request, (Guid)_currentUserService.CurrentUserId));
            }

            if (_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                query = query.Where(ListImageWhere.WhereForAdmin(request));
            }

            if (request.Offset.HasValue)
            {
                query = query.Skip(request.Offset.Value);
            }

            if (request.Limit.HasValue)
            {
                query = query.Take(request.Limit.Value);
            }

            var entitiesResult = await _image.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            var entitiesCount = await _image.AsAsyncRead().CountAsync(query, cancellationToken);

            var items = _mapper.Map<GetImageDto[]>(entitiesResult);

            return new BaseListDto<GetImageDto>
            {
                Items = items,
                TotalCount = entitiesCount
            };
        }
    }
}
