using AutoMapper;
using BoxTI.Challenge.CovidTracking.API.Controllers.Base;
using BoxTI.Challenge.CovidTracking.CrossCutting.Identity.Base;
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
        private readonly IMapper _mapper;

        public InfoCovidController(INotifier notifier,
                                   IUser appUser,
                                   IInfoCovidService infoCovidService, 
                                   IMapper mapper) : base(notifier, appUser)
        {
            _infoCovidService = infoCovidService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna as informações de todos os países da lista de requisitos
        /// </summary>
        /// <param name="orderedListByCases">Indica se a consulta retornará uma lista ordenada por número de casos</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllInfo([FromQuery] bool orderedListByCases)
        {
            var infoList = await _infoCovidService.GetAllInfo(orderedListByCases);
            return CustomResponse(infoList);
        }

        /// <summary>
        /// Retorna as informações de um país específico
        /// </summary>
        /// <param name="country">Nome do país</param>
        /// <returns></returns>
        [HttpGet("by-country")]
        public async Task<IActionResult> GetInfoByCountry([FromQuery] string country)
        {
            var info = await _infoCovidService.GetInfoByCountry(country);
            if (info == null) return NotFound();

            return CustomResponse(info);
        }

        /// <summary>
        /// Adiciona as informações de todos os países da lista de requisitos
        /// </summary>
        /// <returns></returns>
        [HttpPost("add-info")]
        public async Task<IActionResult> AddInfoAllCountries()
        {
            var result = await _infoCovidService.SaveInfo();
            if (result)
            {
                return CustomResponse(new { message = "As informações foram salvas na base de dados com sucesso"});
            }

            return CustomResponse();
        }

        /// <summary>
        /// Atualiza as informações de um país específico
        /// </summary>
        /// <param name="country">Nome do país</param>
        /// <returns></returns>
        [HttpPut("update-info")]
        public async Task<IActionResult> UpdateInfoByCountry([FromQuery] string country)
        {
            var result = await _infoCovidService.UpdateInfo(country);
            if (result)
            {
                return CustomResponse(new { message = "As informações do país foram atualizadas com sucesso" });
            }

            return CustomResponse();
        }

        /// <summary>
        /// Executa a exclusão lógica do registro de um país específico
        /// </summary>
        /// <param name="country">Nome do país</param>
        /// <returns></returns>
        [HttpDelete("delete-info")]
        public async Task<IActionResult> DeleteInfoByCountry([FromQuery] string country)
        {
            var result = await _infoCovidService.DeleteInfo(country);
            if (result)
            {
                return CustomResponse(new { message = "Registro do país inativado com sucesso" });
            }

            return CustomResponse();
        }
    }
}
