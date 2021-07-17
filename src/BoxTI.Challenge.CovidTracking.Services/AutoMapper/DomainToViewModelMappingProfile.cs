using AutoMapper;
using BoxTI.Challenge.CovidTracking.Models.Entities;
using BoxTI.Challenge.CovidTracking.Services.ViewModel;

namespace BoxTI.Challenge.CovidTracking.Services.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<InfoCovid, InfoCovidViewModel>().ReverseMap();
        }
    }
}
