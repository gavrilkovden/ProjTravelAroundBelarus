﻿using Core.Auth.Application.Attributes;
using MediatR;
using Travel.Application.Dtos;

namespace Attractions.Application.Handlers.Attractions.Queries.GetAttraction
{
    [RequestAuthorize]
    public class GetAttractionQuery : IRequest<GetAttractionDto>
    {
        public int Id { get; init; }
    }
}