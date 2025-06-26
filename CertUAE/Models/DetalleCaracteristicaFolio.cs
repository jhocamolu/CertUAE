using System;
using System.Collections.Generic;

namespace CertUAE.Models;

public partial class DetalleCaracteristicaFolio
{
    /// <summary>
    /// Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico
    /// </summary>
    public long IdDetalleCaracteristicaFolio { get; set; }

    /// <summary>
    /// Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico
    /// </summary>
    public string ObservacionesFolio { get; set; } = null!;

    /// <summary>
    /// Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico
    /// </summary>
    public string NivelDeterioroFolio { get; set; } = null!;

    /// <summary>
    /// Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico
    /// </summary>
    public string DescripcionDeterioroFolio { get; set; } = null!;

    public virtual ICollection<AntecedentesReg> AntecedentesRegs { get; set; } = new List<AntecedentesReg>();
}
