using System;
using System.Collections.Generic;

namespace CertUAE.Models;

public partial class Orip
{
    /// <summary>
    /// Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico - Aplica para antecedentes registrales
    /// </summary>
    public string IdOrip { get; set; } = null!;

    /// <summary>
    /// Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico - Aplica para antecedentes registrales
    /// </summary>
    public long IdDepartamento { get; set; }

    /// <summary>
    /// Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico - Aplica para antecedentes registrales
    /// </summary>
    public long IdMunicipio { get; set; }

    /// <summary>
    /// Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico - Aplica para antecedentes registrales
    /// </summary>
    public string NombreOficina { get; set; } = null!;

    public virtual ICollection<AntecedentesReg> AntecedentesRegs { get; set; } = new List<AntecedentesReg>();

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;

    public virtual Municipio IdMunicipioNavigation { get; set; } = null!;
}
