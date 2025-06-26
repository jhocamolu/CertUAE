using System;
using System.Collections.Generic;

namespace CertUAE.Models;

public partial class TipoLibro
{
    /// <summary>
    /// Campo con datos SNR-Aplica para Libros Antiguo Sistema -        id_tipo libro:1    serie_libro:380    serie_documental:Libros Antiguo Sistema
    /// </summary>
    public long IdTipoLibro { get; set; }

    /// <summary>
    /// Campo con datos SNR-Aplica para Libros Antiguo Sistema -        id_tipo libro:1    serie_libro:380    serie_documental:Libros Antiguo Sistema
    /// </summary>
    public int SerieLibro { get; set; }

    /// <summary>
    /// Campo con datos SNR-Aplica para Libros Antiguo Sistema -        id_tipo libro:1    serie_libro:380    serie_documental:Libros Antiguo Sistema
    /// </summary>
    public int SubSerieLibro { get; set; }

    /// <summary>
    /// Campo con datos SNR-Aplica para Libros Antiguo Sistema -        id_tipo libro:1    serie_libro:380    serie_documental:Libros Antiguo Sistema
    /// </summary>
    public string SerieDocumental { get; set; } = null!;

    /// <summary>
    /// Campo con datos SNR-Aplica para Libros Antiguo Sistema -        id_tipo libro:1    serie_libro:380    serie_documental:Libros Antiguo Sistema
    /// </summary>
    public string TipoDocumental { get; set; } = null!;

    public virtual ICollection<AntecedentesReg> AntecedentesRegs { get; set; } = new List<AntecedentesReg>();
}
