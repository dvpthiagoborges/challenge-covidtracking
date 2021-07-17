using BoxTI.Challenge.CovidTracking.ExternalData.ViewModel;
using BoxTI.Challenge.CovidTracking.Models.Entities;
using BoxTI.Challenge.CovidTracking.Services.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxTI.Challenge.CovidTracking.Services.Interfaces
{
    public interface IInfoCovidService : IDisposable
    {
        Task<bool> SaveInfo();
        Task<bool> UpdateInfo(string country);
        Task<bool> DeleteInfo(string country);
        Task<List<InfoCovidViewModel>> GetAllInfo(bool orderedListByCases);
        Task<InfoCovidViewModel> GetInfoByCountry(string country);

        List<InfoCovid> MapDomainList(List<InfoCovidExternalViewModel> viewModel);
    }
}
