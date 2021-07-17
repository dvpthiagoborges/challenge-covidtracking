using System;

namespace BoxTI.Challenge.CovidTracking.Services.ViewModel
{
    public class InfoCovidViewModel
    {
        public Guid Id { get; set; }
        public int? ActiveCases { get; set; }
        public string Country { get; set; }
        public DateTime LastUpdate { get; set; }
        public string NewCases { get; set; }
        public string NewDeaths { get; set; }
        public int TotalCases { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalRecovered { get; set; }
        public bool Active { get; set; }
    }
}
