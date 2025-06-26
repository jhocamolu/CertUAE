using System;
using System.Collections.Generic;

namespace CertUAE.Models;

/// <summary>
/// Aplica para Libros Antiguo Sistema y Antecedentes Registrales.
/// </summary>
public partial class Metadato
{
    /// <summary>
    /// Tabla de (metadatos). Aplica para Libros Antiguo Sistema y Antecedentes Registrales.
    /// </summary>
    public long IdMetadato { get; set; }

    /// <summary>
    /// Nombre completo del archivo XMP y/o XML Aplica para Libros Antiguo Sistema XXX-3800001.XMP o XML y Antecedentes Registrales XXX-0000000.XMP o XML
    /// </summary>
    public string NomMetadato { get; set; } = null!;

    public long IdTipoMetadato { get; set; }

    /// <summary>
    /// Archivo .XMP y/o .XML
    /// </summary>
    public string Extencion { get; set; } = null!;

    /// <summary>
    /// Letra de identificación de la unidad contenerdora donde se almaceno la información.  Ejem: &quot;C&quot;
    /// </summary>
    public string UnidadContenedora { get; set; } = null!;

    /// <summary>
    /// Ruta completa sin la letra de identificación donde se almaceno la información. Ejem: Users\LENOVO\Desktop\SQL_2024
    /// </summary>
    public string DireccionContenedora { get; set; } = null!;

    public virtual TipoMetadato IdTipoMetadatoNavigation { get; set; } = null!;

    public virtual ICollection<Imagen> Imagens { get; set; } = new List<Imagen>();
}
