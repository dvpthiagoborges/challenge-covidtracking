using BoxTI.Challenge.CovidTracking.Models.Core.Base;
using System;

namespace BoxTI.Challenge.CovidTracking.Models.Entities
{
    public class InfoCovid : Entity
    {
        public int? ActiveCases { get; set; }
        public string Country { get; set; }
        public DateTime LastUpdate { get; set; }
        public string NewCases { get; set; }
        public string NewDeaths { get; set; }
        public int TotalCases { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalRecovered { get; set; }
    }
}
