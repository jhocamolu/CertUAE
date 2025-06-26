using System;
using System.Collections.Generic;

namespace CertUAE.Models;

public partial class Tipificacion
{
    public long IdTipificacion { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public string NombTipiTipoDocumento { get; set; } = null!;

    public virtual ICollection<Imagen> Imagens { get; set; } = new List<Imagen>();
}
