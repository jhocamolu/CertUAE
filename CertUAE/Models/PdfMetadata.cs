using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertUAE.Models
{
    public class PdfMetadata
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Keywords { get; set; }
        public string Creator { get; set; }
        public string Producer { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModDate { get; set; }
        public int PageCount { get; set; }
    }
}
