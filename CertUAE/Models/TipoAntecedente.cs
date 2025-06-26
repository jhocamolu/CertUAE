using System;
using System.Collections.Generic;

namespace CertUAE.Models;

public partial class TipoAntecedente
{
    public long IdTipoAntecedente { get; set; }

    /// <summary>
    /// 380 - Número de identificación de la SNR para Libros Antiguo Sistema
    /// </summary>
    public int SerieDocumental { get; set; }

    /// <summary>
    /// Campo con datos SNR-Aplica para Libros Antiguo Sistema
    /// </summary>
    public string NombreTipoAntecedente { get; set; } = null!;

    public virtual ICollection<AntecedentesReg> AntecedentesRegs { get; set; } = new List<AntecedentesReg>();
}
