using System;
using System.Collections.Generic;

namespace CertUAE.Models;

public partial class Inventario
{
    public long IdInventario { get; set; }

    /// <summary>
    /// NA para Libros Antiguo Sistema, ni para antecedentes registrales.
    /// </summary>
    public string Dependnecia { get; set; } = null!;

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public string InventarioEstante { get; set; } = null!;

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public string CajaNumero { get; set; } = null!;

    /// <summary>
    /// Aplica para Libros que han sido desencuadernados y almacenado en carpetas
    /// </summary>
    public string CarpetaNumero { get; set; } = null!;

    /// <summary>
    /// NA para Libros Antiguo Sistema
    /// </summary>
    public int UbicacionTopograficaTomo { get; set; }

    /// <summary>
    /// Total folios segun inventario diagnostico.
    /// </summary>
    public int TotalFoliosFisicos { get; set; }

    /// <summary>
    /// 380
    /// </summary>
    public int Serie { get; set; }

    /// <summary>
    /// Fecha del Libro (inicial) segun inventario diagnostico - AAAA/MM/DD -  Para antecedente registral debe quedar según Hoja de Control o FUID
    /// </summary>
    public DateOnly Fecha { get; set; }

    public virtual ICollection<AntecedentesReg> AntecedentesRegs { get; set; } = new List<AntecedentesReg>();
}
