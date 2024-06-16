using Core.Application.DTOs;
using MediatR;
using Tours.Application.Dtos;

namespace Tours.Application.Handlers.Tours.Queries.GetTours
{
    public class GetToursQuery : ListToursFilter, IBasePaginationFilter, IRequest<BaseListDto<GetTourDto>>
    {
        public int? Limit { get; init; }

        public int? Offset { get; init; }
    }
}
