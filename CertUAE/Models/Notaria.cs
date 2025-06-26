using System;
using System.Collections.Generic;

namespace CertUAE.Models;

/// <summary>
/// Cargada con información de la SNR. No aplica para Libros Antiguo Sistema,  ni para Antecedentes.
/// </summary>
public partial class Notaria
{
    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public long IdNotaria { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public int CodigoDane { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public string NombreNotaria { get; set; } = null!;

    public virtual ICollection<Anotacion> Anotacions { get; set; } = new List<Anotacion>();
}
