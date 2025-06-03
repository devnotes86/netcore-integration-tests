using AutoMapper;
using HeavyMetalBands.Models;

namespace HeavyMetalBands.Maping
{
    public class BandProfile : Profile
    {

        public BandProfile()
        {
            CreateMap<BandDAO, BandDTO>() 
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.BandName, opt => opt.MapFrom(src => src.band_name))
                .ForMember(dest => dest.YearCreated, opt => opt.MapFrom(src => src.year_created))
                .ForMember(dest => dest.BandNameUppercase, opt => opt.MapFrom(src => (src.band_name == null ? "" : src.band_name.ToUpper())));


            CreateMap<BandDTO, BandDAO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.band_name, opt => opt.MapFrom(src => src.BandName))
                .ForMember(dest => dest.year_created, opt => opt.MapFrom(src => src.YearCreated));
        }
    }
}
