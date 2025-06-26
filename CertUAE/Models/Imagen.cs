using System;
using System.Collections.Generic;

namespace CertUAE.Models;

public partial class Imagen
{
    /// <summary>
    /// Autoincremental por insercion de datos - Tabla de (Imágen). Aplica para Libros Antiguo Sistema
    /// </summary>
    public long IdImagen { get; set; }

    public long IdAntecedentesReg { get; set; }

    public long IdMetadato { get; set; }

    public long IdCapaDeTexto { get; set; }

    public long IdTipificacion { get; set; }

    /// <summary>
    /// Cargue de Imágenes .PDF y Imágenes .TIF por cada unidad documental intervenida, para Libros gran formato (Unificación de Imagenes) se cargará en el SQL las imágenes unidas. Para las imágenes .TIF nativas reposaran en una carpeta contenedora por libro llamada (Imagenes TIFF nativas) 
    /// </summary>
    public string NombreImagen { get; set; } = null!;

    /// <summary>
    /// Fecha creación de las imágenes AAAA-MM-DD
    /// </summary>
    public DateOnly FechaCreacionImagen { get; set; }

    /// <summary>
    /// Nombre del Digitalizador por unidad documental, esta persona es la que aparece en la Indexación del PDF/A
    /// </summary>
    public string UserDigitador { get; set; } = null!;

    /// <summary>
    /// Total folios por unidad documental
    /// </summary>
    public int FolioTotal { get; set; }

    /// <summary>
    /// Primer folio del Libro
    /// </summary>
    public int FolioPaginaInicial { get; set; }

    /// <summary>
    /// Ultimo folio del Libro
    /// </summary>
    public int FolioPaginaFinal { get; set; }

    /// <summary>
    /// Tamaño de Imagen en Bytes
    /// </summary>
    public long TamañoImagen { get; set; }

    /// <summary>
    /// Tipo de archivo PDF ó TIF
    /// </summary>
    public string Extencion { get; set; } = null!;

    /// <summary>
    /// Letra de identificación de la unidad contenedora donde se almaceno la información.  Ejem: &quot;C&quot;
    /// </summary>
    public string UnidadContenedora { get; set; } = null!;

    /// <summary>
    /// Ruta completa sin la letra de identificación donde se almaceno la información. Ejem: Users\LENOVO\Desktop\SQL_2024
    /// </summary>
    public string DireccionContenedora { get; set; } = null!;

    /// <summary>
    /// Hash por imágen formato SHA-1 de 250 
    /// </summary>
    public string Hash { get; set; } = null!;

    public virtual AntecedentesReg IdAntecedentesRegNavigation { get; set; } = null!;

    public virtual CapaDeTexto IdCapaDeTextoNavigation { get; set; } = null!;

    public virtual Metadato IdMetadatoNavigation { get; set; } = null!;

    public virtual Tipificacion IdTipificacionNavigation { get; set; } = null!;
}
