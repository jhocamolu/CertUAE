using System;
using System.Collections.Generic;

namespace CertUAE.Models;

public partial class Municipio
{
    /// <summary>
    /// Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico -  Aplica de igual manera para antecedentes registrales.
    /// </summary>
    public long IdMunicipio { get; set; }

    /// <summary>
    /// Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico -  Aplica de igual manera para antecedentes registrales.
    /// </summary>
    public long IdDepartamento { get; set; }

    /// <summary>
    /// Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico -  Aplica de igual manera para antecedentes registrales.
    /// </summary>
    public long CodigoMunicipio { get; set; }

    /// <summary>
    /// Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico -  Aplica de igual manera para antecedentes registrales.
    /// </summary>
    public string NombreMunicipio { get; set; } = null!;

    public virtual ICollection<AntecedentesReg> AntecedentesRegs { get; set; } = new List<AntecedentesReg>();

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;

    public virtual ICollection<Orip> Orips { get; set; } = new List<Orip>();

    public virtual ICollection<Vereda> Vereda { get; set; } = new List<Vereda>();
}
