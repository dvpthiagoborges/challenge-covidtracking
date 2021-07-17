using BoxTI.Challenge.CovidTracking.Data.Repository.Interfaces.Base;
using BoxTI.Challenge.CovidTracking.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxTI.Challenge.CovidTracking.Data.Repository.Interfaces
{
    public interface IInfoCovidRepository : IRepository<InfoCovid>
    {
        Task<List<InfoCovid>> GetAllData();
        Task<InfoCovid> GetByCountry(string country);
        Task<List<InfoCovid>> OrderByTotalCases();
    }
}
