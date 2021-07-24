using BoxTI.Challenge.CovidTracking.Models.ImportExport.Interfaces;
using BoxTI.Challenge.CovidTracking.Services.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BoxTI.Challenge.CovidTracking.Models.ImportExport.Services
{
    public class ImportExportService : IImportExportService
    {
        public async Task<FileContentResult> ExportFileCSV(InfoCovidViewModel viewModel)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Id,ActiveCases,Country,LastUpdate,NewCases,NewDeaths,TotalCases,TotalDeaths,TotalRecovered,Active");

            builder.AppendLine($"{viewModel.Id},{viewModel.ActiveCases},{viewModel.Country},{viewModel.LastUpdate},{viewModel.NewCases.Replace(",",".")}" +
                $",{viewModel.NewDeaths.Replace(",", ".")},{viewModel.TotalCases},{viewModel.TotalDeaths},{viewModel.TotalRecovered},{viewModel.Active}");

            FileContentResult fileContentResult = new FileContentResult(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv");
            fileContentResult.FileDownloadName = "InfoCovidReport.csv";
            fileContentResult.LastModified = DateTime.Now;

            return fileContentResult;
        }
    }
}
