using System;
using System.Collections.Generic;

namespace CertUAE.Models;

public partial class Region
{
    /// <summary>
    /// Poblada por SNR
    /// </summary>
    public long IdRegion { get; set; }

    /// <summary>
    /// Poblada por SNR
    /// </summary>
    public string NombreRegion { get; set; } = null!;

    public virtual ICollection<Departamento> Departamentos { get; set; } = new List<Departamento>();
}
