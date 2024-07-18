using Attractions.Application.Dtos;
using Attractions.Application.Handlers.Attractions.Queries.ImageFilter;
using Core.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.Application.Handlers.Attractions.Queries.GetImages
{
    public class GetImagesQuery : ListImagesFilter, IBasePaginationFilter, IRequest<BaseListDto<GetImageDto>>
    {
        public int? Limit { get; init; }
        public int? Offset { get; init; }
    }
}
