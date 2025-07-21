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
        public long TamanoBytes { get; set; }
        public int Paginas { get; set; } // Mantenemos para compatibilidad con el CSV actual
        public int CantidadTiffs { get; set; }
        public int DiferenciaTiffsVsPaginas { get; set; }

        public string ContieneXml { get; set; } // Nuevo campo para indicar si contiene XML

        // Nuevos campos para los metadatos del PDF
        public string? PdfAuthor { get; set; }
        public string? PdfTitle { get; set; }
        public string? PdfSubject { get; set; }
        public string? PdfCreator { get; set; }
        public string? PdfProducer { get; set; }
        public string? PdfHashType { get; set; }
        public string? PdfHash { get; set; }
        public DateTime? PdfCreationDate { get; set; }
        public DateTime? PdfModificationDate { get; set; }
        public string? PdfDescription { get; set; }
    }
}
