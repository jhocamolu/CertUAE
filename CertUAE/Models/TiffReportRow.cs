using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertUAE.Models
{
    public class TiffReportRow
    {
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public long TamanoKB { get; set; }
        public double TamanoMB { get; set; }
        public double TamanoGB { get; set; }
        public int Alto { get; set; }
        public int Ancho { get; set; }
    }
}
