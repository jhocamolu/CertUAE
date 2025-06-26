using System;
using System.Collections.Generic;

namespace CertUAE.Models;

public partial class TipoIdentificacion
{
    public long IdTipoIdentificacion { get; set; }

    /// <summary>
    /// Campo con datos SNR- NA para Libros Antiguo Sistema
    /// </summary>
    public string NombreTipoIdentificacion { get; set; } = null!;

    public virtual ICollection<Interviniente> Intervinientes { get; set; } = new List<Interviniente>();
}
