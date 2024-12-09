using AutoMapper;
using Routes.Application.Dtos;
using Routes.Application.Handlers.Commands.UpdateRoute;
using Routes.Application.Handlers.Queries.GetRoute;
using Travels.Domain;

namespace Routes.Application
{
    public class RoutesMappingProfile : Profile
    {
        public RoutesMappingProfile()
        {
            CreateMap<UpdateRouteCommand, Route>();
            CreateMap<GetAttractionInRouteDto, AttractionInRoute>();
        }
    }
}
