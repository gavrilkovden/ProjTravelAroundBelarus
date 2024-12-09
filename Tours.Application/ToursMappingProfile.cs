using AutoMapper;
using Tours.Application.Handlers.Tours.Commands.UpdateTour;
using Travels.Domain;

namespace Tours.Application
{
    public class ToursMappingProfile : Profile
    {
        public ToursMappingProfile()
        {
            CreateMap<UpdateTourCommand, Tour>();
        }
    }
}
