using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using CertUAE.Models; // Para PdfReportRow, TiffReportRow, FileInfoData, PdfMetadata
using CertUAE.Utilities; // Para FileAnalysisUtils
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Linq;
using System.Management;

namespace CertUAE.Services
{
    public class FileScannerService : IFileScannerService
    {
        //private readonly IDatabaseService _databaseService;
        private readonly IFileAnalysisUtils _fileAnalysisUtils;
        private List<string> directories { get; set; }

        string basePath { get; set; }

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
            var options = new System.IO.EnumerationOptions
            {
                RecurseSubdirectories = true,
                MatchCasing = MatchCasing.CaseInsensitive,
                IgnoreInaccessible = true
            };
            List<string> rootFiles = new List<string>(Directory.EnumerateDirectories(targetDirectory, "*", options));
            List<string> files = new List<string>(Directory.EnumerateFiles(targetDirectory, "*.*", options));
            files.AddRange(rootFiles);
            basePath = Path.Combine(targetDirectory, "Cert-SNR");
            Directory.CreateDirectory(basePath);

            Console.WriteLine("\n--- Selecciona una opción ---");
            Console.WriteLine("1. Generar solo listado de archivos");
            Console.WriteLine("2. Generar solo procesamiento.");
            Console.WriteLine("3. Generar Certificacion todos los archivos");
            Console.Write("Tu opción: ");
            string generarRutero = Console.ReadLine();
            switch (generarRutero)
            {
                case "1":
                    Rutero(files).Wait();
                    break;
                case "2":
                    this.directories = rootFiles;
                    ProcessDirectory(targetDirectory).Wait();
                    break;
                case "3":
                    Rutero(files).Wait();
                    this.directories = rootFiles;
                    ProcessDirectory(targetDirectory).Wait();
                    break;
                default:
                    Console.WriteLine("Opción no válida. No se generará el rutero.");
                    break;
            }
        }

        private async Task Rutero(List<string> targetDirectory)
        {
            var options = new System.IO.EnumerationOptions
            {
                RecurseSubdirectories = true,
                MatchCasing = MatchCasing.CaseInsensitive,
                IgnoreInaccessible = true
            };
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                Encoding = new UTF8Encoding(true),
                ShouldQuote = args => true
            };

