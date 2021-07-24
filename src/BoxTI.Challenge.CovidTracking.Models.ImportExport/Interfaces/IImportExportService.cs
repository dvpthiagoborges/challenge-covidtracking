using BoxTI.Challenge.CovidTracking.Services.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BoxTI.Challenge.CovidTracking.Models.ImportExport.Interfaces
{
    public interface IImportExportService
    {
        Task<FileContentResult> ExportFileCSV(InfoCovidViewModel viewModel);
    }
}
