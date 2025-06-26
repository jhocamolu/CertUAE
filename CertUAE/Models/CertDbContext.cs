using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CertUAE.Models;

public partial class CertDbContext : DbContext
{
    public CertDbContext(DbContextOptions<CertDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Anotacion> Anotacions { get; set; }

    public virtual DbSet<AntecedentesReg> AntecedentesRegs { get; set; }

    public virtual DbSet<CapaDeTexto> CapaDeTextos { get; set; }

    public virtual DbSet<CodigoNaturalezaJuridica> CodigoNaturalezaJuridicas { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<DetalleCaracteristicaFolio> DetalleCaracteristicaFolios { get; set; }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<Imagen> Imagens { get; set; }

    public virtual DbSet<Interviniente> Intervinientes { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Metadato> Metadatos { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Notaria> Notarias { get; set; }

    public virtual DbSet<OficinaOrigen> OficinaOrigens { get; set; }

    public virtual DbSet<Orip> Orips { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Tipificacion> Tipificacions { get; set; }

    public virtual DbSet<TipoActo> TipoActos { get; set; }

    public virtual DbSet<TipoAntecedente> TipoAntecedentes { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<TipoIdentificacion> TipoIdentificacions { get; set; }

    public virtual DbSet<TipoInterviniente> TipoIntervinientes { get; set; }

    public virtual DbSet<TipoLibro> TipoLibros { get; set; }

    public virtual DbSet<TipoMetadato> TipoMetadatos { get; set; }

    public virtual DbSet<TipoPredio> TipoPredios { get; set; }

    public virtual DbSet<TrazabilidadUnidadDocumental> TrazabilidadUnidadDocumentals { get; set; }

    public virtual DbSet<Vereda> Veredas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Anotacion>(entity =>
        {
            entity.HasKey(e => e.IdAnotacion).HasName("PRIMARY");

            entity.ToTable("anotacion", tb => tb.HasComment("NA para Libros Antiguo Sistema"));

            entity.HasIndex(e => e.IdAntecedentesReg, "id_antecedentes_reg_1");

            entity.HasIndex(e => e.IdDocumento, "id_documento");

            entity.HasIndex(e => e.IdNaturalezaJuridica, "id_naturaleza_juridica");

            entity.HasIndex(e => e.IdNotaria, "id_notaria");

            entity.HasIndex(e => e.IdOficinaOrigen, "id_oficina_origen");

            entity.HasIndex(e => e.IdTipoActo, "id_tipo_acto");

            entity.Property(e => e.IdAnotacion)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_anotacion");
            entity.Property(e => e.ContieneCd)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("bit(1)")
                .HasColumnName("contiene_cd");
            entity.Property(e => e.ContieneMapa)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("bit(1)")
                .HasColumnName("contiene_mapa");
            entity.Property(e => e.ContienePeriodico)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("bit(1)")
                .HasColumnName("contiene_periodico");
            entity.Property(e => e.ContienePlano)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("bit(1)")
                .HasColumnName("contiene_plano");
            entity.Property(e => e.DireccionPredio)
                .HasMaxLength(100)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("direccion_predio");
            entity.Property(e => e.EstadoDelFolio)
                .HasMaxLength(20)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("estado_del_folio");
            entity.Property(e => e.FaltaAnexo)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("bit(1)")
                .HasColumnName("falta_anexo");
            entity.Property(e => e.FaltaFormulario)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("bit(1)")
                .HasColumnName("falta_formulario");
            entity.Property(e => e.FaltaTramite)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("bit(1)")
                .HasColumnName("falta_tramite");
            entity.Property(e => e.FechaAnotacion)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("fecha_anotacion");
            entity.Property(e => e.FechaApertura)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("fecha_apertura");
            entity.Property(e => e.FechaDeRadicado)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("fecha_de_radicado");
            entity.Property(e => e.FechaDeRegistro)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("fecha_de_registro");
            entity.Property(e => e.FechaDocumento)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("fecha_documento");
            entity.Property(e => e.FolioActo)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("int(10)")
                .HasColumnName("folio_acto");
            entity.Property(e => e.FolioFinal)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("int(10)")
                .HasColumnName("folio_final");
            entity.Property(e => e.FolioInicial)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("int(10)")
                .HasColumnName("folio_inicial");
            entity.Property(e => e.IdAntecedentesReg)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_antecedentes_reg");
            entity.Property(e => e.IdDocumento)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_documento");
            entity.Property(e => e.IdNaturalezaJuridica)
                .HasMaxLength(6)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("id_naturaleza_juridica");
            entity.Property(e => e.IdNotaria)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_notaria");
            entity.Property(e => e.IdOficinaOrigen)
                .HasMaxLength(10)
                .HasDefaultValueSql("''")
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("id_oficina_origen");
            entity.Property(e => e.IdTipoActo)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_tipo_acto");
            entity.Property(e => e.MatriculaApertura)
                .HasMaxLength(50)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("matricula_apertura");
            entity.Property(e => e.NumAnotacion)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("int(5)")
                .HasColumnName("Num_Anotacion");
            entity.Property(e => e.NumDocumento)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("int(10)")
                .HasColumnName("num_documento");
            entity.Property(e => e.NumeroDeRadicado)
                .HasMaxLength(50)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("numero_de_radicado");
            entity.Property(e => e.NumeroRegistro)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("int(20)")
                .HasColumnName("numero_registro");
            entity.Property(e => e.TotalFoliosDigitalizados)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("int(10)")
                .HasColumnName("total_folios_digitalizados");

            entity.HasOne(d => d.IdAntecedentesRegNavigation).WithMany(p => p.Anotacions)
                .HasForeignKey(d => d.IdAntecedentesReg)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_antecedentes_reg_1");

            entity.HasOne(d => d.IdDocumentoNavigation).WithMany(p => p.Anotacions)
                .HasForeignKey(d => d.IdDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_documento");

            entity.HasOne(d => d.IdNaturalezaJuridicaNavigation).WithMany(p => p.Anotacions)
                .HasForeignKey(d => d.IdNaturalezaJuridica)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_naturaleza_juridica");

            entity.HasOne(d => d.IdNotariaNavigation).WithMany(p => p.Anotacions)
                .HasForeignKey(d => d.IdNotaria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_notaria");

            entity.HasOne(d => d.IdOficinaOrigenNavigation).WithMany(p => p.Anotacions)
                .HasForeignKey(d => d.IdOficinaOrigen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_oficina_origen");

            entity.HasOne(d => d.IdTipoActoNavigation).WithMany(p => p.Anotacions)
                .HasForeignKey(d => d.IdTipoActo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_tipo_acto");
        });

        modelBuilder.Entity<AntecedentesReg>(entity =>
        {
            entity.HasKey(e => e.IdAntecedentesReg).HasName("PRIMARY");

            entity.ToTable("antecedentes_reg", tb => tb.HasComment("Aplica para Libros Antiguo Sistema"));

            entity.HasIndex(e => e.IdDepartamento, "id_departamento_1");

            entity.HasIndex(e => e.IdDetalleCaracteristicaFolio, "id_detalle_caracteristica_folio");

            entity.HasIndex(e => e.IdInventario, "id_inventario");

            entity.HasIndex(e => e.IdMunicipio, "id_municipio_1");

            entity.HasIndex(e => e.IdOrip, "id_orip");

            entity.HasIndex(e => e.IdTipoAntecedente, "id_tipo_antecedente");

            entity.HasIndex(e => e.IdTipoLibro, "id_tipo_libro");

            entity.HasIndex(e => e.IdTipoPredio, "id_tipo_predio");

            entity.HasIndex(e => e.IdTrazabUnidDocumental, "id_trazab_unid_documental");

            entity.HasIndex(e => e.IdVereda, "id_vereda_1");

            entity.Property(e => e.IdAntecedentesReg)
                .HasComment("Autoincremental por insercion de datos - Tabla de (Antecedentes Registrales). Aplica para Libros Antiguo Sistema")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_antecedentes_reg");
            entity.Property(e => e.CodLibro)
                .HasComment("El número que identifica el Libro Ejemplo: LAS XXX-3800001, se debe colocar el N° 1")
                .HasColumnType("int(20)")
                .HasColumnName("cod_libro");
            entity.Property(e => e.DescripcionLibro)
                .HasMaxLength(5000)
                .HasComment("Según formato Inventario Diagnóstico")
                .HasColumnName("descripcion_libro");
            entity.Property(e => e.FechaFinal)
                .HasComment("Fecha final del Libro Según formato Inventario Diagnóstico")
                .HasColumnName("fecha_final");
            entity.Property(e => e.FechaInicial)
                .HasComment("Fecha inicial del Libro Según formato Inventario Diagnóstico")
                .HasColumnName("fecha_inicial");
            entity.Property(e => e.FechaLibro)
                .HasComment("Fecha inicial del Libro Según formato Inventario Diagnóstico")
                .HasColumnName("fecha_libro");
            entity.Property(e => e.FolioDeMatriculaInmobiliaria)
                .HasMaxLength(50)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("folio_de_matricula_inmobiliaria");
            entity.Property(e => e.FolioPagFina)
                .HasComment("Folio final del Libro Según formato Inventario Diagnóstico")
                .HasColumnType("int(11)")
                .HasColumnName("folio_pag_fina");
            entity.Property(e => e.FolioPagIni)
                .HasComment("Folio inicial del Libro Según formato Inventario Diagnóstico")
                .HasColumnType("int(11)")
                .HasColumnName("folio_pag_ini");
            entity.Property(e => e.IdDepartamento)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_departamento");
            entity.Property(e => e.IdDetalleCaracteristicaFolio)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_detalle_caracteristica_folio");
            entity.Property(e => e.IdInventario)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_inventario");
            entity.Property(e => e.IdMunicipio)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_municipio");
            entity.Property(e => e.IdOrip)
                .HasMaxLength(3)
                .HasColumnName("id_orip");
            entity.Property(e => e.IdTipoAntecedente)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_tipo_antecedente");
            entity.Property(e => e.IdTipoLibro)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_tipo_libro");
            entity.Property(e => e.IdTipoPredio)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_tipo_predio");
            entity.Property(e => e.IdTrazabUnidDocumental)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_trazab_unid_documental");
            entity.Property(e => e.IdVereda)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_vereda");
            entity.Property(e => e.NombreLibro)
                .HasMaxLength(100)
                .HasComment("Según formato Inventario Diagnóstico")
                .HasColumnName("nombre_libro");
            entity.Property(e => e.NumeroDeTurno)
                .HasMaxLength(50)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("numero_de_turno");
            entity.Property(e => e.NumeroLibro)
                .HasMaxLength(50)
                .HasComment("Codigo LAS (N° Rotulo GGD - X-380XXXX) - Según formato inventario Diagnóstico")
                .HasColumnName("numero_libro");
            entity.Property(e => e.NumeroLibroLomo)
                .HasComment("Según formato Inventario Diagnóstico")
                .HasColumnType("int(20)")
                .HasColumnName("numero_libro_lomo");
            entity.Property(e => e.NumeroLibroPortada)
                .HasComment("Según formato Inventario Diagnóstico")
                .HasColumnType("int(20)")
                .HasColumnName("numero_libro_portada");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(500)
                .HasComment("Se debe colocar las observaciones del formato diagnostico (Columna Observación)")
                .HasColumnName("observaciones");
            entity.Property(e => e.Proveedor)
                .HasMaxLength(50)
                .HasComment("Nombre del proveedor que intervendrá los Libros")
                .HasColumnName("proveedor");
            entity.Property(e => e.RangoPartidaDesde)
                .HasComment("Aplica cuando los Libros tienen rangos de partida")
                .HasColumnType("int(11)")
                .HasColumnName("rango_partida_desde");
            entity.Property(e => e.RangoPartidaHasta)
                .HasComment("Aplica cuando los Libros tienen rangos de partida")
                .HasColumnType("int(11)")
                .HasColumnName("rango_partida_hasta");
            entity.Property(e => e.RotuloSnr)
                .HasMaxLength(100)
                .HasComment("Rotulo SNR - Stickers fisico del Libro")
                .HasColumnName("rotulo_snr");
            entity.Property(e => e.UltFolioConContenido)
                .HasComment("Ultimo folio con contenido del Libro")
                .HasColumnType("int(11)")
                .HasColumnName("ult_folio_con_contenido");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.AntecedentesRegs)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_departamento_1");

            entity.HasOne(d => d.IdDetalleCaracteristicaFolioNavigation).WithMany(p => p.AntecedentesRegs)
                .HasForeignKey(d => d.IdDetalleCaracteristicaFolio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_detalle_caracteristica_folio");

            entity.HasOne(d => d.IdInventarioNavigation).WithMany(p => p.AntecedentesRegs)
                .HasForeignKey(d => d.IdInventario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_inventario");

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.AntecedentesRegs)
                .HasForeignKey(d => d.IdMunicipio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_municipio_1");

            entity.HasOne(d => d.IdOripNavigation).WithMany(p => p.AntecedentesRegs)
                .HasForeignKey(d => d.IdOrip)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_orip");

            entity.HasOne(d => d.IdTipoAntecedenteNavigation).WithMany(p => p.AntecedentesRegs)
                .HasForeignKey(d => d.IdTipoAntecedente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_tipo_antecedente");

            entity.HasOne(d => d.IdTipoLibroNavigation).WithMany(p => p.AntecedentesRegs)
                .HasForeignKey(d => d.IdTipoLibro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_tipo_libro");

            entity.HasOne(d => d.IdTipoPredioNavigation).WithMany(p => p.AntecedentesRegs)
                .HasForeignKey(d => d.IdTipoPredio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_tipo_predio");

            entity.HasOne(d => d.IdTrazabUnidDocumentalNavigation).WithMany(p => p.AntecedentesRegs)
                .HasForeignKey(d => d.IdTrazabUnidDocumental)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_trazab_unid_documental");

            entity.HasOne(d => d.IdVeredaNavigation).WithMany(p => p.AntecedentesRegs)
                .HasForeignKey(d => d.IdVereda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_vereda_1");
        });

        modelBuilder.Entity<CapaDeTexto>(entity =>
        {
            entity.HasKey(e => e.IdCapaDeTexto).HasName("PRIMARY");

            entity.ToTable("capa_de_texto", tb => tb.HasComment("Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema "));

            entity.Property(e => e.IdCapaDeTexto)
                .HasComment("Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_capa_de_texto");
            entity.Property(e => e.Icr)
                .HasComment("Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico")
                .HasColumnType("bit(1)")
                .HasColumnName("icr");
            entity.Property(e => e.Ocr)
                .HasComment("Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico")
                .HasColumnType("bit(1)")
                .HasColumnName("ocr");
            entity.Property(e => e.Rah)
                .HasComment("Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico")
                .HasColumnType("bit(1)")
                .HasColumnName("rah");
        });

        modelBuilder.Entity<CodigoNaturalezaJuridica>(entity =>
        {
            entity.HasKey(e => e.IdNaturalezaJuridica).HasName("PRIMARY");

            entity.ToTable("codigo_naturaleza_juridica", tb => tb.HasComment("NA para Libros Antiguo Sistema"));

            entity.Property(e => e.IdNaturalezaJuridica)
                .HasMaxLength(6)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("id_naturaleza_juridica");
            entity.Property(e => e.EspecificacionDeNaturalezaJuridica)
                .HasMaxLength(500)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("especificacion_de_naturaleza_juridica");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PRIMARY");

            entity.ToTable("departamento");

            entity.HasIndex(e => e.IdRegion, "id_region");

            entity.Property(e => e.IdDepartamento)
                .HasComment("Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_departamento");
            entity.Property(e => e.IdRegion)
                .HasComment("Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_region");
            entity.Property(e => e.NombreDepartamento)
                .HasMaxLength(50)
                .HasComment("Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico")
                .HasColumnName("nombre_departamento");

            entity.HasOne(d => d.IdRegionNavigation).WithMany(p => p.Departamentos)
                .HasForeignKey(d => d.IdRegion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_region");
        });

        modelBuilder.Entity<DetalleCaracteristicaFolio>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCaracteristicaFolio).HasName("PRIMARY");

            entity.ToTable("detalle_caracteristica_folio");

            entity.Property(e => e.IdDetalleCaracteristicaFolio)
                .HasComment("Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_detalle_caracteristica_folio");
            entity.Property(e => e.DescripcionDeterioroFolio)
                .HasMaxLength(500)
                .HasComment("Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico")
                .HasColumnName("descripcion_deterioro_folio");
            entity.Property(e => e.NivelDeterioroFolio)
                .HasMaxLength(500)
                .HasComment("Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico")
                .HasColumnName("nivel_deterioro_folio");
            entity.Property(e => e.ObservacionesFolio)
                .HasMaxLength(500)
                .HasComment("Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico")
                .HasColumnName("observaciones_folio");
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.IdDocumento).HasName("PRIMARY");

            entity.ToTable("documento", tb => tb.HasComment("NA para Libros Antiguo Sistema"));

            entity.HasIndex(e => e.IdTipoDocumento, "id_tipo_documento");

            entity.Property(e => e.IdDocumento)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_documento");
            entity.Property(e => e.FechaDocumento)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("fecha_documento");
            entity.Property(e => e.IdTipoDocumento)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_tipo_documento");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(50)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("numero_documento");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.Documentos)
                .HasForeignKey(d => d.IdTipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_tipo_documento");
        });

        modelBuilder.Entity<Imagen>(entity =>
        {
            entity.HasKey(e => e.IdImagen).HasName("PRIMARY");

            entity.ToTable("imagen");

            entity.HasIndex(e => e.IdAntecedentesReg, "id_antecedentes_reg");

            entity.HasIndex(e => e.IdCapaDeTexto, "id_capa_de_texto");

            entity.HasIndex(e => e.IdMetadato, "id_metadato");

            entity.HasIndex(e => e.IdTipificacion, "id_tipificacion");

            entity.Property(e => e.IdImagen)
                .HasComment("Autoincremental por insercion de datos - Tabla de (Imágen). Aplica para Libros Antiguo Sistema")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_imagen");
            entity.Property(e => e.DireccionContenedora)
                .HasMaxLength(300)
                .HasComment("Ruta completa sin la letra de identificación donde se almaceno la información. Ejem: Users\\LENOVO\\Desktop\\SQL_2024")
                .HasColumnName("direccion_contenedora");
            entity.Property(e => e.Extencion)
                .HasMaxLength(5)
                .HasComment("Tipo de archivo PDF ó TIF")
                .HasColumnName("extencion");
            entity.Property(e => e.FechaCreacionImagen)
                .HasComment("Fecha creación de las imágenes AAAA-MM-DD")
                .HasColumnName("fecha_creacion_imagen");
            entity.Property(e => e.FolioPaginaFinal)
                .HasComment("Ultimo folio del Libro")
                .HasColumnType("int(10)")
                .HasColumnName("folio_pagina_final");
            entity.Property(e => e.FolioPaginaInicial)
                .HasComment("Primer folio del Libro")
                .HasColumnType("int(10)")
                .HasColumnName("folio_pagina_inicial");
            entity.Property(e => e.FolioTotal)
                .HasComment("Total folios por unidad documental")
                .HasColumnType("int(10)")
                .HasColumnName("folio_total");
            entity.Property(e => e.Hash)
                .HasMaxLength(250)
                .HasComment("Hash por imágen formato SHA-1 de 250 ")
                .HasColumnName("hash");
            entity.Property(e => e.IdAntecedentesReg)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_antecedentes_reg");
            entity.Property(e => e.IdCapaDeTexto)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_capa_de_texto");
            entity.Property(e => e.IdMetadato)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_metadato");
            entity.Property(e => e.IdTipificacion)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_tipificacion");
            entity.Property(e => e.NombreImagen)
                .HasMaxLength(50)
                .HasComment("Cargue de Imágenes .PDF y Imágenes .TIF por cada unidad documental intervenida, para Libros gran formato (Unificación de Imagenes) se cargará en el SQL las imágenes unidas. Para las imágenes .TIF nativas reposaran en una carpeta contenedora por libro llamada (Imagenes TIFF nativas) ")
                .HasColumnName("nombre_imagen");
            entity.Property(e => e.TamañoImagen)
                .HasComment("Tamaño de Imagen en Bytes")
                .HasColumnType("bigint(20)")
                .HasColumnName("tamaño_imagen");
            entity.Property(e => e.UnidadContenedora)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasComment("Letra de identificación de la unidad contenedora donde se almaceno la información.  Ejem: \"C\"")
                .HasColumnName("unidad_contenedora");
            entity.Property(e => e.UserDigitador)
                .HasMaxLength(50)
                .HasComment("Nombre del Digitalizador por unidad documental, esta persona es la que aparece en la Indexación del PDF/A")
                .HasColumnName("user_digitador");

            entity.HasOne(d => d.IdAntecedentesRegNavigation).WithMany(p => p.Imagens)
                .HasForeignKey(d => d.IdAntecedentesReg)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_antecedentes_reg");

            entity.HasOne(d => d.IdCapaDeTextoNavigation).WithMany(p => p.Imagens)
                .HasForeignKey(d => d.IdCapaDeTexto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_capa_de_texto");

            entity.HasOne(d => d.IdMetadatoNavigation).WithMany(p => p.Imagens)
                .HasForeignKey(d => d.IdMetadato)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_metadato");

            entity.HasOne(d => d.IdTipificacionNavigation).WithMany(p => p.Imagens)
                .HasForeignKey(d => d.IdTipificacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_tipificacion");
        });

        modelBuilder.Entity<Interviniente>(entity =>
        {
            entity.HasKey(e => e.IdInterviniente).HasName("PRIMARY");

            entity.ToTable("intervinientes");

            entity.HasIndex(e => e.IdAnotacion, "id_anotacion");

            entity.HasIndex(e => e.IdTipoIdentificacion, "id_tipo_identificacion");

            entity.HasIndex(e => e.IdTipoInterviniente, "id_tipo_interviniente");

            entity.Property(e => e.IdInterviniente)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_interviniente");
            entity.Property(e => e.IdAnotacion)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_anotacion");
            entity.Property(e => e.IdTipoIdentificacion)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_tipo_identificacion");
            entity.Property(e => e.IdTipoInterviniente)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_tipo_interviniente");
            entity.Property(e => e.NombreInterviniente)
                .HasMaxLength(50)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("nombre_interviniente");
            entity.Property(e => e.NumeroIdentifInterviniente)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("int(20)")
                .HasColumnName("numero_identif_interviniente");

            entity.HasOne(d => d.IdAnotacionNavigation).WithMany(p => p.Intervinientes)
                .HasForeignKey(d => d.IdAnotacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_anotacion");

            entity.HasOne(d => d.IdTipoIdentificacionNavigation).WithMany(p => p.Intervinientes)
                .HasForeignKey(d => d.IdTipoIdentificacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_tipo_identificacion");

            entity.HasOne(d => d.IdTipoIntervinienteNavigation).WithMany(p => p.Intervinientes)
                .HasForeignKey(d => d.IdTipoInterviniente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_tipo_interviniente");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdInventario).HasName("PRIMARY");

            entity.ToTable("inventario");

            entity.Property(e => e.IdInventario)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_inventario");
            entity.Property(e => e.CajaNumero)
                .HasMaxLength(50)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("caja_numero");
            entity.Property(e => e.CarpetaNumero)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasComment("Aplica para Libros que han sido desencuadernados y almacenado en carpetas")
                .HasColumnName("carpeta_numero");
            entity.Property(e => e.Dependnecia)
                .HasMaxLength(100)
                .HasComment("NA para Libros Antiguo Sistema, ni para antecedentes registrales.")
                .HasColumnName("dependnecia");
            entity.Property(e => e.Fecha)
                .HasComment("Fecha del Libro (inicial) segun inventario diagnostico - AAAA/MM/DD -  Para antecedente registral debe quedar según Hoja de Control o FUID")
                .HasColumnName("fecha");
            entity.Property(e => e.InventarioEstante)
                .HasMaxLength(50)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("inventario_estante");
            entity.Property(e => e.Serie)
                .HasComment("380")
                .HasColumnType("int(20)")
                .HasColumnName("serie");
            entity.Property(e => e.TotalFoliosFisicos)
                .HasComment("Total folios segun inventario diagnostico.")
                .HasColumnType("int(20)")
                .HasColumnName("total_folios_fisicos");
            entity.Property(e => e.UbicacionTopograficaTomo)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("int(20)")
                .HasColumnName("ubicacion_topografica_tomo");
        });

        modelBuilder.Entity<Metadato>(entity =>
        {
            entity.HasKey(e => e.IdMetadato).HasName("PRIMARY");

            entity.ToTable("metadatos", tb => tb.HasComment("Aplica para Libros Antiguo Sistema y Antecedentes Registrales."));

            entity.HasIndex(e => e.IdTipoMetadato, "id_tipo_metadato");

            entity.Property(e => e.IdMetadato)
                .HasComment("Tabla de (metadatos). Aplica para Libros Antiguo Sistema y Antecedentes Registrales.")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_metadato");
            entity.Property(e => e.DireccionContenedora)
                .HasMaxLength(300)
                .HasComment("Ruta completa sin la letra de identificación donde se almaceno la información. Ejem: Users\\LENOVO\\Desktop\\SQL_2024")
                .HasColumnName("direccion_contenedora");
            entity.Property(e => e.Extencion)
                .HasMaxLength(5)
                .HasComment("Archivo .XMP y/o .XML")
                .HasColumnName("extencion");
            entity.Property(e => e.IdTipoMetadato)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_tipo_metadato");
            entity.Property(e => e.NomMetadato)
                .HasMaxLength(50)
                .HasComment("Nombre completo del archivo XMP y/o XML Aplica para Libros Antiguo Sistema XXX-3800001.XMP o XML y Antecedentes Registrales XXX-0000000.XMP o XML")
                .HasColumnName("nom_metadato");
            entity.Property(e => e.UnidadContenedora)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasComment("Letra de identificación de la unidad contenerdora donde se almaceno la información.  Ejem: \"C\"")
                .HasColumnName("unidad_contenedora");

            entity.HasOne(d => d.IdTipoMetadatoNavigation).WithMany(p => p.Metadatos)
                .HasForeignKey(d => d.IdTipoMetadato)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_tipo_metadato");
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasKey(e => e.IdMunicipio).HasName("PRIMARY");

            entity.ToTable("municipio");

            entity.HasIndex(e => e.IdDepartamento, "id_departamento");

            entity.Property(e => e.IdMunicipio)
                .HasComment("Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico -  Aplica de igual manera para antecedentes registrales.")
                .HasColumnType("bigint(5)")
                .HasColumnName("id_municipio");
            entity.Property(e => e.CodigoMunicipio)
                .HasComment("Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico -  Aplica de igual manera para antecedentes registrales.")
                .HasColumnType("bigint(5)")
                .HasColumnName("codigo_municipio");
            entity.Property(e => e.IdDepartamento)
                .HasComment("Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico -  Aplica de igual manera para antecedentes registrales.")
                .HasColumnType("bigint(5)")
                .HasColumnName("id_departamento");
            entity.Property(e => e.NombreMunicipio)
                .HasMaxLength(30)
                .HasComment("Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico -  Aplica de igual manera para antecedentes registrales.")
                .HasColumnName("nombre_municipio");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Municipios)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_departamento");
        });

        modelBuilder.Entity<Notaria>(entity =>
        {
            entity.HasKey(e => e.IdNotaria).HasName("PRIMARY");

            entity.ToTable("notarias", tb => tb.HasComment("Cargada con información de la SNR. No aplica para Libros Antiguo Sistema,  ni para Antecedentes."));

            entity.Property(e => e.IdNotaria)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_notaria");
            entity.Property(e => e.CodigoDane)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnType("int(20)")
                .HasColumnName("codigo_dane");
            entity.Property(e => e.NombreNotaria)
                .HasMaxLength(100)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("nombre_notaria");
        });

        modelBuilder.Entity<OficinaOrigen>(entity =>
        {
            entity.HasKey(e => e.IdOficinaOrigen).HasName("PRIMARY");

            entity.ToTable("oficina_origen");

            entity.Property(e => e.IdOficinaOrigen)
                .HasMaxLength(10)
                .HasColumnName("id_oficina_origen");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Orip>(entity =>
        {
            entity.HasKey(e => e.IdOrip).HasName("PRIMARY");

            entity.ToTable("orip");

            entity.HasIndex(e => e.IdDepartamento, "id_departamento_2");

            entity.HasIndex(e => e.IdMunicipio, "id_municipio_2");

            entity.Property(e => e.IdOrip)
                .HasMaxLength(3)
                .HasComment("Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico - Aplica para antecedentes registrales")
                .HasColumnName("id_orip");
            entity.Property(e => e.IdDepartamento)
                .HasComment("Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico - Aplica para antecedentes registrales")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_departamento");
            entity.Property(e => e.IdMunicipio)
                .HasComment("Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico - Aplica para antecedentes registrales")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_municipio");
            entity.Property(e => e.NombreOficina)
                .HasMaxLength(50)
                .HasComment("Tabla poblada con datos SNR-Aplica para Libros Antiguo Sistema - Información de formato inventario diagnóstico - Aplica para antecedentes registrales")
                .HasColumnName("nombre_oficina");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Orips)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_departamento_2");

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.Orips)
                .HasForeignKey(d => d.IdMunicipio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_municipio_2");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.IdRegion).HasName("PRIMARY");

            entity.ToTable("region");

            entity.Property(e => e.IdRegion)
                .HasComment("Poblada por SNR")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_region");
            entity.Property(e => e.NombreRegion)
                .HasMaxLength(50)
                .HasComment("Poblada por SNR")
                .HasColumnName("nombre_region");
        });

        modelBuilder.Entity<Tipificacion>(entity =>
        {
            entity.HasKey(e => e.IdTipificacion).HasName("PRIMARY");

            entity.ToTable("tipificacion");

            entity.Property(e => e.IdTipificacion)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_tipificacion");
            entity.Property(e => e.NombTipiTipoDocumento)
                .HasMaxLength(50)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("nomb_tipi_tipo_documento");
        });

        modelBuilder.Entity<TipoActo>(entity =>
        {
            entity.HasKey(e => e.IdTipoActo).HasName("PRIMARY");

            entity.ToTable("tipo_acto");

            entity.Property(e => e.IdTipoActo)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_tipo_acto");
            entity.Property(e => e.DescripcionActo)
                .HasMaxLength(500)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("descripcion_acto");
            entity.Property(e => e.TipoActo1)
                .HasMaxLength(100)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("tipo_acto");
        });

        modelBuilder.Entity<TipoAntecedente>(entity =>
        {
            entity.HasKey(e => e.IdTipoAntecedente).HasName("PRIMARY");

            entity.ToTable("tipo_antecedente");

            entity.Property(e => e.IdTipoAntecedente)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_tipo_antecedente");
            entity.Property(e => e.NombreTipoAntecedente)
                .HasMaxLength(50)
                .HasComment("Campo con datos SNR-Aplica para Libros Antiguo Sistema")
                .HasColumnName("nombre_tipo_antecedente");
            entity.Property(e => e.SerieDocumental)
                .HasComment("380 - Número de identificación de la SNR para Libros Antiguo Sistema")
                .HasColumnType("int(50)")
                .HasColumnName("serie_documental");
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumento).HasName("PRIMARY");

            entity.ToTable("tipo_documento");

            entity.Property(e => e.IdTipoDocumento)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_tipo_documento");
            entity.Property(e => e.NombreDocumento)
                .HasMaxLength(70)
                .HasColumnName("nombre_documento");
        });

        modelBuilder.Entity<TipoIdentificacion>(entity =>
        {
            entity.HasKey(e => e.IdTipoIdentificacion).HasName("PRIMARY");

            entity.ToTable("tipo_identificacion");

            entity.Property(e => e.IdTipoIdentificacion)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_tipo_identificacion");
            entity.Property(e => e.NombreTipoIdentificacion)
                .HasMaxLength(50)
                .HasComment("Campo con datos SNR- NA para Libros Antiguo Sistema")
                .HasColumnName("nombre_tipo_identificacion");
        });

        modelBuilder.Entity<TipoInterviniente>(entity =>
        {
            entity.HasKey(e => e.IdTipoInterviniente).HasName("PRIMARY");

            entity.ToTable("tipo_interviniente");

            entity.Property(e => e.IdTipoInterviniente)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_tipo_interviniente");
            entity.Property(e => e.TipoInterviniente1)
                .HasMaxLength(50)
                .HasComment("NA para Libros Antiguo Sistema")
                .HasColumnName("tipo_interviniente");
        });

        modelBuilder.Entity<TipoLibro>(entity =>
        {
            entity.HasKey(e => e.IdTipoLibro).HasName("PRIMARY");

            entity.ToTable("tipo_libro");

            entity.Property(e => e.IdTipoLibro)
                .HasComment("Campo con datos SNR-Aplica para Libros Antiguo Sistema -        id_tipo libro:1    serie_libro:380    serie_documental:Libros Antiguo Sistema")
                .HasColumnType("bigint(20)")
                .HasColumnName("id_tipo_libro");
            entity.Property(e => e.SerieDocumental)
                .HasMaxLength(100)
                .HasComment("Campo con datos SNR-Aplica para Libros Antiguo Sistema -        id_tipo libro:1    serie_libro:380    serie_documental:Libros Antiguo Sistema")
                .HasColumnName("serie_documental");
            entity.Property(e => e.SerieLibro)
                .HasComment("Campo con datos SNR-Aplica para Libros Antiguo Sistema -        id_tipo libro:1    serie_libro:380    serie_documental:Libros Antiguo Sistema")
                .HasColumnType("int(11)")
                .HasColumnName("serie_libro");
            entity.Property(e => e.SubSerieLibro)
                .HasComment("Campo con datos SNR-Aplica para Libros Antiguo Sistema -        id_tipo libro:1    serie_libro:380    serie_documental:Libros Antiguo Sistema")
                .HasColumnType("int(11)")
                .HasColumnName("sub_serie_libro");
            entity.Property(e => e.TipoDocumental)
                .HasMaxLength(20)
                .HasComment("Campo con datos SNR-Aplica para Libros Antiguo Sistema -        id_tipo libro:1    serie_libro:380    serie_documental:Libros Antiguo Sistema")
                .HasColumnName("tipo_documental");
        });

        modelBuilder.Entity<TipoMetadato>(entity =>
        {
            entity.HasKey(e => e.IdTipoMetadato).HasName("PRIMARY");

            entity.ToTable("tipo_metadato");

            entity.Property(e => e.IdTipoMetadato)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_tipo_metadato");
            entity.Property(e => e.NomTipoMetadato)
                .HasMaxLength(50)
                .HasComment("Campo con datos SNR-Aplica para Libros Antiguo Sistema    -   id_tipo_metadato:1   nom_tipo_metadato: ND")
                .HasColumnName("nom_tipo_metadato");
        });

        modelBuilder.Entity<TipoPredio>(entity =>
        {
            entity.HasKey(e => e.IdTipoPredio).HasName("PRIMARY");

            entity.ToTable("tipo_predio");

            entity.Property(e => e.IdTipoPredio)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_tipo_predio");
            entity.Property(e => e.NombreDelPredio)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasComment("Campo con datos SNR-Aplica para Libros Antiguo Sistema    -      id_tipo_predio:1  nombre_del_predio:NO APLICA - Para antecedentes registrales depende del predio.")
                .HasColumnName("nombre_del_predio");
        });

        modelBuilder.Entity<TrazabilidadUnidadDocumental>(entity =>
        {
            entity.HasKey(e => e.IdTrazabUnidDocumental).HasName("PRIMARY");

            entity.ToTable("trazabilidad_unidad_documental");

            entity.Property(e => e.IdTrazabUnidDocumental)
                .ValueGeneratedNever()
                .HasColumnType("bigint(20)")
                .HasColumnName("id_trazab_unid_documental");
            entity.Property(e => e.EstadoDevolvUnidASnr)
                .HasMaxLength(60)
                .HasComment("Para Libros (Como lo entrega el proveedor a la SNR), Según Formato Inventario Diagnostico.")
                .HasColumnName("estado_devolv_unid_a_snr");
            entity.Property(e => e.EstadoUnidEntregaDesdeSnr)
                .HasMaxLength(60)
                .HasComment("Aplica para Libros Antiguo Sistema - Información según formato inventario diagnóstico - ESTADOS DE CONSERVACIÓN POR NIVEL DE DETERIORO  (BAJO - MEDIO - ALTO - POSIBLE PÉRDIDA INMINENTE) ")
                .HasColumnName("estado_unid_entrega_desde_snr");
            entity.Property(e => e.FechaDevolUnidDocumSnr)
                .HasComment("Fecha del acta de traslado de la Bodega del proveedor a la Bodega de la SNR  AAAA/MM/DD")
                .HasColumnName("fecha_devol_unid_docum_snr");
            entity.Property(e => e.FechaSalidaUnidDocumSnr)
                .HasComment("Fecha del acta de traslado de la ORIP a la Bodega del Proveedor  AAAA/MM/DD")
                .HasColumnName("fecha_salida_unid_docum_snr");
            entity.Property(e => e.NombEntidDevuelUnidDocum)
                .HasMaxLength(60)
                .HasComment("Nombre del Proveedor")
                .HasColumnName("nomb_entid_devuel_unid_docum");
            entity.Property(e => e.NombEntidRecibeUnidDocum)
                .HasMaxLength(60)
                .HasComment("SNR")
                .HasColumnName("nomb_entid_recibe_unid_docum");
            entity.Property(e => e.NovedadesPresentadasUnidDoc)
                .HasMaxLength(1000)
                .HasComment("Trazabilidad realizada en herramienta tecnológica con cada una de las etapas de la unidad documental. Ejem: Desinfección puntual (LAS con Biodeterioro), Limpieza, foliación, novedades en primeros auxilios, corte de costuras, desencuadernado, unión de rasgaduras, recuperación de plano, eliminación de material metálico, calidad conservación, calidad SNR, novedades en digitalización, Indexación, calidad en digitalización por parte del proveedor, calidad en digitalización por parte de SNR, Auditoria SNR")
                .HasColumnName("novedades_presentadas_unid_doc");
        });

        modelBuilder.Entity<Vereda>(entity =>
        {
            entity.HasKey(e => e.IdVereda).HasName("PRIMARY");

            entity.ToTable("veredas");

            entity.HasIndex(e => e.IdMunicipio, "id_municipio");

            entity.Property(e => e.IdVereda)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_vereda");
            entity.Property(e => e.IdMunicipio)
                .HasComment("Campo con datos SNR-Aplica para Libros Antiguo Sistema - Aplica de igual manera para antecedentes registrales")
                .HasColumnType("bigint(5)")
                .HasColumnName("id_municipio");
            entity.Property(e => e.NombreVereda)
                .HasMaxLength(70)
                .HasComment("Campo con datos SNR-Aplica para Libros Antiguo Sistema - Aplica de igual manera para antecedentes registrales")
                .HasColumnName("nombre_vereda");

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.Vereda)
                .HasForeignKey(d => d.IdMunicipio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_municipio");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
