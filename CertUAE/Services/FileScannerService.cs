using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using CertUAE.Models; // Para PdfReportRow, TiffReportRow, FileInfoData, PdfMetadata
using CertUAE.Utilities; // Para FileAnalysisUtils

namespace CertUAE.Services
{
    public class FileScannerService : IFileScannerService
    {
        private readonly IDatabaseService _databaseService;
        private readonly IFileAnalysisUtils _fileAnalysisUtils;

        public FileScannerService(IDatabaseService databaseService, IFileAnalysisUtils fileAnalysisUtils)
        {
            _databaseService = databaseService;
            _fileAnalysisUtils = fileAnalysisUtils;
        }

        public void RunScanner()
        {
            Console.Write("Por favor, introduce la ruta del directorio a escanear: ");
            string targetDirectory = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(targetDirectory) || !Directory.Exists(targetDirectory))
            {
                Console.WriteLine($"Error: La ruta '{targetDirectory}' no es un directorio válido o está vacía.");
                return;
            }

            ValidarEstructuraDirectorio(targetDirectory);
            ProcessDirectory(targetDirectory).Wait(); // Espera a que el método asíncrono termine
        }

        private void ValidarEstructuraDirectorio(string targetDirectory)
        {
            string[] carpetasObligatorias = new[] { "PDF", "TIFF", "SQL", "XLSX" };
            foreach (string carpeta in carpetasObligatorias)
            {
                string ruta = Path.Combine(targetDirectory, carpeta);
                if (!Directory.Exists(ruta))
                {
                    Console.WriteLine($"⚠️ Advertencia: Falta carpeta obligatoria: {carpeta}");
                }
            }
        }

        private async Task ProcessDirectory(string targetDirectory)
        {
            List<PdfReportRow> pdfReport = new List<PdfReportRow>();
            List<TiffReportRow> tiffReport = new List<TiffReportRow>();

            Console.WriteLine($"\n--- Escaneando directorio y subdirectorios: {targetDirectory} ---\n");

            // Recorre el directorio raíz y todos los subdirectorios
            foreach (var dir in Directory.GetDirectories(targetDirectory, "*", SearchOption.AllDirectories).Prepend(targetDirectory))
            {
                Console.WriteLine($"\n-- Carpeta: {dir} --");

                var files = Directory.GetFiles(dir);
                var pdfs = files.Where(f => f.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase)).ToList();
                var tiffs = files.Where(f => f.EndsWith(".tif", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".tiff", StringComparison.OrdinalIgnoreCase)).ToList();
                var sqls = files.Where(f => f.EndsWith(".sql", StringComparison.OrdinalIgnoreCase)).ToList();
                var excels = files.Where(f => f.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase)).ToList();

                if (!pdfs.Any() && !tiffs.Any() && !sqls.Any() && !excels.Any())
                {
                    Console.WriteLine("⚠️ No se encontraron archivos válidos en esta carpeta.");
                    continue;
                }

