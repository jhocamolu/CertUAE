using System;
using System.Collections.Generic;

namespace CertUAE.Models;

public partial class Vereda
{
    public long IdVereda { get; set; }

    /// <summary>
    /// Campo con datos SNR-Aplica para Libros Antiguo Sistema - Aplica de igual manera para antecedentes registrales
    /// </summary>
    public long IdMunicipio { get; set; }

    /// <summary>
    /// Campo con datos SNR-Aplica para Libros Antiguo Sistema - Aplica de igual manera para antecedentes registrales
    /// </summary>
    public string NombreVereda { get; set; } = null!;

    public virtual ICollection<AntecedentesReg> AntecedentesRegs { get; set; } = new List<AntecedentesReg>();

    public virtual Municipio IdMunicipioNavigation { get; set; } = null!;
}
