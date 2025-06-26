using System;
using System.Collections.Generic;

namespace CertUAE.Models;

/// <summary>
/// Aplica para Libros Antiguo Sistema
/// </summary>
public partial class AntecedentesReg
{
    /// <summary>
    /// Autoincremental por insercion de datos - Tabla de (Antecedentes Registrales). Aplica para Libros Antiguo Sistema
    /// </summary>
    public long IdAntecedentesReg { get; set; }

    public long IdTipoAntecedente { get; set; }

    public string IdOrip { get; set; } = null!;

    public long IdDepartamento { get; set; }

    public long IdMunicipio { get; set; }

    public long IdVereda { get; set; }

    public long IdTipoLibro { get; set; }

    public long IdDetalleCaracteristicaFolio { get; set; }

    public long IdInventario { get; set; }

    public long IdTrazabUnidDocumental { get; set; }

    public long IdTipoPredio { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public string FolioDeMatriculaInmobiliaria { get; set; } = null!;

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public string NumeroDeTurno { get; set; } = null!;

    /// <summary>
    /// Rotulo SNR - Stickers fisico del Libro
    /// </summary>
    public string RotuloSnr { get; set; } = null!;

    /// <summary>
    /// Codigo LAS (N° Rotulo GGD - X-380XXXX) - Según formato inventario Diagnóstico
    /// </summary>
    public string NumeroLibro { get; set; } = null!;

    /// <summary>
    /// Según formato Inventario Diagnóstico
    /// </summary>
    public int NumeroLibroPortada { get; set; }

    /// <summary>
    /// Según formato Inventario Diagnóstico
    /// </summary>
    public int NumeroLibroLomo { get; set; }

    /// <summary>
    /// Según formato Inventario Diagnóstico
    /// </summary>
    public string NombreLibro { get; set; } = null!;

    /// <summary>
    /// Según formato Inventario Diagnóstico
    /// </summary>
    public string DescripcionLibro { get; set; } = null!;

    /// <summary>
    /// El número que identifica el Libro Ejemplo: LAS XXX-3800001, se debe colocar el N° 1
    /// </summary>
    public int CodLibro { get; set; }

    /// <summary>
    /// Fecha inicial del Libro Según formato Inventario Diagnóstico
    /// </summary>
    public DateOnly FechaLibro { get; set; }

    /// <summary>
    /// Fecha inicial del Libro Según formato Inventario Diagnóstico
    /// </summary>
    public DateOnly FechaInicial { get; set; }

    /// <summary>
    /// Fecha final del Libro Según formato Inventario Diagnóstico
    /// </summary>
    public DateOnly FechaFinal { get; set; }

    /// <summary>
    /// Folio inicial del Libro Según formato Inventario Diagnóstico
    /// </summary>
    public int FolioPagIni { get; set; }

    /// <summary>
    /// Folio final del Libro Según formato Inventario Diagnóstico
    /// </summary>
    public int FolioPagFina { get; set; }

    /// <summary>
    /// Ultimo folio con contenido del Libro
    /// </summary>
    public int UltFolioConContenido { get; set; }

    /// <summary>
    /// Aplica cuando los Libros tienen rangos de partida
    /// </summary>
    public int RangoPartidaDesde { get; set; }

    /// <summary>
    /// Aplica cuando los Libros tienen rangos de partida
    /// </summary>
    public int RangoPartidaHasta { get; set; }

    /// <summary>
    /// Se debe colocar las observaciones del formato diagnostico (Columna Observación)
    /// </summary>
    public string Observaciones { get; set; } = null!;

    /// <summary>
    /// Nombre del proveedor que intervendrá los Libros
    /// </summary>
    public string Proveedor { get; set; } = null!;

    public virtual ICollection<Anotacion> Anotacions { get; set; } = new List<Anotacion>();

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;

    public virtual DetalleCaracteristicaFolio IdDetalleCaracteristicaFolioNavigation { get; set; } = null!;

    public virtual Inventario IdInventarioNavigation { get; set; } = null!;

    public virtual Municipio IdMunicipioNavigation { get; set; } = null!;

    public virtual Orip IdOripNavigation { get; set; } = null!;

    public virtual TipoAntecedente IdTipoAntecedenteNavigation { get; set; } = null!;

    public virtual TipoLibro IdTipoLibroNavigation { get; set; } = null!;

    public virtual TipoPredio IdTipoPredioNavigation { get; set; } = null!;

    public virtual TrazabilidadUnidadDocumental IdTrazabUnidDocumentalNavigation { get; set; } = null!;

    public virtual Vereda IdVeredaNavigation { get; set; } = null!;

    public virtual ICollection<Imagen> Imagens { get; set; } = new List<Imagen>();
}
