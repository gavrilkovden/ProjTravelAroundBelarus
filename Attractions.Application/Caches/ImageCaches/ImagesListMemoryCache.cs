using Attractions.Application.Dtos;
using Core.Application.BaseRealizations;
using Core.Application.DTOs;

namespace Attractions.Application.Caches.ImageCaches
{
    public class ImagesListMemoryCache : BaseCache<BaseListDto<GetImageDto>>;
}
