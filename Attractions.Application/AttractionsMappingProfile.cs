using Attractions.Application.Dtos;
using Attractions.Application.Handlers.Attractions.Commands.UpdateAttraction;
using Attractions.Application.Handlers.Attractions.Commands.UpdateAttractionStatus;
using AutoMapper;
using Travel.Application.Dtos;
using Travels.Domain;

namespace Attractions.Application
{
    public class AttractionsMappingProfile : Profile
    {
        public AttractionsMappingProfile()
        {
            CreateMap<Attraction, GetAttractionDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<UpdateAttractionCommand, Attraction>();
            CreateMap<UpdateAttractionStatusCommand, Attraction>();
            CreateMap<AddressDto, Address>();
            CreateMap<GeoLocationDto, GeoLocation>();
            CreateMap<GeoLocation, GeoLocationDto>();
            CreateMap<WorkScheduleDto, WorkSchedule>();
            CreateMap<WorkSchedule, WorkScheduleDto>();
            CreateMap<Image, GetImageDto>();

        }
    }
}
