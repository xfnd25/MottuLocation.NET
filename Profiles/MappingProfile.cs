using AutoMapper;
using MottuLocation.DTOs;
using MottuLocation.Entities;

namespace MottuLocation.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Moto, MotoDTO>().ReverseMap();
            CreateMap<Sensor, SensorDTO>().ReverseMap();
            CreateMap<Movimentacao, MovimentacaoDTO>().ReverseMap();
        }
    }
}