using AutoMapper;
using StarWarsAPI.Models.DTOs;
using StarWarsAPI.Models.Entities;

namespace StarWarsAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Habitant, HabitantDTO>();
            CreateMap<Species, SpeciesDTO>();
            CreateMap<Planet, PlanetDTO>();
        }
    }
}
