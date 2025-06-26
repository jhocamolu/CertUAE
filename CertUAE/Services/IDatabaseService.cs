using System.Collections.Generic;
using System.Threading.Tasks;

using CertUAE.Models; // Asegúrate de incluir tus modelos

namespace CertUAE.Services
{
    public interface IDatabaseService
    {
        Task SavePdfReportAsync(List<PdfReportRow> report);
        Task SaveTiffReportAsync(List<TiffReportRow> report);
    }
}
