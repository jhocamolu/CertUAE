using System;
using System.Collections.Generic;

namespace CertUAE.Models;

/// <summary>
/// Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema 
/// </summary>
public partial class CapaDeTexto
{
    /// <summary>
    /// Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico
    /// </summary>
    public long IdCapaDeTexto { get; set; }

    /// <summary>
    /// Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico
    /// </summary>
    public ulong Ocr { get; set; }

    /// <summary>
    /// Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico
    /// </summary>
    public ulong Icr { get; set; }

    /// <summary>
    /// Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico
    /// </summary>
    public ulong Rah { get; set; }

    public virtual ICollection<Imagen> Imagens { get; set; } = new List<Imagen>();
}
