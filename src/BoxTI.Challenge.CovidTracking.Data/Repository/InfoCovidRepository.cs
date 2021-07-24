using BoxTI.Challenge.CovidTracking.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoxTI.Challenge.CovidTracking.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BoxTI.Challenge.CovidTracking.Data.Repository.Base;
using BoxTI.Challenge.CovidTracking.Data.Repository.Interfaces;

namespace BoxTI.Challenge.CovidTracking.Data.Repository
{
    public class InfoCovidRepository : Repository<InfoCovid>, IInfoCovidRepository
    {
        public InfoCovidRepository(CovidTrackingContext context) : base(context)
        {
        }

        public async Task<List<InfoCovid>> GetAllData()
        {
            return await _db.InfoCovid.AsNoTracking().ToListAsync();
        }

        public async Task<InfoCovid> GetByCountry(string country)
        {
            return await _db.InfoCovid.AsNoTracking().FirstOrDefaultAsync(a => a.Country.Equals(country));
        }

        public async Task<List<InfoCovid>> OrderByTotalCases()
        {
            return await _db.InfoCovid.AsNoTracking().OrderByDescending(e => e.ActiveCases).ToListAsync();
        }
    }
}
