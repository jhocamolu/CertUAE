using CertUAE.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Necesario para ToListAsync(), SaveChangesAsync() etc.

namespace CertUAE.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly CertDbContext _dbContext;

        public DatabaseService(CertDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SavePdfReportAsync(List<PdfReportRow> report)
        {
            Console.WriteLine("\n--- Simulando Guardado de Reporte PDF en BD ---");
            foreach (var row in report)
            {
                Console.WriteLine($"Guardando PDF: {row.Nombre}, Páginas: {row.Paginas}");
                // Aquí iría la lógica real para mapear PdfReportRow a tus modelos de BD
                // Por ejemplo, si tienes una tabla 'ReportePDFs'
                // await _dbContext.ReportePDFs.AddAsync(new ReportePDF { /* mapeo de propiedades */ });
            }
            // await _dbContext.SaveChangesAsync(); // Descomentar si realmente estás agregando a DbSets
            Console.WriteLine("Reporte PDF simulado guardado en la base de datos.");
        }

        public async Task SaveTiffReportAsync(List<TiffReportRow> report)
        {
            Console.WriteLine("\n--- Simulando Guardado de Reporte TIFF en BD ---");
            foreach (var row in report)
            {
                Console.WriteLine($"Guardando TIFF: {row.Nombre}, Alto: {row.Alto}, Ancho: {row.Ancho}");
                // Aquí iría la lógica real para mapear TiffReportRow a tus modelos de BD
                // Por ejemplo, si tienes una tabla 'ReporteTIFFs'
                // await _dbContext.ReporteTIFFs.AddAsync(new ReporteTIFF { /* mapeo de propiedades */ });
            }
            // await _dbContext.SaveChangesAsync(); // Descomentar si realmente estás agregando a DbSets
            Console.WriteLine("Reporte TIFF simulado guardado en la base de datos.");
        }

        // Puedes agregar métodos para interactuar con tus modelos reales de la BD, por ejemplo:
        public async Task<List<Anotacion>> GetAllAnotacionesAsync()
        {
            try
            {
                Console.WriteLine("Intentando obtener anotaciones de la base de datos...");
                return await _dbContext.Anotacions.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener anotaciones: {ex.Message}");
                return new List<Anotacion>();
            }
        }

    }
}

