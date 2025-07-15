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
        //private readonly IDatabaseService _databaseService;
        private readonly IFileAnalysisUtils _fileAnalysisUtils;
        private List<string> directories { get; set; }

        public FileScannerService(//IDatabaseService databaseService, 
            IFileAnalysisUtils fileAnalysisUtils)
        {
            //_databaseService = databaseService;
            _fileAnalysisUtils = fileAnalysisUtils;
        }

        public void RunScanner()
        {
            Console.Write("Por favor, introduce la ruta del directorio a escanear: ");
            string targetDirectory = Console.ReadLine();
            Console.WriteLine($"Escaneando directorio: {targetDirectory} - {DateTime.Now.ToString(format: "yyyy-MM-dd HH:mm")}");

            if (string.IsNullOrWhiteSpace(targetDirectory) || !Directory.Exists(targetDirectory))
            {
                Console.WriteLine($"Error: La ruta '{targetDirectory}' no es un directorio válido o está vacía.");
                return;
            }
            var options = new EnumerationOptions
            {
                RecurseSubdirectories = true,
                MatchCasing = MatchCasing.CaseInsensitive,
                IgnoreInaccessible = true // Ignora directorios a los que no se puede acceder
            };
            List<string> rootFiles = new List<string>(Directory.EnumerateDirectories(targetDirectory,"*",options));
            List<string> files = new List<string>(Directory.EnumerateFiles(targetDirectory, "*.*", options));
            // Guardar los informes en CSV
            string basePath = Path.Combine(targetDirectory, "Cert-SNR");
            Directory.CreateDirectory(basePath);


            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                Encoding = Encoding.UTF8,
                ShouldQuote = args => true // CsvHelper 30.0.0+ usa args => true
                                           // Versiones anteriores podrían usar _ => true
            };

            using (var writer = new StreamWriter(Path.Combine(basePath, "ListadoArchivos.csv")))
            using (var csv = new CsvWriter(writer, config))
            {
                // Escribir un encabezado si lo deseas (por ejemplo, "NombreArchivo")
                csv.WriteField("NombreArchivo");
                csv.NextRecord();

                // Escribir cada archivo como un registro individual
                foreach (var file in files)
                {
                    csv.WriteField(file); // Puedes escribir la ruta completa o solo el nombre del archivo
                    csv.NextRecord();
                }
            }


            this.directories = rootFiles;
            ProcessDirectory(targetDirectory).Wait(); // Espera a que el método asíncrono termine
        }

  

        private async Task ProcessDirectory(string targetDirectory)
        {
            List<PdfReportRow> pdfReport = new List<PdfReportRow>();
            List<TiffReportRow> tiffReport = new List<TiffReportRow>();

            Console.WriteLine($"\n--- Escaneando directorio y subdirectorios: {targetDirectory} ---\n");
            long totalbytes = 0;
            int totalXml = 0;
            DateTime begin = DateTime.Now;
            // Recorre el directorio raíz y todos los subdirectorios
            foreach (var dir in this.directories)
            {
                Console.WriteLine($"\n-- Carpeta: {dir} --");

                var files = Directory.GetFiles(dir);
                var pdfs = files.Where(f => f.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase)).ToList();
                var tiffs = files.Where(f => f.EndsWith(".tif", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".tiff", StringComparison.OrdinalIgnoreCase)).ToList();
                var sqls = files.Where(f => f.EndsWith(".sql", StringComparison.OrdinalIgnoreCase)).ToList();
                var excels = files.Where(f => f.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase)).ToList();
                var xml = files.Where(f => f.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".xmp", StringComparison.OrdinalIgnoreCase)).ToList();
                totalbytes += files.Sum(f => new FileInfo(f).Length);
                totalXml += xml.Count;
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

                    int diff = pdfMetadata.PageCount - tiffs.Count; // Usa el conteo de páginas de los metadatos

                    pdfReport.Add(new PdfReportRow
                    {
                        Nombre = fileData.Name,
                        Ruta = fileData.Path,
                        TamanoBytes = fileData.SizeBytes,
                        Paginas = pdfMetadata.PageCount,
                        CantidadTiffs = tiffs.Count,
                        ContieneXml = xml.Any() ? "Si" : "No", // Indica si hay XML/XMP
                        DiferenciaTiffsVsPaginas = diff,
                        PdfAuthor = pdfMetadata.Author,
                        PdfTitle = pdfMetadata.Title,
                        PdfSubject = pdfMetadata.Subject,
                        PdfCreator = pdfMetadata.Creator,
                        PdfProducer = pdfMetadata.Producer,
                        PdfHashType = fileData.HashType,
                        PdfHash = fileData.Hash,
                        PdfCreationDate = fileData.CreatedAt,
                        PdfModificationDate = fileData.ModifiedAt,
                        PdfDescription = pdfMetadata.Keywords
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

                        tiffReport.Add(new TiffReportRow
                        {
                            Nombre = fileData.Name,
                            Ruta = fileData.Path,
                            TamanoBytes = fileData.SizeBytes,
                        });

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"⚠️ No se pudo leer imagen TIFF: {fileData.Path} - Error: {ex.Message}");
                    }
                }
            }

            // Guardar los informes en CSV
            string basePath = Path.Combine(targetDirectory, "Cert-SNR");
            Directory.CreateDirectory(basePath);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                Encoding = Encoding.Latin1,
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

            List<GeneralReport> generalReport = new List<GeneralReport> {
                 new GeneralReport()
                {
                    Item = "Total de PDFs",
                    Total = pdfReport.Count.ToString()
                },
                new GeneralReport()
                {
                    Item = "Total de TIFFs",
                    Total = tiffReport.Count.ToString()
                },
                new GeneralReport()
                {
                    Item = "Total de XML/XMP",
                    Total = totalXml.ToString()
                },
                new GeneralReport()
                {
                    Item = "Total de BYTES",
                    Total = totalbytes.ToString()
                },
                new GeneralReport()
                {
                    Item = "Fecha Inicio",
                    Total = begin.ToString(format:"yyyy-MM-dd HH:m")
                },
                new GeneralReport()
                {
                    Item = "Fecha Termina",
                    Total = DateTime.Now.ToString(format:"yyyy-MM-dd HH:m")
                },
            }
            ;
            using (var writer = new StreamWriter(Path.Combine(basePath, "Cert.csv")))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteHeader<GeneralReport>();
                csv.NextRecord();
                csv.WriteRecords(generalReport);
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
