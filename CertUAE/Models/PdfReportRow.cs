using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertUAE.Models
{
    public class PdfReportRow
    {
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public double TamanoKB { get; set; }
        public double TamanoMB { get; set; }
        public double TamanoGB { get; set; }
        public int Paginas { get; set; } // Mantenemos para compatibilidad con el CSV actual
        public int CantidadTiffs { get; set; }
        public int DiferenciaTiffsVsPaginas { get; set; }

        // Nuevos campos para los metadatos del PDF
        public string PdfAuthor { get; set; }
        public string PdfTitle { get; set; }
        public string PdfSubject { get; set; }
        public string PdfCreator { get; set; }
        public string PdfProducer { get; set; }
        public DateTime? PdfCreationDate { get; set; }
        public DateTime? PdfModificationDate { get; set; }
    }
}
