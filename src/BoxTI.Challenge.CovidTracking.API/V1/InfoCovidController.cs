using AutoMapper;
using BoxTI.Challenge.CovidTracking.API.Controllers.Base;
using BoxTI.Challenge.CovidTracking.CrossCutting.Identity.Base;
using BoxTI.Challenge.CovidTracking.Models.ImportExport.Interfaces;
using BoxTI.Challenge.CovidTracking.Services.Interfaces;
using BoxTI.Challenge.CovidTracking.Services.Notifications.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BoxTI.Challenge.CovidTracking.API.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/info-covid")]
    [ApiController]
    public class InfoCovidController : BaseController
    {
        private readonly IInfoCovidService _infoCovidService;
        private readonly IImportExportService _importExportService;

        public InfoCovidController(INotifier notifier,
                                   IUser appUser,
                                   IInfoCovidService infoCovidService,
                                   IImportExportService importExportService) : base(notifier, appUser)
        {
            _infoCovidService = infoCovidService;
            _importExportService = importExportService;
        }

        /// <summary>
        /// Returns information for all countries in the requirements list.
        /// </summary>
        /// <param name="orderedListByCases">Enter true for the query to return a list ordered by number of cases.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllInfo([FromQuery] bool orderedListByCases)
        {
            var infoList = await _infoCovidService.GetAllInfo(orderedListByCases);
            return CustomResponse(infoList);
        }

        /// <summary>
        /// Returns information for a specific country.
        /// </summary>
        /// <param name="country">Country name</param>
        /// <returns></returns>
        [HttpGet("by-country")]
        public async Task<IActionResult> GetInfoByCountry([FromQuery] string country)
        {
            var info = await _infoCovidService.GetInfoByCountry(country);
            if (info == null) return NotFound();

            return CustomResponse(info);
        }

        /// <summary>
        /// Adds information for all countries from the requirements list.
        /// </summary>
        /// <returns></returns>
        [HttpPost("add-info")]
        public async Task<IActionResult> AddInfoAllCountries()
        {
            var result = await _infoCovidService.SaveInfo();
            if (result)
            {
                return CustomResponse(new { message = "The information was successfully saved to the database." });
            }

            return CustomResponse();
        }

        /// <summary>
        /// Updates the information for a specific country.
        /// </summary>
        /// <param name="country">Country name</param>
        /// <returns></returns>
        [HttpPut("update-info")]
        public async Task<IActionResult> UpdateInfoByCountry([FromQuery] string country)
        {
            var result = await _infoCovidService.UpdateInfo(country);
            if (result)
            {
                return CustomResponse(new { message = "Country information has been successfully updated." });
            }

            return CustomResponse();
        }

        /// <summary>
        /// Executes logical record deletion for a specific country.
        /// </summary>
        /// <param name="country">Country name</param>
        /// <returns></returns>
        [HttpDelete("delete-info")]
        public async Task<IActionResult> DeleteInfoByCountry([FromQuery] string country)
        {
            var result = await _infoCovidService.DeleteInfo(country);
            if (result)
            {
                return CustomResponse(new { message = "Country registration successfully inactivated." });
            }

            return CustomResponse();
        }

        /// <summary>
        /// Returns csv file with data for a specific country.
        /// </summary>
        /// <param name="country">Country name</param>
        /// <returns></returns>
        [HttpGet("export-csv")]
        public async Task<IActionResult> ExportDataCsv([FromQuery] string country)
        {
            var viewModel = await _infoCovidService.GetInfoByCountry(country);
            if (viewModel == null) return NotFound();

            var file = await _importExportService.ExportFileCSV(viewModel);

            return CustomResponse(file);
        }
    }
}
