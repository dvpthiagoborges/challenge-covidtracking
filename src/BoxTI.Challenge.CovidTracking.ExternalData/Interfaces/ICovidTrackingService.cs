using BoxTI.Challenge.CovidTracking.ExternalData.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxTI.Challenge.CovidTracking.ExternalData.Interfaces
{
    public interface ICovidTrackingService
    {
        Task<InfoCovidExternalViewModel> GetByCountry(string country);
        Task<IEnumerable<InfoCovidExternalViewModel>> GetAllData();
    }
}
