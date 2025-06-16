using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertUAE.Models
{
    public class FileInfoData
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public long SizeBytes { get; set; }
        public string Extension { get; set; }
        public string Hash { get; set; }
        public string HashType { get; set; }
        public int? PdfPageCount { get; set; } // Solo para PDFs
    }
}
