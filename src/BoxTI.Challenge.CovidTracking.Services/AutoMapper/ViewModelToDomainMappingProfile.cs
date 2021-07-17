using AutoMapper;
using BoxTI.Challenge.CovidTracking.ExternalData.ViewModel;
using BoxTI.Challenge.CovidTracking.Models.Entities;

namespace BoxTI.Challenge.CovidTracking.Services.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<InfoCovidExternalViewModel, InfoCovid>()
                .ForMember(dest => dest.ActiveCases, opt => opt.MapFrom(src => src.ActiveCases))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => src.LastUpdate))
                .ForMember(dest => dest.NewCases, opt => opt.MapFrom(src => src.NewCases))
                .ForMember(dest => dest.NewDeaths, opt => opt.MapFrom(src => src.NewDeaths))
                .ForMember(dest => dest.TotalCases, opt => opt.MapFrom(src => int.Parse(src.TotalCases)))
                .ForMember(dest => dest.TotalDeaths, opt => opt.MapFrom(src => int.Parse(src.TotalDeaths)))
                .ForMember(dest => dest.TotalRecovered, opt => opt.MapFrom(src => int.Parse(src.TotalRecovered)));
        }
    }
}
