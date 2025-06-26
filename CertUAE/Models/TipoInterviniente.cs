using System;
using System.Collections.Generic;

namespace CertUAE.Models;

public partial class TipoInterviniente
{
    public long IdTipoInterviniente { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public string TipoInterviniente1 { get; set; } = null!;

    public virtual ICollection<Interviniente> Intervinientes { get; set; } = new List<Interviniente>();
}
