using System;
using System.Collections.Generic;

namespace CertUAE.Models;

public partial class Departamento
{
    /// <summary>
    /// Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico
    /// </summary>
    public long IdDepartamento { get; set; }

    /// <summary>
    /// Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico
    /// </summary>
    public long IdRegion { get; set; }

    /// <summary>
    /// Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico
    /// </summary>
    public string NombreDepartamento { get; set; } = null!;

    public virtual ICollection<AntecedentesReg> AntecedentesRegs { get; set; } = new List<AntecedentesReg>();

    public virtual Region IdRegionNavigation { get; set; } = null!;

    public virtual ICollection<Municipio> Municipios { get; set; } = new List<Municipio>();

    public virtual ICollection<Orip> Orips { get; set; } = new List<Orip>();
}
