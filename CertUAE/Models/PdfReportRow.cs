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
        public long TamanoKB { get; set; }
        public double TamanoMB { get; set; }
        public double TamanoGB { get; set; }
        public int Paginas { get; set; }
        public int CantidadTiffs { get; set; }
        public int DiferenciaTiffsVsPaginas { get; set; }
    }
}
