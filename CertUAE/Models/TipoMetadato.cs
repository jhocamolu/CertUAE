using System;
using System.Collections.Generic;

namespace CertUAE.Models;

public partial class TipoMetadato
{
    public long IdTipoMetadato { get; set; }

    /// <summary>
    /// Campo con datos SNR-Aplica para Libros Antiguo Sistema    -   id_tipo_metadato:1   nom_tipo_metadato: ND
    /// </summary>
    public string NomTipoMetadato { get; set; } = null!;

    public virtual ICollection<Metadato> Metadatos { get; set; } = new List<Metadato>();
}
