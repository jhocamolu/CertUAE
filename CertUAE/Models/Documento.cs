using System;
using System.Collections.Generic;

namespace CertUAE.Models;

/// <summary>
/// NA para Libros Antiguo Sistema
/// </summary>
public partial class Documento
{
    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public long IdDocumento { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public long IdTipoDocumento { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public string NumeroDocumento { get; set; } = null!;

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public DateOnly FechaDocumento { get; set; }

    public virtual ICollection<Anotacion> Anotacions { get; set; } = new List<Anotacion>();

    public virtual TipoDocumento IdTipoDocumentoNavigation { get; set; } = null!;
}
