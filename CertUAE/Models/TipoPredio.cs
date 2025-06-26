using System;
using System.Collections.Generic;

namespace CertUAE.Models;

public partial class TipoPredio
{
    public long IdTipoPredio { get; set; }

    /// <summary>
    /// Campo con datos SNR-Aplica para Libros Antiguo Sistema    -      id_tipo_predio:1  nombre_del_predio:NO APLICA - Para antecedentes registrales depende del predio.
    /// </summary>
    public string NombreDelPredio { get; set; } = null!;

    public virtual ICollection<AntecedentesReg> AntecedentesRegs { get; set; } = new List<AntecedentesReg>();
}
