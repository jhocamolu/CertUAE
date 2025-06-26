using System;
using System.Collections.Generic;

namespace CertUAE.Models;

public partial class Interviniente
{
    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public long IdInterviniente { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public long IdAnotacion { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public long IdTipoIdentificacion { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public long IdTipoInterviniente { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public int NumeroIdentifInterviniente { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public string NombreInterviniente { get; set; } = null!;

    public virtual Anotacion IdAnotacionNavigation { get; set; } = null!;

    public virtual TipoIdentificacion IdTipoIdentificacionNavigation { get; set; } = null!;

    public virtual TipoInterviniente IdTipoIntervinienteNavigation { get; set; } = null!;
}
