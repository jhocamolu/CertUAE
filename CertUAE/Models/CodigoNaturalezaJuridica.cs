using System;
using System.Collections.Generic;

namespace CertUAE.Models;

/// <summary>
/// NA para Libros Antiguo Sistema
/// </summary>
public partial class CodigoNaturalezaJuridica
{
    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public string IdNaturalezaJuridica { get; set; } = null!;

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public string EspecificacionDeNaturalezaJuridica { get; set; } = null!;

    public virtual ICollection<Anotacion> Anotacions { get; set; } = new List<Anotacion>();
}
