using System;
using System.Collections.Generic;

namespace CertUAE.Models;

public partial class TipoDocumento
{
    public long IdTipoDocumento { get; set; }

    public string NombreDocumento { get; set; } = null!;

    public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();
}
