﻿using Core.Application.DTOs;
using MediatR;
using Travel.Application.Dtos;

namespace Attractions.Application.Handlers.Attractions.Queries.GetAttractions
{
    public class GetAttractionsQuery : ListAttractionsFilter, IBasePaginationFilter, IRequest<BaseListDto<GetAttractionDto>>
    {
        public int? Limit { get; init; }

        public int? Offset { get; init; }
    }
}