                foreach (var pdfPath in pdfs)
                {
                    var fileData = _fileAnalysisUtils.GetFileInfo(pdfPath);
                    var pdfMetadata = _fileAnalysisUtils.GetPdfMetadata(pdfPath); // Obtener todos los metadatos del PDF

                    Console.WriteLine($"PDF: {fileData.Name} - Páginas: {pdfMetadata.PageCount}");
                    Console.WriteLine($"  Autor: {pdfMetadata.Author ?? "N/A"}");
                    Console.WriteLine($"  Título: {pdfMetadata.Title ?? "N/A"}");
                    Console.WriteLine($"  Software Creador: {pdfMetadata.Creator ?? "N/A"}");
                    Console.WriteLine($"  Software Productor: {pdfMetadata.Producer ?? "N/A"}");
                    Console.WriteLine($"  Fecha Creación: {pdfMetadata.CreationDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? "N/A"}");
                    Console.WriteLine($"  Fecha Modificación: {pdfMetadata.ModDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? "N/A"}");


                    int diff = pdfMetadata.PageCount - tiffs.Count; // Usa el conteo de páginas de los metadatos

                    pdfReport.Add(new PdfReportRow
                    {
                        Nombre = fileData.Name,
                        Ruta = fileData.Path,
                        TamanoKB = fileData.SizeBytes / 1024,
                        TamanoMB = fileData.SizeBytes / (1024.0 * 1024.0),
                        TamanoGB = fileData.SizeBytes / (1024.0 * 1024.0 * 1024.0),
                        Paginas = pdfMetadata.PageCount,
                        CantidadTiffs = tiffs.Count,
                        DiferenciaTiffsVsPaginas = diff,
                        PdfAuthor = pdfMetadata.Author,
                        PdfTitle = pdfMetadata.Title,
                        PdfSubject = pdfMetadata.Subject,
                        PdfCreator = pdfMetadata.Creator,
                        PdfProducer = pdfMetadata.Producer,
                        PdfCreationDate = pdfMetadata.CreationDate,
                        PdfModificationDate = pdfMetadata.ModDate
                    });

                    if (pdfMetadata.PageCount == tiffs.Count)
                        Console.WriteLine($"✅ Coincidencia: {fileData.Name} tiene {pdfMetadata.PageCount} páginas y {tiffs.Count} TIFFs.");
                    else
                        Console.WriteLine($"❌ Diferencia: {fileData.Name} tiene {pdfMetadata.PageCount} páginas y {tiffs.Count} TIFFs.");
                }

                foreach (var tiffPath in tiffs)
                {
                    var fileData = _fileAnalysisUtils.GetFileInfo(tiffPath);
                    try
                    {
                        using (Image<Rgba32> image = Image.Load<Rgba32>(tiffPath))
                        {
                            tiffReport.Add(new TiffReportRow
                            {
                                Nombre = fileData.Name,
                                Ruta = fileData.Path,
                                TamanoKB = fileData.SizeBytes / 1024,
                                TamanoMB = fileData.SizeBytes / (1024.0 * 1024.0),
                                TamanoGB = fileData.SizeBytes / (1024.0 * 1024.0 * 1024.0),
                                Alto = image.Height,
                                Ancho = image.Width
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"⚠️ No se pudo leer imagen TIFF: {fileData.Path} - Error: {ex.Message}");
                    }
                }
            }

            // Guardar los informes en CSV
            string basePath = Path.Combine(targetDirectory, "export_resultados");
            Directory.CreateDirectory(basePath);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                Encoding = Encoding.UTF8,
                ShouldQuote = _ => true
            };

            using (var writer = new StreamWriter(Path.Combine(basePath, "pdf_report.csv")))
            using (var csv = new CsvWriter(writer, config))
            {
                // Escribir el encabezado con los nuevos campos de metadatos
                csv.WriteHeader<PdfReportRow>();
                csv.NextRecord();
                csv.WriteRecords(pdfReport);
            }

            using (var writer = new StreamWriter(Path.Combine(basePath, "tiff_report.csv")))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteHeader<TiffReportRow>();
                csv.NextRecord();
                csv.WriteRecords(tiffReport);
            }

            Console.WriteLine($"\n✅ Exportación de informes CSV completada en: {basePath}");

            // Prueba de conexión y lectura de datos de la DB (opcional, para verificar)
            Console.WriteLine("\n--- Probando conexión a la Base de Datos ---");
            try
            {
                //var anotaciones = await _databaseService.GetAllAnotacionesAsync();
               // Console.WriteLine($"Se encontraron {anotaciones.Count} anotaciones en la base de datos.");
                // Puedes iterar sobre las anotaciones si quieres mostrar más detalles
                // foreach (var anotacion in anotaciones.Take(5)) // Muestra las primeras 5
                // {
                //     Console.WriteLine($"- Anotacion ID: {anotacion.IdAnotacion}, Radicado: {anotacion.NumeroDeRadicado}");
                // }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al interactuar con la base de datos: {ex.Message}");
            }
        }
    }
}
