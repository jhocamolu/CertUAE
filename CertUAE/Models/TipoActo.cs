using System;
using System.Collections.Generic;

namespace CertUAE.Models;

public partial class TipoActo
{
    public long IdTipoActo { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public string TipoActo1 { get; set; } = null!;

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public string? DescripcionActo { get; set; }

    public virtual ICollection<Anotacion> Anotacions { get; set; } = new List<Anotacion>();
}
