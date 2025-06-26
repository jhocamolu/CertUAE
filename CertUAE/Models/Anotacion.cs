using System;
using System.Collections.Generic;

namespace CertUAE.Models;

/// <summary>
/// NA para Libros Antiguo Sistema
/// </summary>
public partial class Anotacion
{
    public long IdAnotacion { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public long IdAntecedentesReg { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public long IdDocumento { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public long IdNotaria { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public string IdOficinaOrigen { get; set; } = null!;

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public string IdNaturalezaJuridica { get; set; } = null!;

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public long IdTipoActo { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public int NumAnotacion { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public string NumeroDeRadicado { get; set; } = null!;

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public DateOnly FechaDeRadicado { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public string EstadoDelFolio { get; set; } = null!;

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public int FolioInicial { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public int FolioFinal { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public int TotalFoliosDigitalizados { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public int FolioActo { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public int NumDocumento { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public string DireccionPredio { get; set; } = null!;

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public DateOnly FechaDocumento { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public DateOnly FechaAnotacion { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public DateOnly FechaDeRegistro { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public int NumeroRegistro { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public DateOnly FechaApertura { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public ulong FaltaFormulario { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public string MatriculaApertura { get; set; } = null!;

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public ulong? FaltaAnexo { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public ulong? FaltaTramite { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public ulong? ContienePeriodico { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public ulong? ContieneMapa { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public ulong? ContieneCd { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public ulong? ContienePlano { get; set; }

    public virtual AntecedentesReg IdAntecedentesRegNavigation { get; set; } = null!;

    public virtual Documento IdDocumentoNavigation { get; set; } = null!;

    public virtual CodigoNaturalezaJuridica IdNaturalezaJuridicaNavigation { get; set; } = null!;

    public virtual Notaria IdNotariaNavigation { get; set; } = null!;

    public virtual OficinaOrigen IdOficinaOrigenNavigation { get; set; } = null!;

    public virtual TipoActo IdTipoActoNavigation { get; set; } = null!;

    public virtual ICollection<Interviniente> Intervinientes { get; set; } = new List<Interviniente>();
}
