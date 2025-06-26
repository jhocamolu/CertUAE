using System;
using System.Collections.Generic;

namespace CertUAE.Models;

public partial class TrazabilidadUnidadDocumental
{
    public long IdTrazabUnidDocumental { get; set; }

    /// <summary>
    /// Fecha del acta de traslado de la ORIP a la Bodega del Proveedor  AAAA/MM/DD
    /// </summary>
    public DateOnly FechaSalidaUnidDocumSnr { get; set; }

    /// <summary>
    /// Fecha del acta de traslado de la Bodega del proveedor a la Bodega de la SNR  AAAA/MM/DD
    /// </summary>
    public DateOnly FechaDevolUnidDocumSnr { get; set; }

    /// <summary>
    /// SNR
    /// </summary>
    public string NombEntidRecibeUnidDocum { get; set; } = null!;

    /// <summary>
    /// Nombre del Proveedor
    /// </summary>
    public string NombEntidDevuelUnidDocum { get; set; } = null!;

    /// <summary>
    /// Aplica para Libros Antiguo Sistema - Información según formato inventario diagnóstico - ESTADOS DE CONSERVACIÓN POR NIVEL DE DETERIORO  (BAJO - MEDIO - ALTO - POSIBLE PÉRDIDA INMINENTE) 
    /// </summary>
    public string EstadoUnidEntregaDesdeSnr { get; set; } = null!;

    /// <summary>
    /// Para Libros (Como lo entrega el proveedor a la SNR), Según Formato Inventario Diagnostico.
    /// </summary>
    public string EstadoDevolvUnidASnr { get; set; } = null!;

    /// <summary>
    /// Trazabilidad realizada en herramienta tecnológica con cada una de las etapas de la unidad documental. Ejem: Desinfección puntual (LAS con Biodeterioro), Limpieza, foliación, novedades en primeros auxilios, corte de costuras, desencuadernado, unión de rasgaduras, recuperación de plano, eliminación de material metálico, calidad conservación, calidad SNR, novedades en digitalización, Indexación, calidad en digitalización por parte del proveedor, calidad en digitalización por parte de SNR, Auditoria SNR
    /// </summary>
    public string NovedadesPresentadasUnidDoc { get; set; } = null!;

    public virtual ICollection<AntecedentesReg> AntecedentesRegs { get; set; } = new List<AntecedentesReg>();
}
