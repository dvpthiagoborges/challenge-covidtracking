using BoxTI.Challenge.CovidTracking.ExternalData.Base;
using BoxTI.Challenge.CovidTracking.ExternalData.ViewModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoxTI.Challenge.CovidTracking.ExternalData.Interfaces
{
    public class CovidTrackingService : ICovidTrackingService
    {
        public async Task<IEnumerable<InfoCovidExternalViewModel>> GetAllData()
        {
            var action = $"{ExternalDataSettings.Url}/v1";
            var response = await ExecuteRequest(action);
            var responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<InfoCovidExternalViewModel>>(responseBody);
        }

        public async Task<InfoCovidExternalViewModel> GetByCountry(string country)
        {
            var action = $"{ExternalDataSettings.Url}/v1/{country}";
            var response = await ExecuteRequest(action);
            var responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<InfoCovidExternalViewModel>(responseBody);
        }

        private async Task<HttpResponseMessage> ExecuteRequest(string action)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", ExternalDataSettings.Key);
                httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", ExternalDataSettings.Host);

                return await httpClient.GetAsync(action);
            }
        }
    }
}
