using AutoMapper;
using BoxTI.Challenge.CovidTracking.Data.Repository.Interfaces;
using BoxTI.Challenge.CovidTracking.ExternalData.Interfaces;
using BoxTI.Challenge.CovidTracking.ExternalData.ViewModel;
using BoxTI.Challenge.CovidTracking.Models.Entities;
using BoxTI.Challenge.CovidTracking.Services.Interfaces;
using BoxTI.Challenge.CovidTracking.Services.Notifications.Interfaces;
using BoxTI.Challenge.CovidTracking.Services.Services.Base;
using BoxTI.Challenge.CovidTracking.Services.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxTI.Challenge.CovidTracking.Services.Services
{
    public class InfoCovidService : BaseService, IInfoCovidService
    {
        private readonly IInfoCovidRepository _infoCovidRepository;
        private readonly ICovidTrackingService _covidTrackingService;
        private readonly IMapper _mapper;

        public InfoCovidService(INotifier notifier,
                                IInfoCovidRepository infoCovidRepository,
                                ICovidTrackingService covidTrackingService,
                                IMapper mapper) : base(notifier)
        {
            _infoCovidRepository = infoCovidRepository;
            _covidTrackingService = covidTrackingService;
            _mapper = mapper;
        }

        public async Task<bool> SaveInfo()
        {
            var listInfo = new List<InfoCovidExternalViewModel>();
            string[] listCountries = { "Australia", "Brazil", "Japan", "Netherlands", "Nigeria", "World" };
            foreach (var item in listCountries)
            {
                var country = await _infoCovidRepository.GetByCountry(item);
                if (country == null)
                {
                    var info = await _covidTrackingService.GetByCountry(item);
                    listInfo.Add(info);
                }
            }

            if (listInfo.Count > 0)
            {
                var model = MapDomainList(listInfo);
                await _infoCovidRepository.AddInfo(model);
            }
            else
            {
                Notify("A base de dados já possui as informações de todos os países da lista de requisitos");
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateInfo(string country)
        {
            var info = await _infoCovidRepository.GetByCountry(country);
            if (info == null)
            {
                Notify("Nenhum registro encontrado na base de dados");
                return false;
            }

            var newInfo = await _covidTrackingService.GetByCountry(country);
            if (newInfo == null)
            {
                Notify("Os dados deste país estão indisponíveis, tente atualizar mais tarde");
                return false;
            }

            var model = MapDomain(newInfo);
            model.Id = info.Id;
            model.Active = info.Active;

            await _infoCovidRepository.UpdateInfo(model);
            return true;
        }

        public async Task<bool> DeleteInfo(string country)
        {
            var info = await _infoCovidRepository.GetByCountry(country);
            if (info == null)
            {
                Notify("Nenhum registro encontrado na base de dados");
                return false;
            }

            info.Active = false;

            await _infoCovidRepository.UpdateInfo(info);
            return true;
        }

        public async Task<List<InfoCovidViewModel>> GetAllInfo(bool orderedListByCases)
        {
            var allInfo = new List<InfoCovid>();
            if (!orderedListByCases)
            {
                allInfo = await _infoCovidRepository.GetAllData();
            }
            else
            {
                allInfo = await _infoCovidRepository.OrderByTotalCases();
            }

            return CalcPercentageDifference(_mapper.Map<List<InfoCovidViewModel>>(allInfo));
        }

        public async Task<InfoCovidViewModel> GetInfoByCountry(string country)
        {
            var info = await _infoCovidRepository.GetByCountry(country);
            return _mapper.Map<InfoCovidViewModel>(info);
        }

        public InfoCovid MapDomain(InfoCovidExternalViewModel viewModel)
        {
            FormatValues(viewModel);
            if (viewModel.ActiveCases == "")
            {
                viewModel.ActiveCases = null;
            }

            var result = _mapper.Map<InfoCovid>(viewModel);
            return result;
        }

        public List<InfoCovid> MapDomainList(List<InfoCovidExternalViewModel> viewModel)
        {
            var infoList = new List<InfoCovid>();
            foreach (var item in viewModel)
            {
                FormatValues(item);
                if (item.ActiveCases == "")
                {
                    item.ActiveCases = null;
                }

                var result = _mapper.Map<InfoCovid>(item);
                infoList.Add(result);
            }

            return infoList;
        }

        private void FormatValues(InfoCovidExternalViewModel viewmodel)
        {
            viewmodel.ActiveCases = RemoveMask(viewmodel.ActiveCases);
            viewmodel.TotalCases = RemoveMask(viewmodel.TotalCases);
            viewmodel.TotalDeaths = RemoveMask(viewmodel.TotalDeaths);
            viewmodel.TotalRecovered = RemoveMask(viewmodel.TotalRecovered);
        }

        private string RemoveMask(string maskedValue)
        {
            return maskedValue.Replace(",", "");
        }

        private List<InfoCovidViewModel> CalcPercentageDifference(List<InfoCovidViewModel> viewModel)
        {
            var returnViewModel = viewModel;
            decimal? totalCases = 0;

            foreach (var item in viewModel)
            {
                if (item.ActiveCases != null)
                {
                    totalCases += item.ActiveCases;
                }
            }

            foreach (var item in returnViewModel)
            {
                if (item.ActiveCases.HasValue)
                {
                    decimal? percentage = (item.ActiveCases * 100) / totalCases;
                    item.PercentageDifference = string.Format("{0}%", decimal.Round(percentage.Value, 2, System.MidpointRounding.AwayFromZero).ToString());
                }
            }

            return returnViewModel;
        }

        public void Dispose()
        {
            _infoCovidRepository?.Dispose();
        }
    }
}