            using (var writer = new StreamWriter(Path.Combine(basePath, "ListadoArchivos.csv"), false, new UTF8Encoding(true)))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteField("NombreArchivo");
                csv.NextRecord();
                foreach (var file in targetDirectory.Order())
                {
                    csv.WriteField(file);
                    csv.NextRecord();
                }
            }
            Console.WriteLine($"Listado de archivos guardado en: {Path.Combine(basePath, "ListadoArchivos.csv")}");
        }


        private async Task ProcessDirectory(string targetDirectory)
        {
            List<PdfReportRow> pdfReport = new List<PdfReportRow>();
            List<TiffReportRow> tiffReport = new List<TiffReportRow>();

            Console.WriteLine($"\n--- Escaneando directorio y subdirectorios: {targetDirectory} ---\n");
            long totalbytes = 0;
            int totalXml = 0;
            DateTime begin = DateTime.Now;

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
                    Console.WriteLine($"PDF: {fileData.Name} - Páginas: {((pdfMetadata != null) ? pdfMetadata.PageCount : 0)}");

                    int diff = ((pdfMetadata != null) ? pdfMetadata.PageCount : 0) - tiffs.Count; // Usa el conteo de páginas de los metadatos

                    //var isSigned = _fileAnalysisUtils.ValidatePdfDigitalSignature(pdfPath);
                    //bool hasOcr = _fileAnalysisUtils.HasOcrText(pdfPath);

                    pdfReport.Add(new PdfReportRow
                    {
                        Nombre = fileData.Name,
                        Ruta = fileData.Path,
                        TamanoBytes = fileData.SizeBytes,
                        Paginas = ((pdfMetadata != null) ? pdfMetadata.PageCount : 0),
                        CantidadTiffs = tiffs.Count,
                        ContieneXml = xml.Any() ? "Si" : "No", // Indica si hay XML/XMP
                        DiferenciaTiffsVsPaginas = diff,
                        PdfAuthor = pdfMetadata?.Author,
                        PdfTitle = pdfMetadata?.Title,
                        PdfSubject = pdfMetadata?.Subject,
                        PdfCreator = pdfMetadata?.Creator,
                        PdfProducer = pdfMetadata?.Producer,
                        PdfHashType = fileData.HashType,
                        PdfHash = fileData.Hash,
                        PdfCreationDate = pdfMetadata.CreationDate,
                        PdfModificationDate = pdfMetadata.ModDate,
                        //Sellado = isSigned,
                        //ContieneOCR = hasOcr ? "Si" : "No",
                        PdfDescription = pdfMetadata?.Keywords
                    });

                    if (((pdfMetadata != null) ? pdfMetadata.PageCount : 0) == tiffs.Count)
                        Console.WriteLine($"✅ Coincidencia: {fileData.Name} tiene {pdfMetadata?.PageCount} páginas y {tiffs.Count} TIFFs.");
                    else
                        Console.WriteLine($"❌ Diferencia: {fileData.Name} tiene {pdfMetadata?.PageCount} páginas y {tiffs.Count} TIFFs.");
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
                Encoding = new UTF8Encoding(true),
                ShouldQuote = _ => true
            };

            using (var writer = new StreamWriter(Path.Combine(basePath, "pdf_report.csv"), false, new UTF8Encoding(true)))
            using (var csv = new CsvWriter(writer, config))
            {
                // Escribir el encabezado con los nuevos campos de metadatos
                csv.WriteHeader<PdfReportRow>();
                csv.NextRecord();
                csv.WriteRecords(pdfReport);
            }

            using (var writer = new StreamWriter(Path.Combine(basePath, "tiff_report.csv"), false, new UTF8Encoding(true)))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteHeader<TiffReportRow>();
                csv.NextRecord();
                csv.WriteRecords(tiffReport);
            }
            

            List<GeneralReport> generalReport = new List<GeneralReport> {
                 new GeneralReport()
                {
                    Item = "Equipo Evaluador",
                    Total = Environment.MachineName
                },
                  new GeneralReport()
                {
                    Item = "Usuario Evaluador",
                    Total = Environment.UserName
                },
                 new GeneralReport()
                {
                    Item = "Unidad Evaluada",
                    Total = targetDirectory
                },
                  new GeneralReport()
                {
                    Item = "N° Serie",
                    Total = GetHardDriveSerialNumber(targetDirectory.Replace("\\", ""))
                },
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
                new GeneralReport()
                {
                    Item = "Tiempo total analizado",
                    Total =  CalcularYDiferencia(begin, DateTime.Now)
                },

            }
            ;
            using (var writer = new StreamWriter(Path.Combine(basePath, "Cert.csv"), false, new UTF8Encoding(true)))
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


        private string GetHardDriveSerialNumber(string driveLetter)
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT VolumeSerialNumber FROM Win32_LogicalDisk WHERE DeviceID = '" + driveLetter + "'");

                foreach (ManagementObject obj in searcher.Get())
                {
                    return obj["VolumeSerialNumber"].ToString();
                }
                return "";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el número de serie del disco duro ({driveLetter}): {ex.Message}");
                Console.WriteLine("Asegúrate de que la referencia a 'System.Management' está añadida y que la aplicación se ejecuta con suficientes privilegios si es necesario.");
                return "";
            }
        }

        public string CalcularYDiferencia(DateTime fechaInicio, DateTime fechaFin)
        {
            Console.WriteLine($"Calculando diferencia entre {fechaInicio:yyyy-MM-dd HH:mm} y {fechaFin:yyyy-MM-dd HH:mm}");

            // Calcula la diferencia usando TimeSpan
            TimeSpan diferencia = fechaFin - fechaInicio;

            // Extrae los componentes de la diferencia
            int dias = diferencia.Days;
            int horas = diferencia.Hours;
            int minutos = diferencia.Minutes;

            // Formatea el resultado
            string resultado = $"{dias} día{(dias == 1 ? "" : "s")} {horas} hora{(horas == 1 ? "" : "s")} {minutos} minuto{(minutos == 1 ? "" : "s")}";

            return resultado;
        }
    }
}
